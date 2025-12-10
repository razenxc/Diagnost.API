using System;

namespace Diagnost.Domain.Models
{
    public class Result // Change the model later
    {
        public long Id { get; set; } // Using long to simplify user interactions.

        public Guid AccessCodeId { get; set; }
        public AccessCode? AccessCode { get; set; }

        // User Info
        public string StudentFullName { get; set; } = string.Empty;
        public string SportType { get; set; } = string.Empty;
        public string SportQualification { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }

        // Results
        // --- TEST 1 (ПЗМР) ---
        public double PZMRChtoToTam1 { get; set; }
        public double PZMRSmth2 { get; set; }
        public int PZMR_ErrorsTotal { get; set; }
        public int PZMR_SuccessfulClicks { get; set; }

        // --- Test 2 (ПВ-2-3) ---
        public double PV2_3Smth1 { get; set; }
        public double PV2_StdDev_ms { get; set; }
        public int PV2_ErrorsMissed { get; set; }
        public int PV2_ErrorsWrongButton { get; set; }
        public int PV2_ErrorsFalseAlarm { get; set; }

        // --- Test 3 (УФП - Універсальна Функціональна Проба) ---
        public double UFPSmth1 { get; set; }       // Середня латентність
        public double UFP_StdDev_ms { get; set; }  // Відхилення

        // Додаткові поля, специфічні для тесту 3 (динаміка)
        public int UFP_MinExposure_ms { get; set; }    // Мінімальний досягнутий інтервал
        public double UFP_TotalTime_s { get; set; }    // Загальний час тесту
        public double UFP_TimeTillMinExp_s { get; set; } // Час виходу на плато

        public int UFP_ErrorsMissed { get; set; }
        public int UFP_ErrorsWrongButton { get; set; }
        public int UFP_ErrorsFalseAlarm { get; set; }
    }
}
