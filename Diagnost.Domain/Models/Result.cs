using System;

namespace Diagnost.Domain.Models
{
    public enum ResultType
    {
        PZMR,
        PV2_3,
        UFP
    }

    public class Result // Change the model later
    {
        public long Id { get; set; } // Using long to simplify user interactions.

        public Guid AccessCodeId { get; set; }
        public AccessCode? AccessCode { get; set; }

        // <--- User Info--->
        public string StudentFullName { get; set; } = string.Empty;
        public string SportType { get; set; } = string.Empty;
        public string SportQualification { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        
        // --- ТЕСТ 1 (ПЗМР) ---
        public double PZMRLatet { get; set; }      // Середня латентність
        public double PZMRvidhil { get; set; }     // Стандартне відхилення
        public int PZMR_ErrorsTotal { get; set; } // Загальна кількість помилок (пропуск/випередження)

        // --- ТЕСТ 2 (ПВ-2-3) ---
        public double PV2_3Latet { get; set; }     // Середня латентність вибору
        public double PV2_StdDev_ms { get; set; }  // Відхилення
        public double PV2_ErrorsTotal { get; set; }
        public int PV2_ErrorsMissed { get; set; }
        public int PV2_ErrorsWrongButton { get; set; }
        public int PV2_ErrorsFalseAlarm { get; set; }

        // --- ТЕСТ 3 (УФП - Універсальна Функціональна Проба) ---
        public double UFPLatet { get; set; }       // Середня латентність
        public double UFP_StdDev_ms { get; set; }  // Відхилення
        public double UFP_ErrorsTotal { get; set; }
    }
}
