namespace Management.Application.Abstractions.Dispatcher;

public interface IRequest;
public interface IRequest<out TResponse> : IRequest;