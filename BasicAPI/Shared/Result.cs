namespace BasicAPI.Shared
{
    public class Result
    {
        public bool HasError { get; }
        public Error Error { get; }

        protected Result(bool hasError, Error error)
        {
            HasError = hasError;
            Error = error;
        }

        public static Result Success() => new(false, Error.None);

        public static Result Failure(Error error) => new(true, error);

        public void Deconstruct(out bool hasError, out Error error)
        {
            hasError = HasError;
            error = Error;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected Result(bool hasError, Error error, T value) : base(hasError, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(false, Error.None, value);
    }
}

