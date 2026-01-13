namespace PureGaze.Application.Requests;

public interface IRequestDispatcher 
{
    Task SendAsync<TRequest>(TRequest request, CancellationToken ct) 
        where TRequest : IRequest;

    Task<TRequestResult> SendAsync<TRequest, TRequestResult>(TRequest request,  CancellationToken ct) 
        where TRequest : IRequest<TRequestResult>;
}