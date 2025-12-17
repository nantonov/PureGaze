using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.Application.Configurations;
using Common.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Notification.Infrastructure.Workers;

public class HighPriorityWorker(
    IServiceScopeFactory scopeFactory,
    ILogger<HighPriorityWorker> logger,
    IOptions<RetryPolicyOptions> options)
    : BaseEmailWorker(scopeFactory, options, TimeSpan.FromMinutes(5), EmailPriority.High, logger);
