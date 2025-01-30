namespace QueueSystem.Shared.Models
{
    /// <summary>
    /// Represents different types of services available
    /// </summary>
    public class ServiceType
    {
        /// <summary>
        /// Unique identifier for the service type
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Display name of the service
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the service
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Average processing time in minutes
        /// </summary>
        public int AverageProcessingTime { get; set; }

        /// <summary>
        /// Default priority level for this service
        /// </summary>
        public int DefaultPriority { get; set; }

        /// <summary>
        /// Prefix for ticket numbers of this service
        /// </summary>
        public string TicketPrefix { get; set; }

        /// <summary>
        /// Whether this service is currently available
        /// </summary>
        public bool IsActive { get; set; }
    }
}
