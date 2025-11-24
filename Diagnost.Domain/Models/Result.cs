using System;

namespace Diagnost.Domain.Models
{
    public class Result // Change the model later
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        
        // Results
        public int SuccessfullClicks { get; set; }
        public int Errors { get; set; }
        public double AverageLatency { get; set; }
        public double StandardDeviation { get; set; }
        public DateTime SubmittedAt { get; set; }

        // Navigation property
        public Session? Session { get; set; }
    }
}
