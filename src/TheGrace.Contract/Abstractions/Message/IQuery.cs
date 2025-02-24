using MediatR;

namespace TheGrace.Contract.Abstractions.Shared;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{}
