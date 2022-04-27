namespace RunasDev.Core.Factories
{
    public interface IFactory<out T>
    {
        T Create();
    }
}