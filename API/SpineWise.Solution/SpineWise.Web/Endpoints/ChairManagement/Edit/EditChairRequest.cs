namespace SpineWise.Web.Endpoints.ChairManagement.Edit
{
    public class EditChairRequest
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int ChairModelId { get; set; }
        public string Naziv { get; set; }
    }
}
