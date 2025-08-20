using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaERP.Data;
using SpaERP.Models;
using SpaERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaERP.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService DomainService;
        private readonly ILogger<UsersController> Logger;

        protected UsersController(IUsersService domainService, ILogger<UsersController> logger = null) : base()
        {
            this.DomainService = domainService ?? throw new ArgumentNullException(nameof(domainService));
            this.Logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(CancellationToken cancellationToken)
        {
            var result = await this.DomainService.GetUsersAsync(cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        // Add more actions (POST, PUT, DELETE) as needed
    }
}