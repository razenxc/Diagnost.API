using System;

namespace Diagnost.Domain.Models
{
    public enum TestType
    {
        PZMR = 0,
        RV2_3 = 1,
        UFP = 2
    }

    public class Session
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public string Code { get; set; } = string.Empty;
        public TestType TestType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public Teacher? Teacher { get; set; }
    }
}
