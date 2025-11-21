using System;

namespace Diagnost.Domain.Models
{
    public class Result // Change the model later
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public double AverageTime { get; set; }
        public int ErrorsCount { get; set; }
        public string RawDataJson { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }

        // Navigation property
        public Session? Session { get; set; }
    }
}
