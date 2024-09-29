using System.Text.Json.Serialization;

namespace eAppointment.Backend.Domain.Helpers
{
    public sealed class Result<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessages { get; set; }
        public bool IsSuccessfull { get; set; } = true;

        [JsonIgnore]
        public int StatusCode { get; set; }

        public Result(int statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public Result(int statusCode, List<string> errorMessages)
        {
            IsSuccessfull = false;
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
        }

        public Result(int statusCode, string errorMessage)
        {
            IsSuccessfull = false;
            StatusCode = statusCode;
            ErrorMessages = new() { errorMessage };
        }

        public static implicit operator Result<T>((int statusCode, T data) parameters)
        {
            return new(parameters.statusCode, parameters.data);
        }

        public static implicit operator Result<T>((int statusCode, List<string> errorMessages) parameters)
        {
            return new(parameters.statusCode, parameters.errorMessages);
        }

        public static implicit operator Result<T>((int statusCode, string errorMessage) parameters)
        {
            return new(parameters.statusCode, parameters.errorMessage);
        }


        public static Result<T> Succeed(int statusCode, T data)
        {
            return new(statusCode, data);
        }

        public static Result<T> Failure(int statusCode, List<string> errorMessages)
        {
            return new(statusCode, errorMessages);
        }

        public static Result<T> Failure(int statusCode, string errorMessage)
        {
            return new(statusCode, errorMessage);
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new(500, errorMessage);
        }

        public static Result<T> Failure(List<string> errorMessages)
        {
            return new(500, errorMessages);
        }
    }
}
