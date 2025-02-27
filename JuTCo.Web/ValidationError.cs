namespace JuTCo.Web
{
    public class ValidationError
    {
        public ValidationError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Message { get; }

        public string Key { get; }
    }
}