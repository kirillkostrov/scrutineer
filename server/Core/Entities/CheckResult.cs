using System;

namespace Core.Entities
{
    public class CheckResult : BaseEntity
    {
        public ResultCode ResultCode { get; set; }

        public Standart Standart { get; set; }

        public Homologation Homologation { get; set; }

        public DateTime CheckTime { get; set; }

        // TODO: [IS] check that this is works correctly in MongoDB
        public Guid SessionId { get; set; }
    }
}