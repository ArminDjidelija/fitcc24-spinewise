namespace SpineWise.Web.Endpoints.SpinePostureDataLog.GetGoodBadRatio
{
    public class GetGoodBadRatioResponse
    {
        public DateTime Date { get; set; }
        public int CountGood { get; set; }
        public int CountBad { get; set; }
        public float RatioGood { get; set; }
        public float RatioBad { get; set; }
    }
}
