using Microsoft.Extensions.Hosting;
using PlatformFramework.EFCore.Identity.Abstrations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Services
{
    public class JwtRefreshTokenCacheService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IJwtAuthService _jwtAuthManager;

        public JwtRefreshTokenCacheService(IJwtAuthService jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // remove expired refresh tokens from cache every minute
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _jwtAuthManager.RemoveExpiredRefreshTokens(DateTime.Now);
        }

        public Task StopAsync(CancellationToken stoppingToken)
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
