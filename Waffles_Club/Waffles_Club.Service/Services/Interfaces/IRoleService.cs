using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.ViewModels;

namespace Waffles_Club.Service.Services.Interfaces
{
	public interface IRoleService
	{
		public Task<List<Role>> GetAllAsync();
		public Task<Role> CreateAsync(string name);
		public Task<Role> UpdateAsync(Guid roleId, string name);
		public Task<Role> DeleteAsync(Guid roleId);
		public Task<Role> GetById(Guid roleId);
		public Task<Role> GetByName(string name);
	}
}
