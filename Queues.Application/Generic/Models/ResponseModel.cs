namespace Queues.Application.Generic.Models;
public class ResponseModel<TData>
{
    public TData Data { get; private set; } = default!;
    public string ErrorMessage { get; private set; } = null!;
    public bool HasError { get; private set; }

    public void SetData(TData data) => Data = data;

    public bool HasData() => Data != null;

    public void SetErrorMessage(string message)
    {
        ErrorMessage = message;
        HasError = true;
    }

    public void SetErrorMessage(Exception exception)
    {
        ErrorMessage = (exception.InnerException ?? exception).Message;
        HasError = true;
    }
}
