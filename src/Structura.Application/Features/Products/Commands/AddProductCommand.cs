using MediatR;
using Structura.Application.Features.Products.DTOs;

namespace Structura.Application.Features.Products.Commands;

public record AddProductCommand(string Name, decimal Price) : IRequest<ProductDto>;
