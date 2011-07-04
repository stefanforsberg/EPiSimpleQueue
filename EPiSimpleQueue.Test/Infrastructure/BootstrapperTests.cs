using System.Text;
using EPiSimpleQueue.Test.Helpers;
using NUnit.Framework;

namespace EPiSimpleQueue.Test.Infrastructure
{
    public class when_requesting_a_message_handler
    {
        [Test]
        public void should_return_the_type_that_implements_the_message_handler()
        {
            // Arrange
            var container = EPiSimpleQueue.Infrastructure.Bootstrapper.CreateContainer();

            // Act
            var handler = container.GetInstance<IHandleMessage<AnEvent>>();

            // Assert
            Assert.That(handler, Is.InstanceOf<AnEventHandler>());
        }
    }
}
    
