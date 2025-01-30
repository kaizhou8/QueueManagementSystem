using System.Collections.Generic;

namespace QueueSystem.Shared.Models
{
    /// <summary>
    /// Represents a service counter in the system
    /// </summary>
    public class Counter
    {
        /// <summary>
        /// Counter number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Name or description of the counter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current status of the counter
        /// </summary>
        public CounterStatus Status { get; set; }

        /// <summary>
        /// List of service types that this counter can handle
        /// </summary>
        public List<string> ServiceTypes { get; set; }

        /// <summary>
        /// Currently serving ticket number
        /// </summary>
        public string CurrentTicketNumber { get; set; }

        /// <summary>
        /// Staff member currently operating the counter
        /// </summary>
        public string CurrentOperator { get; set; }
    }

    /// <summary>
    /// Represents the status of a counter
    /// </summary>
    public enum CounterStatus
    {
        /// <summary>
        /// Counter is available for service
        /// </summary>
        Available,

        /// <summary>
        /// Counter is currently serving a customer
        /// </summary>
        Serving,

        /// <summary>
        /// Counter is temporarily closed
        /// </summary>
        Closed,

        /// <summary>
        /// Counter is on break
        /// </summary>
        Break
    }
}
