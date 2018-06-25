namespace BusinessRules
{
    public interface IXmlDeserialiser
    {
        T DeserializeXmlStringToObject<T>(string xmlString);
    }
}
