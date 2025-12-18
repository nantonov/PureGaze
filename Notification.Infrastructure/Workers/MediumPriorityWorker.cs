using Common.Data.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.Application.Configurations;
using Microsoft.Extensions.Logging;

namespace Notification.Infrastructure.Workers;

public class MediumPriorityWorker(
    IServiceScopeFactory scopeFactory,
    ILogger<MediumPriorityWorker> logger,
    IOptions<RetryPolicyOptions> options)
    : BaseEmailWorker(scopeFactory, options, TimeSpan.FromMinutes(10), EmailPriority.Normal, logger);
