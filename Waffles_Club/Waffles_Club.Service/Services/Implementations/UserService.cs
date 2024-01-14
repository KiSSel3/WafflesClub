using Microsoft.Extensions.Logging;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Models;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;
using Waffles_Club.Shared.Mappers;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Service.Services.Implementations;

public class UserService:IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleUserRepository _roleUserRepository;
    private readonly StringToGuidMapper _guidMapper;

    public UserService(ILogger<UserService> logger, IUserRepository userRepository, IRoleRepository roleRepository, IRoleUserRepository roleUserRepository, StringToGuidMapper guidMapper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _roleUserRepository = roleUserRepository;
        _guidMapper = guidMapper;
    }

    private void HandleError(string message)
    {
        _logger.LogError(message);
        throw new Exception(message);
    }
    public async Task CreateAsync(RegisterViewModel viewModel)
    {
        var userByLogin = await _userRepository.GetByLogin(viewModel.Login);
        if (userByLogin!=null)
        {
            HandleError("User with same login is exists");
        }
        var user = new User()
        {
            Email = viewModel.Email,
            Login = viewModel.Login,
            Name = viewModel.Name,
            Password = viewModel.Password
        };
        await _userRepository.Create(user);
    }

    public async Task<User> GetById(string userId)
    {
        var guidUserId = _guidMapper.MapTo(userId);
        var user = await _userRepository.GetById(guidUserId);
        if (user==null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        return user;
    }

    public async Task<List<User>> GetAll()
    {
        var users = await _userRepository.GetAll();
        if (users.Count==0)
        {
            HandleError("Users are not found");
        }

        return users;
    }

    public async Task AddToRoleAsync(string userId, string roleName)
    {
        var guidUserId = _guidMapper.MapTo(userId);
        var user = await _userRepository.GetById(guidUserId);
        var role = await _roleRepository.GetByName(roleName);
        if (user==null||role==null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        await _roleUserRepository.Create(new RoleUser() { RoleId = role.Id, UserId = user.Id });

    }

    public async Task DeleteAsync(string userId)
    {
        var guidUserId = _guidMapper.MapTo(userId);
        var user = await _userRepository.GetById(guidUserId);
        if (user==null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        await _userRepository.Delete(user);
    }

    public async Task UpdateAsync(string userId, UpdateUserViewModel viewModel)
    {
        var guidUserId = _guidMapper.MapTo(userId);
        var user = await _userRepository.GetById(guidUserId);
        if (user==null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        user.Name = viewModel.Name;
        user.Email = viewModel.Email;
        user.Login = viewModel.Login;
        await _userRepository.Update(user);
    }
    

    public async Task UpdatePassword(string userId,UpdatePasswordViewModel viewModel)
    {
        var guidUserId = _guidMapper.MapTo(userId);
        var user = await _userRepository.GetById(guidUserId);
        if (user==null)
        {
            HandleError($"User with id: {guidUserId} not found");
        }

        if (viewModel.CurrentPassword!=user.Password)
        {
            HandleError("Invalid password");
        }

        user.Password = viewModel.NewPassword;
        await _userRepository.Update(user);
    }

    public async Task<User> GetByLogin(string login)
    {
        var user = await _userRepository.GetByLogin(login);
        if (user==null)
        {
            HandleError($"User with login: {login} not found");
        }

        return user;
    }
}