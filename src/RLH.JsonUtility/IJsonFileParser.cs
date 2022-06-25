namespace RLH.JsonUtility
{
    public interface IJsonFileParser : IDisposable
    {
        TClass Parse<TClass>(string? fileName = null) where TClass : class;
        IEnumerable<TClass> ParseCollection<TClass>(string? fileName = null) where TClass : class;
    }
}
