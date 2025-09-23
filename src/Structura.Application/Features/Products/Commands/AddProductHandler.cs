using MediatR;
using Structura.Application.Features.Products.DTOs;
using Structura.Application.Interfaces;
using Structura.Domain.Entities;

namespace Structura.Application.Features.Products.Commands;

public class AddProductHandler(IProductRepository productRepository) : IRequestHandler<AddProductCommand, ProductDto>
{
    private readonly IProductRepository  _productRepository = productRepository;

    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price);
        await _productRepository.AddAsync(product, cancellationToken);
        return new ProductDto(product.Id, product.Name, product.Price);
    }
}