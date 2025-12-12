using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

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

        double PZMRChtoToTam1,
        double PZMRSmth2,
        int PZMR_ErrorsTotal,
        int PZMR_SuccessfulClicks
        );

    public record PV2ResultRequest(
        string AccessCode,
        long ResultId,

        double PV2_3Smth1,
        double PV2_StdDev_ms,
        int PV2_ErrorsMissed,
        int PV2_ErrorsWrongButton,
        int PV2_ErrorsFalseAlarm
        );

    public record UFPResultRequest(
        string AccessCode,
        long ResultId,

        double UFPSmth1,
        double UFP_StdDev_ms,
        int UFP_MinExposure_ms,
        double UFP_TotalTime_s,
        double UFP_TimeTillMinExp_s,
        int UFP_ErrorsMissed,
        int UFP_ErrorsWrongButton,
        int UFP_ErrorsFalseAlarm
        );
}
