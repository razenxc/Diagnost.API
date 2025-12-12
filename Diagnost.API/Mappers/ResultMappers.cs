using Diagnost.API.Contracts;
using Diagnost.Domain.Models;

namespace Diagnost.API.Mappers
{
    public static class ResultMappers
    {
        public static Result ToResult(this ResultRequest r)
        {
            return new Result
            {
                StudentFullName = r.StudentFullName,
                SportType = r.SportType,
                SportQualification = r.SportQualification,
                Group = r.Group,
                Gender = r.Gender,
            };
        }

        public static Result ToPZMRREsult(this PZMRResultRequest r)
        {
            return new Result
            {
                PZMRChtoToTam1 = r.PZMRChtoToTam1,
                PZMRSmth2 = r.PZMRSmth2,
                PZMR_ErrorsTotal = r.PZMR_ErrorsTotal,
                PZMR_SuccessfulClicks = r.PZMR_SuccessfulClicks,
            };
        }

        public static Result ToPV2Result(this PV2ResultRequest r)
        {
            return new Result
            {
                PV2_3Smth1 = r.PV2_3Smth1,
                PV2_StdDev_ms = r.PV2_StdDev_ms,
                PV2_ErrorsMissed = r.PV2_ErrorsMissed,
                PV2_ErrorsWrongButton = r.PV2_ErrorsWrongButton,
                PV2_ErrorsFalseAlarm = r.PV2_ErrorsFalseAlarm,
            };
        }

        public static Result ToUFPResult(this UFPResultRequest r)
        {
            return new Result
            {
                UFPSmth1 = r.UFPSmth1,
                UFP_StdDev_ms = r.UFP_StdDev_ms,
                UFP_MinExposure_ms = r.UFP_MinExposure_ms,
                UFP_TotalTime_s = r.UFP_TotalTime_s,
                UFP_TimeTillMinExp_s = r.UFP_TimeTillMinExp_s,
                UFP_ErrorsMissed = r.UFP_ErrorsMissed,
                UFP_ErrorsWrongButton = r.UFP_ErrorsWrongButton,
                UFP_ErrorsFalseAlarm = r.UFP_ErrorsFalseAlarm,
            };
        }
    }
}
