using System;

namespace Core.Entities
{
    public class Standart : BaseEntity
    {
        public string Name { get; set; }
        
        public int Id { get; set; }
        
        public string Code { get; set; }

        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}