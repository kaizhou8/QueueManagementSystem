using Microsoft.AspNetCore.SignalR;
using QueueSystem.Shared.Models;

namespace QueueSystem.Server.Hubs
{
    /// <summary>
    /// SignalR hub for real-time queue updates
    /// </summary>
    public class QueueHub : Hub
    {
        /// <summary>
        /// Called when a new ticket is created
        /// </summary>
        public async Task NewTicket(QueueTicket ticket)
        {
            await Clients.All.SendAsync("ReceiveNewTicket", ticket);
        }

        /// <summary>
        /// Called when a ticket status is updated
        /// </summary>
        public async Task UpdateTicketStatus(string ticketNumber, TicketStatus status)
        {
            await Clients.All.SendAsync("ReceiveTicketUpdate", ticketNumber, status);
        }

        /// <summary>
        /// Called when a counter status changes
        /// </summary>
        public async Task UpdateCounterStatus(Counter counter)
        {
            await Clients.All.SendAsync("ReceiveCounterUpdate", counter);
        }

        /// <summary>
        /// Called when a ticket is called to a counter
        /// </summary>
        public async Task CallTicket(string ticketNumber, int counterNumber)
        {
            await Clients.All.SendAsync("ReceiveTicketCall", ticketNumber, counterNumber);
        }

        /// <summary>
        /// Join a specific service type group to receive updates
        /// </summary>
        public async Task JoinServiceGroup(string serviceType)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, serviceType);
        }

        /// <summary>
        /// Leave a service type group
        /// </summary>
        public async Task LeaveServiceGroup(string serviceType)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, serviceType);
        }
    }
}
