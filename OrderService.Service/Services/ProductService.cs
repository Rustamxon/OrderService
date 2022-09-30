using AutoMapper;
using OrderService.Data.IRepositories;
using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Porducts;
using OrderService.Service.DTOs.Commons;
using OrderService.Service.Exceptions;
using OrderService.Service.Extensions;
using OrderService.Service.Interfaces.IProductServices;
using System.Linq.Expressions;

namespace OrderService.Service.Services;

public class ProductService : IProductService
{
    protected readonly IMapper mapper;
    protected readonly IUnitOfWork unitOfWork;
    public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Product> CreateAsync(ProductForCreationDto productDto)
    {
        var product =
            await this.unitOfWork.Products.GetAsync(product =>
                product.Name.Equals(productDto.Name));

        if (product is not null)
            throw new CustomException(400, "Product already exists");

        var mappedProduct = this.mapper.Map<Product>(productDto);
        mappedProduct.Create();

        mappedProduct = 
            await this.unitOfWork.Products.CreateAsync(mappedProduct);
        await this.unitOfWork.SaveChangesAsync();

        return mappedProduct;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
    {
        var product =await this.unitOfWork.Products.GetAsync(expression);

        if (product is null)
            throw new CustomException(404, "Couldn't find product for given id");

        product.Delete();
        await this.unitOfWork.SaveChangesAsync();

        return true;
    }

    public IEnumerable<Product> GetAll(
        PaginationParams @params,
        Expression<Func<Product, bool>> expression = null)
    {
        var users =
            this.unitOfWork.Products.Where(expression).ToPaged(@params);

        return users.ToList();
    }

    public async ValueTask<Product> GetAsync(Expression<Func<Product, bool>> expression)
    {
        var product = await this.unitOfWork.Products.GetAsync(expression);

        if (product is null)
            throw new CustomException(404, "Couldn't find product for given id");

        return product;
    }

    public async ValueTask<Product> UpdateAsync(long id, ProductForCreationDto productDto)
    {
        var product = 
            await this.unitOfWork.Products.GetAsync(user => user.Id.Equals(id));

        if (product is null)
            throw new CustomException(404, "Couldn't find product for given id");

        var mappedProduct = this.mapper.Map(productDto, product);
        mappedProduct.Update();

        mappedProduct = await this.unitOfWork.Products.UpdateAsync(mappedProduct);
        await this.unitOfWork.SaveChangesAsync();

        return mappedProduct;
    }
}
