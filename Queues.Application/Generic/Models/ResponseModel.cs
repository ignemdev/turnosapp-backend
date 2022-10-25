namespace Queues.Application.Generic.Models;
public class ResponseModel<TData>
{
    public TData Data { get; set; } = default!;
    public string ErrorMessage { get; private set; } = null!;
    public bool HasError { get; set; }
    public void SetErrorMessage(string message)
    {
        ErrorMessage = message;
        HasError = true;
    }
}
