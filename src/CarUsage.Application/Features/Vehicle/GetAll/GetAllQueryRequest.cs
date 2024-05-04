using CarUsage.Core.ResultPattern;
using MediatR;

namespace CarUsage.Application.Features.Vehicle.GetAll;

public sealed record GetAllQueryRequest(): IRequest<IDataResult<List<GetAllQueryResponse>>>;