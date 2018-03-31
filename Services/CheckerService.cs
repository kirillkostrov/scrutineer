using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MongoDB.Bson;

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

        public Task<CheckResult> Check(string rawRecognozedString)
        {
            throw new NotImplementedException();
        }

        public async Task<CheckResult> Check(string standartCode, string homologationCode)
        {
            var standart = await _standartRepository.GetByCode(standartCode);
            var homologation = await _homologationRepository.GetByCode(homologationCode);
            
            if (homologation != null)
            {
                return CheckHomologation(homologation);
            } else if (standart != null)
            {
                return CheckStandart(standart);
            }
            
            return FailedCheck();
        }

        private CheckResult CheckStandart(Standart standart)
        {
            var checkTime = DateTime.Now;

            if (!(standart.StartDate < checkTime
                && standart.EndDate > checkTime))
            {
                return FailedCheck();
            }
            
            return SuccessCheck(null, standart, checkTime);
        }

        private CheckResult CheckHomologation(Homologation homologation)
        {
            var checkTime = DateTime.Now;

            return FailedCheck();
        }

        private static CheckResult SuccessCheck(
            Homologation homologation, 
            Standart standart,
            DateTime checkTime)
        {
            return new CheckResult
            {
                ResultCode = ResultCode.Success,
                CheckTime = checkTime,
                Homologation = homologation,
                Standart = standart,
                InternalId = ObjectId.GenerateNewId(),
                SessionId = Guid.NewGuid()
            };
        }

        private static CheckResult FailedCheck()
        {
            return new CheckResult
            {
                ResultCode = ResultCode.Fail,
                CheckTime = DateTime.Now,
                InternalId = ObjectId.GenerateNewId(),
                SessionId = Guid.NewGuid()
            };
        }
    }
}