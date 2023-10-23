using IMS.Application.Repositories;
using IMS.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMSDbContext _dbContext;

        public UserRepository(IMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsUserExists(int userId)
        {
            return await _dbContext.Users.AnyAsync(x => x.Id == userId);
        }
    }
}
