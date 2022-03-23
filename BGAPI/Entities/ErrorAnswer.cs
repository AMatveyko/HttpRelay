namespace BGAPI.Entities
{
    internal record ErrorAnswer
    {
        public bool IsError { get; set; }
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}