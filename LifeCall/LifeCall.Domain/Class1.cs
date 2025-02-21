namespace LifeCall.Domain
{
    public class EmergencyReport
    {
        public int Id { get; set; }
        public string CallerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string EmergencyType { get; set; }
        public DateTime ReportedAt { get; set; }


    }
}