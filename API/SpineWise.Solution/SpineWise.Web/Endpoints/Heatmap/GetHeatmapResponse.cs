namespace SpineWise.Web.Endpoints.Heatmap
{
    public class GetHeatmapResponse
    {
        public int ChairId { get; set; }
        public DateTime Datum { get; set; }
        public int BrojZapisa { get; set; }
        public float s1Percentage{ get; set; }
        public float s2Percentage{ get; set; }
        public float s3Percentage { get; set; }
        //public float s4Percentage { get; set; }
        //public float s5Percentage { get; set; }
    }
}
