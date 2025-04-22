namespace Core.Interfaces;

public interface IPrototype<out T> where T : IPrototype<T>
{
    public T DeepCopy();
}