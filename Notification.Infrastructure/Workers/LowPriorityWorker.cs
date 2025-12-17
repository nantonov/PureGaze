using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.Application.Configurations;
using Common.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Notification.Infrastructure.Workers;

public class LowPriorityWorker(
    IServiceScopeFactory scopeFactory,
    ILogger<LowPriorityWorker> logger,
    IOptions<RetryPolicyOptions> options)
    : BaseEmailWorker(scopeFactory, options, TimeSpan.FromMinutes(20), EmailPriority.Low, logger);
