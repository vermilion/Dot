using Microsoft.Extensions.Hosting;
using PlatformFramework.Abstractions;
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
        private readonly IClockProvider _clockProvider;

        public JwtRefreshTokenCacheService(IJwtAuthService jwtAuthManager, IClockProvider clockProvider)
        {
            _jwtAuthManager = jwtAuthManager;
            _clockProvider = clockProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // remove expired refresh tokens from cache every minute
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _jwtAuthManager.RemoveExpiredRefreshTokens(_clockProvider.Now);
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
