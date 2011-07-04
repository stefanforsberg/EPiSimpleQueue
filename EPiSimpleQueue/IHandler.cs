namespace EPiSimpleQueue
{
    public interface IHandler
    {
        void Execute(object message);
    }
}