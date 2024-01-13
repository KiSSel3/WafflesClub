using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.DataManagment.Interfaces;
using Waffles_Club.Service.Services.Interfaces;

namespace Waffles_Club.Service.Services.Implementations
{
	public class RoleService : IRoleService
	{
		private readonly IRoleRepository _roleRepository;

		public RoleService(IRoleRepository roleRepository) =>
			_roleRepository = roleRepository;

		public async Task<Role> CreateAsync(string name)
		{
			var roleByName = await _roleRepository.GetByName(name);
			if(roleByName != null)
			{
				throw new Exception("A role already exists");
			}

			var newRole = new Role { Name = name };

			await _roleRepository.Create(newRole);

			return newRole;
		}

		public async Task<Role> DeleteAsync(Guid roleId)
		{
			var roleById = await _roleRepository.GetById(roleId);
			if(roleById == null)
			{
				throw new Exception("No role");
			}

			await _roleRepository.Delete(roleById);

			return roleById;
		}

		public async Task<List<Role>> GetAllAsync()
		{
			var roles = await _roleRepository.GetAll();

			return roles;
		}

		public async Task<Role> GetByIdAsync(Guid roleId)
		{
			var roleById = await _roleRepository.GetById(roleId);
			if (roleById == null)
			{
				throw new Exception("No role");
			}

			return roleById;
		}

		public async Task<Role> GetByNameAsync(string name)
		{
			var roleByName = await _roleRepository.GetByName(name);
			if (roleByName == null)
			{
				throw new Exception("No role");
			}

			return roleByName;
		}

		public async Task<Role> UpdateAsync(Guid roleId, string name)
		{
			var roleByName = await _roleRepository.GetByName(name);
			if (roleByName != null)
			{
				throw new Exception("A role already exists");
			}

			var roleById = await _roleRepository.GetById(roleId);
			if (roleById == null)
			{
				throw new Exception("No role");
			}

			roleById.Name = name;

			await _roleRepository.Update(roleById);

			return roleById;
		}
	}
}
