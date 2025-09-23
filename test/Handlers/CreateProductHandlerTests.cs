using Microsoft.EntityFrameworkCore;
using Structura.Application.Features.Products.Commands;
using Structura.Infrastructure.DbContexts;
using Structura.Infrastructure.Repositories;

namespace test.Handlers;

public class CreateProductHandlerTests
{
    private readonly AppDbContext _context;
    private readonly AddProductHandler _handler;

    public CreateProductHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        var repository = new ProductRepository(_context);

        _handler = new AddProductHandler(repository);
    }

    // sample test
    [Fact]
    public async Task Handle_ShouldCreateProduct_WhenRequestIsValid()
    {
        var command = new AddProductCommand("TestProduct", 100);

        var productDto = await _handler.Handle(command, CancellationToken.None);

        var product = await _context.Products.FindAsync(productDto.Id);
        Assert.NotNull(product);
        Assert.Equal("TestProduct", product!.Name);
        Assert.Equal(100, product.Price);
    }
}