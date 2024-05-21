namespace SpineWise.Web.Endpoints.SpineWiseDataLog.Add
{
    public class SpineWiseDataLogRequest
    {
        public float LegDistance { get; set; }
        public float LumbarBackDistance { get; set; } //lower back
        public float ThoracicBackDistance { get; set; } //upper back
        public int ChairId { get; set; }
        public string Key { get; set; }
        public DateTime DateTimeOfLog { get; set; }
    }
}
