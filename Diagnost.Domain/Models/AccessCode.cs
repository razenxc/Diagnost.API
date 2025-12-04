using System;

namespace Diagnost.Domain.Models
{
    public enum TestType
    {
        PZMR = 0,
        RV2_3 = 1,
        UFP = 2
    }

    public class AccessCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public AccessCode(Guid id, string code, bool isActive, DateTime createdAt)
        {
            Id = id;
            Code = code;
            IsActive = isActive;
            CreatedAt = createdAt;
        }
    }
}
