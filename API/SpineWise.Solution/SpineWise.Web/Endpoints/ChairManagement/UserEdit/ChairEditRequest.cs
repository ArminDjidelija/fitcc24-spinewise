namespace SpineWise.Web.Endpoints.ChairManagement.UserEdit
{
    public class ChairEditRequest
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Delay { get; set; }
        public bool SaljiPodatke { get; set; }
    }
}
