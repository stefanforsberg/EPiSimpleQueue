using System;
using System.Collections.Generic;
using EPiServer.Data.Dynamic;

namespace EPiSimpleQueue
{
    public class ProccessResults
    {
        public int NumberOfItems { get; set; }
        public int NumberOfFailedItems { get; set; }
    }

    public class QueueManager
    {
        readonly DynamicDataStoreFactory _dynamicDataStoreFactory;
        readonly IHandler _handler;
        readonly DynamicDataStore _queueStore;
        readonly DynamicDataStore _errorQueueStore;

        public QueueManager(DynamicDataStoreFactory dynamicDataStoreFactory, IHandler handler)
        {
            if (dynamicDataStoreFactory == null) throw new ArgumentNullException("dynamicDataStoreFactory");
            if (handler == null) throw new ArgumentNullException("handler");

            _dynamicDataStoreFactory = dynamicDataStoreFactory;
            _handler = handler;

            _queueStore = _dynamicDataStoreFactory
                .GetStore(typeof (QueueItem));

            _errorQueueStore = _dynamicDataStoreFactory
                .GetStore(typeof(ErrorQueueItem));


        }

        public static QueueManager Instance
        {
            get { return Infrastructure.Bootstrapper.Container.GetInstance<QueueManager>(); }
        }

        public void Add(MessageBase message)
        {
            var queueItem = new QueueItem
                                {
                                    Message = message
                                };

            _queueStore.Save(queueItem);
        }

        public ProccessResults ProcessAllQueueItems()
        {
            var result = new ProccessResults();
            var allQueueItems = GetQueueItems();

            result.NumberOfItems = allQueueItems.Count;

            foreach (var queueItem in allQueueItems)
            {
                try
                {
                    _handler.Execute(queueItem.Message);
                }
                catch (Exception ex)
                {
                    result.NumberOfFailedItems++;

                    var exceptionThrown = ex;
                    if(ex.InnerException != null)
                    {
                        exceptionThrown = ex.InnerException;
                    }

                    _errorQueueStore.Save(ErrorQueueItem.FromQueueItem(queueItem.Message, exceptionThrown));
                }
                finally
                {
                    _queueStore.Delete(queueItem.Id);    
                }
            }

            return result;
        }

        public List<QueueItem> GetQueueItems()
        {
            return new List<QueueItem>(_queueStore.LoadAll<QueueItem>());
        }

        public List<ErrorQueueItem> GetErrorQueueItems()
        {
            return new List<ErrorQueueItem>(_errorQueueStore.LoadAll<ErrorQueueItem>());
        }

        public void RemoveAllQueueItems()
        {
            _queueStore
                .DeleteAll();
        }

        public void RemoveAllErrorQueueItems()
        {
            _errorQueueStore
                .DeleteAll();
        }
    }
}