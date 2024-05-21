using System.Net;

namespace SpineWise.Web.Services.KeepAlive
{
    public class KeepAliveService : IHostedService, IDisposable
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://backend.p2307.app.fit.ba/proizvodPopusti?Page=1&TableSize=4&SortID=2");
                //WebRequest req = WebRequest.Create("https://backend.spinewise.p2361.app.fit.ba/sensorlogs/count");
                req.GetResponse();
            }
            catch (Exception ex)
            {
                // Log exception
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
