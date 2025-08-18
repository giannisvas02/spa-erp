using Microsoft.AspNetCore.Mvc;
using SpaERP.Data;
using SpaERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SpaERP.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataDbContext _context;

        public UsersController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.Where(x => x.FirstName == "John").ToListAsync();
            return Ok(users);
        }

        // Add more actions (POST, PUT, DELETE) as needed
    }
}