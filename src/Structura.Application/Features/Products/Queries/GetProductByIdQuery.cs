using MediatR;
using Structura.Application.Features.Products.DTOs;

namespace Structura.Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
