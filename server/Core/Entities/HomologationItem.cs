using System;

namespace Core.Entities
{
    public class HomologationItem : BaseEntity
    {
        public int Uid { get; set; }

        public byte[] Image { get; set; }

        public Size[] Sizes { get; set; }

        public string ModelName { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}