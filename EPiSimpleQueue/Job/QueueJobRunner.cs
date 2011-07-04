using EPiServer.PlugIn;

namespace EPiSimpleQueue.Job
{
    [ScheduledPlugIn(DisplayName = "Queue processor")]
    public class QueueJobRunner
    {
        public static string Execute()
        {
            var container = Infrastructure.Bootstrapper.Container;

            var queueManager = container.GetInstance<QueueManager>();

            var result = queueManager.ProcessAllQueueItems();

            return string.Format("Processed {0} items, {1} failed.", result.NumberOfItems, result.NumberOfFailedItems);
        }
    }
}
