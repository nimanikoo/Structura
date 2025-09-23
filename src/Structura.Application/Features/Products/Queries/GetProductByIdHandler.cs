using MediatR;
using Structura.Application.Features.Products.DTOs;
using Structura.Application.Interfaces;

namespace Structura.Application.Features.Products.Queries;

public class GetProductByIdHandler(IProductRepository repo) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repo.GetByIdAsync(request.Id, cancellationToken);
        return product is null ? null : new ProductDto(product.Id, product.Name, product.Price);
    }
}