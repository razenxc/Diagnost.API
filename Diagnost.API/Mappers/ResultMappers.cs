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
                PZMRLatet = r.PZMRLatet,
                PZMRvidhil = r.PZMRvidhil,
                PZMR_ErrorsTotal = r.PZMR_ErrorsTotal
            };
        }

        public static Result ToPV2Result(this PV2ResultRequest r)
        {
            return new Result
            {
                PV2_3Latet = r.PV2_3Latet,
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
                UFPLatet = r.UFPLatet,
                UFP_StdDev_ms = r.UFP_StdDev_ms,
                UFP_ErrorsTotal = r.UFP_ErrorsTotal,
            };
        }

        public static ResultResponse ToResultResponse(this Result r, string accessCode)
        {
            return new ResultResponse(
                r.Id,
                accessCode,
                r.StudentFullName,
                r.SportType,
                r.SportQualification,
                r.Group,
                r.Gender,
                r.SubmittedAt,
                r.PZMRLatet,
                r.PZMRvidhil,
                r.PZMR_ErrorsTotal,
                r.PV2_3Latet,
                r.PV2_StdDev_ms,
                r.PV2_ErrorsMissed,
                r.PV2_ErrorsWrongButton,
                r.PV2_ErrorsFalseAlarm,
                r.UFPLatet,
                r.UFP_StdDev_ms,
                r.UFP_ErrorsTotal
                );
        }
    }
}
