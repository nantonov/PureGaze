using Microsoft.Extensions.Hosting;

namespace Notification.Infrastructure.Workers;

public abstract class BackgroundWorker(int delayInSeconds)
    : BackgroundService
{
    private readonly SemaphoreSlim _lock = new(1, 1);
    
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(delayInSeconds));
        
        while (!ct.IsCancellationRequested && await timer.WaitForNextTickAsync(ct))
        {
            if (await _lock.WaitAsync(0, ct))
            {
                try
                {
                    await DoWorkAsync(ct);
                }
                finally
                {
                    _lock.Release();
                }
            }
        }
    }
    
    protected abstract Task DoWorkAsync(CancellationToken ct);
}