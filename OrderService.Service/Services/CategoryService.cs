using AutoMapper;
using OrderService.Data.IRepositories;
using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Porducts;
using OrderService.Service.DTOs;
using OrderService.Service.Exceptions;
using OrderService.Service.Extensions;
using OrderService.Service.Interfaces.ICategorieService;
using System.Linq.Expressions;

namespace OrderService.Service.Services;

public class CategoryService : ICategoryService
{
    protected readonly IMapper mapper;
    protected readonly IUnitOfWork unitOfWork;

    public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<ProductCategory> CreateAsync(CategoryForCreationDto categoryDto)
    {
        var category =
            await this.unitOfWork.ProductCategories.GetAsync(category => category.Name.Equals(categoryDto.Name));

        if (category is not null)
            throw new CustomException(400, "This category already exists");

        var mappedCategory = this.mapper.Map<ProductCategory>(categoryDto);
        mappedCategory.Create();

        var result = 
            await this.unitOfWork.ProductCategories.CreateAsync(mappedCategory);

        await this.unitOfWork.SaveChangesAsync();

        return result;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<ProductCategory, bool>> expression)
    {
        var category = 
            await this.unitOfWork.ProductCategories.GetAsync(expression);

        if (category is null)
            throw new CustomException(404, "Couldn't find category for given id");

        category.Delete();

        return true;
    }

    public IEnumerable<ProductCategory> GetAll(
        PaginationParams @params,
        Expression<Func<ProductCategory, bool>> expression = null)
    {
        var categories = 
            unitOfWork.ProductCategories.Where(expression).ToPaged(@params);

        return categories.ToList();
    }

    public async ValueTask<ProductCategory> GetAsync(Expression<Func<ProductCategory, bool>> expression)
    {
        var category =
            await this.unitOfWork.ProductCategories.GetAsync(expression);

        if (category is null)
            throw new CustomException(404, "Couldn't find category for given id");

        return category;
    }

    public async ValueTask<ProductCategory> UpdateAsync(long id, CategoryForCreationDto categoryDto)
    {
        var category =
            await this.unitOfWork.ProductCategories.GetAsync(category => category.Equals(id));

        if (category is null)
            throw new CustomException(404, "Couldn't find category for given id");

        var mappedCategory = this.mapper.Map(categoryDto, category);
        mappedCategory.Update();

        var result = await this.unitOfWork.ProductCategories.UpdateAsync(mappedCategory);
        await this.unitOfWork.SaveChangesAsync();

        return result;
    }
}
