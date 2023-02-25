namespace Zero.AspNetCore.Mvc.Models
{
    public class AjaxResponse<TResult>
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public TResult Result { get; set; }

        public AjaxResponse()
        {
            Message = "成功";
            IsSuccessful = true;
        }

        public AjaxResponse(TResult result) : this()
        {
            IsSuccessful = true;
            Result = result;
        }
        public AjaxResponse(bool isSuccessful, string message) : this()
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }

    public class AjaxResponse : AjaxResponse<object>
    {
        public AjaxResponse()
        {

        }
        public AjaxResponse(object result)
           : base(result)
        {

        }
        public AjaxResponse(bool isSuccessful, string message)
            : base(isSuccessful, message)
        {

        }
    }
}
