using SpineWise.ClassLibrary.Models;

namespace SpineWise.Web.Endpoints.ChairManagement.GetByUser
{
    public class GetChairByUserResponse
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DateOfCreating { get; set; }
        public string ChairModelName { get; set; }
        public string Naziv { get; set; }
        public int Delay { get; set; }
        public bool SaljiPodatke { get; set; }
    }
}
