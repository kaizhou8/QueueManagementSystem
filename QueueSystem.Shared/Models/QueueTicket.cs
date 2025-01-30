using System;

namespace QueueSystem.Shared.Models
{
    /// <summary>
    /// Represents a queue ticket in the system
    /// </summary>
    public class QueueTicket
    {
        /// <summary>
        /// Unique ticket number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Type of service requested
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Priority level of the ticket
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Time when the ticket was created
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Current status of the ticket
        /// </summary>
        public TicketStatus Status { get; set; }

        /// <summary>
        /// Counter number where the ticket holder should go
        /// </summary>
        public int? CounterNumber { get; set; }

        /// <summary>
        /// Estimated waiting time in minutes
        /// </summary>
        public int EstimatedWaitTime { get; set; }
    }

    /// <summary>
    /// Represents the status of a queue ticket
    /// </summary>
    public enum TicketStatus
    {
        /// <summary>
        /// Ticket has been created and is waiting
        /// </summary>
        Waiting,

        /// <summary>
        /// Ticket has been called
        /// </summary>
        Called,

        /// <summary>
        /// Ticket holder is being served
        /// </summary>
        Serving,

        /// <summary>
        /// Service has been completed
        /// </summary>
        Completed,

        /// <summary>
        /// Ticket holder did not show up
        /// </summary>
        NoShow,

        /// <summary>
        /// Ticket has been cancelled
        /// </summary>
        Cancelled
    }
}
