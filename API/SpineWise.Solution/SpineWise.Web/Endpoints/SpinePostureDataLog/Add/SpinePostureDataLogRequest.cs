namespace SpineWise.Web.Endpoints.SpinePostureDataLog.Add
{
    public class SpinePostureDataLogRequest
    {
        public float UpperBackDistance { get; set; }
        public float LegDistance { get; set; }
        public bool PressureSensor1 { get; set; }
        public bool PressureSensor2 { get; set; }
        public bool PressureSensor3 { get; set; }
        public int ChairId { get; set; }
        public string? Key { get; set; }
    }
}
