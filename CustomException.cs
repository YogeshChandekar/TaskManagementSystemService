namespace TaskManagementService
{
    public class CustomException : Exception
    {
        public string? ErrorCode { get; set; }
        public string? Txn { get; set; }

        public CustomException(string? message) : base(message)
        {
            ErrorCode = string.Empty;
            Txn = string.Empty;
        }

        public CustomException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorCode = string.Empty;
            Txn = string.Empty;
        }

        public CustomException(string? message, Exception? innerException, string? errorCode, string? txn) : base(message, innerException)
        {
            ErrorCode = errorCode;
            Txn = txn;
        }
    }
}
