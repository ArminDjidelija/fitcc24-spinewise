using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace SpineWise.Web.Endpoints.SpinePostureDataLog.ExportExcel
{
    public class ExcelEksport:ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ExcelEksport(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }
        [NonAction]
        [HttpGet("podaci")]
        public async Task<ActionResult<NoResponse>> Action([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var downloadFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            var fileNameBase = $"Podaci {DateTime.Now.ToString("dd.MM.yyyy. HH-mm")}.csv";
            var filePath = Path.Combine(downloadFolderPath, fileNameBase);

            var fileStreamOption = new FileStreamOptions()
            {
                Access = FileAccess.Write,
                Mode = FileMode.CreateNew // Use CreateNew mode to ensure file creation fails if the file already exists
            };

            int counter = 1;


            var podaci = await _applicationDbContext.SpinePostureDataLogs.ToListAsync(cancellationToken);
            // At this point, filePath will be a unique name that doesn't already exist in the Downloads folder
            using (var sw = new StreamWriter(filePath, fileStreamOption))
            {
                sw.WriteLine("UpperBackDistance, PressureSensor1, PressureSensor2, PressureSensor3, PressureSensor4, Good");
                double prethodna = 0;
                foreach (var obj in podaci)
                {
                    sw.WriteLine($"{obj.UpperBackDistance}, {obj.PressureSensor1}, {obj.PressureSensor2}, {obj.PressureSensor3}, {obj.PressureSensor4}, {obj.Good}");
                }
                sw.Close();
            }

            return Ok();
        }
    }
}
