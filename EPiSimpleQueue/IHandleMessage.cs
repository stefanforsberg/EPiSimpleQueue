namespace EPiSimpleQueue
{
    public interface IHandleMessage<TMessage>
    {
        void Handle(TMessage message);
    }
}
