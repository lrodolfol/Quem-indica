using API.Configuration;
using API.Middleware;

namespace API.Services;

public sealed class TasksServices(IServiceScopeFactory factory) : TaskScheduler
{
    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    while (!stoppingToken.IsCancellationRequested)
    //    {
    //        await SetOverdueStatusAsync();
    //    }
    //}

    //private async Task SetOverdueStatusAsync()
    //{


    //    using var scope = factory.CreateScope();
    //    var partnership = scope.ServiceProvider.GetRequiredService<PartnershipsMid>();

    //    await partnership.SetOverdueStatusAsync(ConfigFromAppSettings.SectionConfig.DaysForOverdue);
    //}
    protected override IEnumerable<Task>? GetScheduledTasks()
    {
        throw new NotImplementedException();
    }

    protected override void QueueTask(Task task)
    {
        throw new NotImplementedException();
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        throw new NotImplementedException();
    }
}
