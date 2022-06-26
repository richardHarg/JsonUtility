namespace RLH.JsonUtility
{
    public interface IJsonFileParser : IDisposable
    {
        TClass Parse<TClass>(string fileName) where TClass : class;
        IEnumerable<TClass> ParseCollection<TClass>(string fileName) where TClass : class;
        TClass Parse<TClass>() where TClass : class;
        IEnumerable<TClass> ParseCollection<TClass>() where TClass : class;
    }
}
