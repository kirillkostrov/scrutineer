﻿using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
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

        public async Task<CheckResult> Check(string rawRecognozedString)
        {
            var isCorrectString = await GetParseResulst(rawRecognozedString);
            return isCorrectString.Item1 != null
                ? new CheckResult{ResultCode = ResultCode.ExpiresSoon} 
                : new CheckResult{ResultCode = ResultCode.Success};
        }

        public async Task<CheckResult> Check(string standartCode, string homologationCode)
        {
            var standart = await _standartRepository.GetByCode(standartCode);
            var homologation = await _homologationRepository.GetByCode(homologationCode);
            if (standart == null && homologation == null) return FailedCheck();

            return SuccessCheck(homologation, standart);
        }

        private async Task<Tuple<Standart, Homologation>> GetParseResulst(string rawRecognozedString)
        {
            CodeParser.TryParseStandartCode(rawRecognozedString, out var standartCode);
            CodeParser.TryParseStandartCode(rawRecognozedString, out var homologationCode);
            
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