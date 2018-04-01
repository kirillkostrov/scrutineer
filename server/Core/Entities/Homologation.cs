namespace Core.Entities
{
    public class Homologation : BaseEntity
    {
        public string Name { get; set; }
        
        public int Uid { get; set; }
        
        public string Code { get; set; }
        
        public string Description { get; set; }

        public string Manufacturer { get; set; }

        public int StandartId { get; set; }

        public int[] IncompatibleHomologationsIds { get; set; }

        public HomologationItem[] HomologationItems { get; set; }
    }
}