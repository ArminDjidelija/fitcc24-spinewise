namespace SpineWise.Web.Endpoints.SpinePostureDataLog.SpecificDayData
{
    public class GetSpecificDayDataResponse
    {
        public int SittingHours { get; set; }
        public int SittingMinutes { get; set; }
        public float BadPercentage { get; set; }
        public float GoodPercentage { get; set; }
        public float s1Percentage { get; set; }
        public float s2Percentage { get; set; }
        public float s3Percentage { get; set; }
        public float s4Percentage { get; set; }
        public DateTime Datum { get; set; }
    }
}
