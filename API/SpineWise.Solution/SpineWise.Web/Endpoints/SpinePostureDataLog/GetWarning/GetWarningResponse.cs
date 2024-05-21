namespace SpineWise.Web.Endpoints.SpinePostureDataLog.GetWarning
{
    public class GetWarningResponse
    {
        public int BadCount { get; set; }
        public int GoodCount { get; set; }
        public float GoodBadRatio { get; set; }
        public float BadGoodRatio { get; set; }


        public int BadCount5 { get; set; }
        public int GoodCount5 { get; set; }
        public float GoodBadRatio5 { get; set; }
        public float BadGoodRatio5 { get; set; }

    }
}
