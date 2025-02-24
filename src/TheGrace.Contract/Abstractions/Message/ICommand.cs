using MediatR;

namespace TheGrace.Contract.Abstractions.Shared;

public interface ICommand : IRequest<Result> {}
public interface ICommand<TResponse> : IRequest<Result<TResponse>> {}
