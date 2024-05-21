using SpineWise.ClassLibrary.Models;

namespace SpineWise.Web.Endpoints.ChairManagement.GetById
{
    public class GetChairByIdResponse
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DateOfCreating { get; set; }
        public ChairModel ChairModel { get; set; }
    }
}
