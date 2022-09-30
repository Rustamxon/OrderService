using AutoMapper;
using OrderService.Data.IRepositories;
using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Users;
using OrderService.Service.DTOs.Customers;
using OrderService.Service.Exceptions;
using OrderService.Service.Extensions;
using OrderService.Service.Interfaces;
using OrderService.Service.Interfaces.CustomerService;
using System.Linq.Expressions;

namespace OrderService.Service.Services.CustomerServices;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IAttachmentService attachmentService;

    public UserService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IAttachmentService attachmentService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.attachmentService = attachmentService;
    }

    public async ValueTask<User> CreateAsync(UserForCreationDto userDto)
    {
        var user = await this.unitOfWork.Users.GetAsync(user => 
            user.FirstName.Equals(userDto.FirstName)
                && user.LastName.Equals(userDto.LastName));

        if (user is not null)
            throw new CustomException(400, "User already exists");

        var mappedUser = this.mapper.Map<User>(userDto);
        mappedUser.Create();
        
        mappedUser = await this.unitOfWork.Users.CreateAsync(mappedUser);
        await this.unitOfWork.SaveChangesAsync();

        return mappedUser;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        var user = await this.unitOfWork.Users.GetAsync(expression);

        if (user is null)
            throw new CustomException(404, "Couldn't find user for given id");

        user.Delete();
        await this.unitOfWork.SaveChangesAsync();

        return true;
    }

    public IEnumerable<User> GetAll(
        PaginationParams @params,
        Expression<Func<User, bool>> expression = null)
    {
        var users =
            this.unitOfWork.Users.Where(expression).ToPaged(@params);

        return users.ToList();
    }

    public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression)
    {
        var user = await this.unitOfWork.Users.GetAsync(expression);

        if (user is null)
            throw new CustomException(404, "Couldn't find user for given id");

        return user;
    }

    public async ValueTask<User> UpdateAsync(long id, UserForCreationDto userDto)
    {
        var user = 
            await this.unitOfWork.Users.GetAsync(user => user.Id.Equals(id));

        if (user is null)
            throw new CustomException(404, "Couldn't find user for given id");

        var mappedUser = this.mapper.Map(userDto, user);
        mappedUser.Update();

        mappedUser = await this.unitOfWork.Users.UpdateAsync(mappedUser);
        await this.unitOfWork.SaveChangesAsync();

        return mappedUser;
    }
}
