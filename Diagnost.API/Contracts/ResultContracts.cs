namespace Diagnost.API.Contracts
{
    public record ResultRequest(
        string AccessCode,
        string StudentFullName,
        string SportType,
        string SportQualification,
        string Group,
        string Gender
        );

    public record PZMRResultRequest(
        string AccessCode,
        long ResultId,

        double PZMRLatet,
        double PZMRvidhil,
        int PZMR_ErrorsTotal
        );

    public record PV2ResultRequest(
        string AccessCode,
        long ResultId,

        double PV2_3Latet,
        double PV2_StdDev_ms,
        int PV2_ErrorsMissed,
        int PV2_ErrorsWrongButton,
        int PV2_ErrorsFalseAlarm
        );

    public record UFPResultRequest(
        string AccessCode,
        long ResultId,

        double UFPLatet,
        double UFP_StdDev_ms,
        double UFP_ErrorsTotal
        );

    public record ResultResponse(
        long Id,
        string AccessCode,
        string StudentFullName,
        string SportType,
        string SportQualification,
        string Group,
        string Gender,
        DateTime SubmittedAt,

        double PZMRLatet,
        double PZMRvidhil,
        int PZMR_ErrorsTotal,

        double PV2_3Latet,
        double PV2_StdDev_ms,
        int PV2_ErrorsMissed,
        int PV2_ErrorsWrongButton,
        int PV2_ErrorsFalseAlarm,

        double UFPLatet,
        double UFP_StdDev_ms,
        double UFP_ErrorsTotal
        );
}
