using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsUserExists(int userId);
    }
}
