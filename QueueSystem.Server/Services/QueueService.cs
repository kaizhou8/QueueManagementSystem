using Microsoft.AspNetCore.SignalR;
using QueueSystem.Server.Hubs;
using QueueSystem.Shared.Models;

namespace QueueSystem.Server.Services
{
    /// <summary>
    /// Service for managing the queue system
    /// </summary>
    public class QueueService
    {
        private readonly IHubContext<QueueHub> _hubContext;
        private readonly Dictionary<string, Queue<QueueTicket>> _queues;
        private readonly List<Counter> _counters;
        private readonly Dictionary<string, ServiceType> _serviceTypes;

        public QueueService(IHubContext<QueueHub> hubContext)
        {
            _hubContext = hubContext;
            _queues = new Dictionary<string, Queue<QueueTicket>>();
            _counters = new List<Counter>();
            _serviceTypes = new Dictionary<string, ServiceType>();

            InitializeServiceTypes();
            InitializeCounters();
        }

        /// <summary>
        /// Initialize available service types
        /// </summary>
        private void InitializeServiceTypes()
        {
            var services = new List<ServiceType>
            {
                new ServiceType 
                { 
                    Id = "general", 
                    Name = "General Service",
                    Description = "General inquiries and services",
                    AverageProcessingTime = 10,
                    DefaultPriority = 1,
                    TicketPrefix = "A",
                    IsActive = true
                },
                new ServiceType 
                { 
                    Id = "express",
                    Name = "Express Service",
                    Description = "Quick services under 5 minutes",
                    AverageProcessingTime = 5,
                    DefaultPriority = 2,
                    TicketPrefix = "E",
                    IsActive = true
                },
                new ServiceType 
                { 
                    Id = "vip",
                    Name = "VIP Service",
                    Description = "Priority services for VIP customers",
                    AverageProcessingTime = 15,
                    DefaultPriority = 3,
                    TicketPrefix = "V",
                    IsActive = true
                }
            };

            foreach (var service in services)
            {
                _serviceTypes[service.Id] = service;
                _queues[service.Id] = new Queue<QueueTicket>();
            }
        }

        /// <summary>
        /// Initialize service counters
        /// </summary>
        private void InitializeCounters()
        {
            for (int i = 1; i <= 5; i++)
            {
                _counters.Add(new Counter
                {
                    Number = i,
                    Name = $"Counter {i}",
                    Status = CounterStatus.Available,
                    ServiceTypes = new List<string> { "general", "express" },
                    CurrentTicketNumber = null,
                    CurrentOperator = null
                });
            }

            // Add VIP counter
            _counters.Add(new Counter
            {
                Number = 6,
                Name = "VIP Counter",
                Status = CounterStatus.Available,
                ServiceTypes = new List<string> { "vip" },
                CurrentTicketNumber = null,
                CurrentOperator = null
            });
        }

        /// <summary>
        /// Create a new ticket
        /// </summary>
        public async Task<QueueTicket> CreateTicketAsync(string serviceType)
        {
            if (!_serviceTypes.ContainsKey(serviceType))
                throw new ArgumentException("Invalid service type");

            var service = _serviceTypes[serviceType];
            var ticketNumber = GenerateTicketNumber(service.TicketPrefix);
            
            var ticket = new QueueTicket
            {
                Number = ticketNumber,
                ServiceType = serviceType,
                Priority = service.DefaultPriority,
                CreatedTime = DateTime.Now,
                Status = TicketStatus.Waiting,
                CounterNumber = null,
                EstimatedWaitTime = CalculateEstimatedWaitTime(serviceType)
            };

            _queues[serviceType].Enqueue(ticket);
            await _hubContext.Clients.All.SendAsync("ReceiveNewTicket", ticket);
            
            return ticket;
        }

        /// <summary>
        /// Call next ticket for a counter
        /// </summary>
        public async Task<QueueTicket> CallNextTicketAsync(int counterNumber)
        {
            var counter = _counters.FirstOrDefault(c => c.Number == counterNumber)
                ?? throw new ArgumentException("Invalid counter number");

            if (counter.Status != CounterStatus.Available)
                throw new InvalidOperationException("Counter is not available");

            // Find next ticket from available service types for this counter
            QueueTicket nextTicket = null;
            foreach (var serviceType in counter.ServiceTypes)
            {
                if (_queues[serviceType].Count > 0)
                {
                    nextTicket = _queues[serviceType].Dequeue();
                    break;
                }
            }

            if (nextTicket == null)
                return null;

            nextTicket.Status = TicketStatus.Called;
            nextTicket.CounterNumber = counterNumber;

            counter.Status = CounterStatus.Serving;
            counter.CurrentTicketNumber = nextTicket.Number;

            await _hubContext.Clients.All.SendAsync("ReceiveTicketCall", nextTicket.Number, counterNumber);
            await _hubContext.Clients.All.SendAsync("ReceiveCounterUpdate", counter);

            return nextTicket;
        }

        /// <summary>
        /// Complete service for a ticket
        /// </summary>
        public async Task CompleteServiceAsync(string ticketNumber)
        {
            var counter = _counters.FirstOrDefault(c => c.CurrentTicketNumber == ticketNumber);
            if (counter == null)
                throw new ArgumentException("Ticket not found at any counter");

            counter.Status = CounterStatus.Available;
            counter.CurrentTicketNumber = null;

            await _hubContext.Clients.All.SendAsync("ReceiveTicketUpdate", ticketNumber, TicketStatus.Completed);
            await _hubContext.Clients.All.SendAsync("ReceiveCounterUpdate", counter);
        }

        /// <summary>
        /// Generate a unique ticket number
        /// </summary>
        private string GenerateTicketNumber(string prefix)
        {
            return $"{prefix}{DateTime.Now:yyMMdd}{_queues.Values.Sum(q => q.Count):D3}";
        }

        /// <summary>
        /// Calculate estimated waiting time for a service type
        /// </summary>
        private int CalculateEstimatedWaitTime(string serviceType)
        {
            var queueLength = _queues[serviceType].Count;
            var availableCounters = _counters.Count(c => 
                c.ServiceTypes.Contains(serviceType) && 
                c.Status == CounterStatus.Available);

            if (availableCounters == 0)
                availableCounters = 1;

            return (queueLength * _serviceTypes[serviceType].AverageProcessingTime) / availableCounters;
        }

        /// <summary>
        /// Get all active tickets
        /// </summary>
        public IEnumerable<QueueTicket> GetActiveTickets()
        {
            return _queues.Values.SelectMany(q => q);
        }

        /// <summary>
        /// Get all counters
        /// </summary>
        public IEnumerable<Counter> GetCounters()
        {
            return _counters;
        }

        /// <summary>
        /// Get all service types
        /// </summary>
        public IEnumerable<ServiceType> GetServiceTypes()
        {
            return _serviceTypes.Values;
        }
    }
}
