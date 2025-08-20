using Microsoft.EntityFrameworkCore;
using SpaERP.Data;
using SpaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaERP.Services
{
    public class UsersService(DataDbContext dbContext) : IUsersService
    {
        public DataDbContext Context { get; set; } = dbContext;

        public Task<IEnumerable<User>> GetUsersAsync(CancellationToken token)
        {
            IEnumerable<User> res = 
                from user in Context.Users
                select user;

            return Task.FromResult(res);
        }
    }

    public interface IUsersService
    {
        public Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
    }
}
