namespace Finance.Application.Validations
{
    public class Result<TEntity>
    {
        public bool IsSuccess { get; set; }
        public TEntity Value { get; set; }
        public string Error { get; set; }

        public static Result<TEntity> Success(TEntity value)
        {
            return new Result<TEntity> {IsSuccess = true, Value = value};
        }

        public static Result<TEntity> Failure(string error)
        {
            return new Result<TEntity> {IsSuccess = false, Error = error};
        }
    }
}