using System;
using System.Linq;
using EPiServer.Data.Dynamic;
using EPiSimpleQueue.Test.Helpers;
using NUnit.Framework;

namespace EPiSimpleQueue.Test.Infrastructure
{
    public class with_a_queue_with_two_different_messages_where_one_message_handler_causes_an_error
    {
        QueueManager _queueManager;
        AnEventThatWillThrow _eventThatWillThrow;
        ProccessResults _results;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _eventThatWillThrow = new AnEventThatWillThrow {Exception = new ArgumentException("Some exception")};

            var dynamicDataFactory = new Moq.Mock<DynamicDataStoreFactory>();
            dynamicDataFactory
                .Setup(x => x.GetStore(typeof(QueueItem)))
                .Returns(new InMemoryDynamicDataStore());

            dynamicDataFactory
                .Setup(x => x.GetStore(typeof(ErrorQueueItem)))
                .Returns(new InMemoryDynamicDataStore());

            var container = EPiSimpleQueue.Infrastructure.Bootstrapper.CreateContainer();

            _queueManager = new QueueManager(dynamicDataFactory.Object, new Handler(container));

            _queueManager.Add(_eventThatWillThrow);
            _queueManager.Add(new AnEvent());

            // Act
            _results = _queueManager.ProcessAllQueueItems();
        }

        [Test]
        public void should_remove_all_the_message_from_the_queue()
        {
            Assert.That(_queueManager.GetQueueItems(), Is.Empty);
        }

        [Test]
        public void should_add_the_failed_message_to_the_error_queue()
        {
            Assert.That(_queueManager.GetErrorQueueItems().Count, Is.EqualTo(1));
        }

        [Test]
        public void should_add_the_exception_thrown_to_the_message_in_the_error_queue()
        {
            Assert.That(_queueManager.GetErrorQueueItems().First().ExceptionMessage, Is.EqualTo(_eventThatWillThrow.Exception.ToString()));
        }

        [Test]
        public void should_the_proceess_result_indicate_that_two_items_were_processed()
        {
            Assert.That(_results.NumberOfItems, Is.EqualTo(2));
        }

        [Test]
        public void should_the_proceess_result_indicate_that_one_item_failed_to_be_proccessed()
        {
            Assert.That(_results.NumberOfFailedItems, Is.EqualTo(1));
        }
    }

    public class when_a_message_is_processed_without_errors
    {
        [Test]
        public void should_remove_it_from_the_queue()
        {
            // Arrange
            var dynamicDataFactory = new Moq.Mock<DynamicDataStoreFactory>();
            dynamicDataFactory
                .Setup(x => x.GetStore(typeof (QueueItem)))
                .Returns(new InMemoryDynamicDataStore());

            var container = EPiSimpleQueue.Infrastructure.Bootstrapper.CreateContainer();

            var queueManager = new QueueManager(dynamicDataFactory.Object, new Handler(container));

            queueManager.Add(new AnEvent());

            // Act
            queueManager.ProcessAllQueueItems();

            // Assert
            Assert.That(queueManager.GetQueueItems(), Is.Empty);
        }
    }

    public class when_a_queue_with_3_messages_is_emptied
    {
        [Test]
        public void should_remove_it_from_the_queue()
        {
            // Arrange
            var dynamicDataFactory = new Moq.Mock<DynamicDataStoreFactory>();
            dynamicDataFactory
                .Setup(x => x.GetStore(typeof(QueueItem)))
                .Returns(new InMemoryDynamicDataStore());

            var container = EPiSimpleQueue.Infrastructure.Bootstrapper.CreateContainer();

            var queueManager = new QueueManager(dynamicDataFactory.Object, new Handler(container));

            queueManager.Add(new AnEvent());
            queueManager.Add(new AnEvent());
            queueManager.Add(new AnEvent());

            // Act
            queueManager.RemoveAllQueueItems();

            // Assert
            Assert.That(queueManager.GetQueueItems(), Is.Empty);
        }
    }
}