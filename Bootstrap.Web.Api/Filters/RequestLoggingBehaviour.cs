using System;
using MediatR;

namespace Bootstrap.Web.Api.Filters;

public class RequestLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
{
	private readonly IMediator _mediator;
	private readonly IHttpContextAccessor _claims;

	public RequestLoggingBehaviour(IMediator mediator, IHttpContextAccessor claims)
	{
		_mediator = mediator;
		_claims = claims;
	}

	public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		Console.WriteLine(request.GetType().Name);
		return next();
	}
}
