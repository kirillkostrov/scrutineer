using System;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;

using Core.Entities;
using Core.Helpers;
using Core.Interfaces;

namespace Services
{
    public class CheckerService : ICheckerService
    {
        private readonly IHomologationRepository _homologationRepository;
        private readonly IStandartRepository _standartRepository;

        public CheckerService(
            IStandartRepository standartRepository,
            IHomologationRepository homologationRepository)
        {
            _standartRepository = standartRepository;
            _homologationRepository = homologationRepository;
        }

        public async Task<CheckResult> Check(string rawRecognozedString)
        {
            var isCorrectString = await GetParseResulst(rawRecognozedString);
            return isCorrectString.Item1 != null
                ? new CheckResult{ResultCode = ResultCode.ExpiresSoon} 
                : new CheckResult{ResultCode = ResultCode.Success};
        }

        public async Task<CheckResult> Check(string standartCode, string homologationCode, string timeZone)
        {
            var homologation = await _homologationRepository.GetByCode(homologationCode);

            var standart = await (homologation != null
                ? _standartRepository.GetByCode(homologation.StandartId.ToString())
                : _standartRepository.GetByCode(standartCode));

            if (standart == null)
            {
                return FailedCheck();
            }

            var timeZoneInfo = TimeZoneInfo.FromSerializedString(timeZone);
            if(timeZoneInfo == null)
            {
                timeZoneInfo = TimeZoneInfo.Utc;
            }

            var dateTimeNowClient = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);

            if(standart != null && (standart.EndDate <= dateTimeNowClient || standart.StartDate > dateTimeNowClient))
            {
                return FailedCheck();
            }

            if(homologation != null && homologation.HomologationItems.Any(x => x.EndDate <= dateTimeNowClient || x.StartDate > dateTimeNowClient))
            {
                return WarningCheck(homologation, standart);
            }

            return SuccessCheck(homologation, standart);
        }

        private async Task<Tuple<Standart, Homologation>> GetParseResulst(string rawRecognozedString)
        {
            CodeParser.TryParseStandartCode(rawRecognozedString, out var standartCode);
            CodeParser.ParseHomologationCode(rawRecognozedString, out var homologationCode);
            
            return Tuple.Create<Standart, Homologation>(
                    await _standartRepository.GetByCode(standartCode),
                    await _homologationRepository.GetByCode(homologationCode)
            );
        }

        private static CheckResult SuccessCheck(Homologation homologation, Standart standart)
        {
            return new CheckResult
            {
                ResultCode = ResultCode.Success,
                CheckTime = DateTime.Now,
                Homologation = homologation,
                Standart = standart,
                InternalId = ObjectId.GenerateNewId(),
                SessionId = Guid.NewGuid()
            };
        }

        private static CheckResult FailedCheck() => new CheckResult
        {
            ResultCode = ResultCode.Fail,
            CheckTime = DateTime.Now,
            InternalId = ObjectId.GenerateNewId(),
            SessionId = Guid.NewGuid(),
        };

        private static CheckResult WarningCheck(Homologation homologation, Standart standart)
        {
            return new CheckResult
            {
                ResultCode = ResultCode.ExpiresSoon,
                CheckTime = DateTime.Now,
                Homologation = homologation,
                Standart = standart,
                InternalId = ObjectId.GenerateNewId(),
                SessionId = Guid.NewGuid()
            };
        }
    }
}