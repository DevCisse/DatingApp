using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _contex;

        public UsersController(DataContext contex)
        {
            _contex = contex;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return  await  _contex.Users.ToListAsync();
         
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
            return await _contex.Users.FindAsync(id);
            
        }
    }
}
