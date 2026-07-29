

namespace Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }

        private Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, string.Empty);

        public static Result Fail(string error) => new Result(false, error);
    }
}
