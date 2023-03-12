using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    //public class AccountController : BaseApiController
    //{
    //  //  private readonly DataContext _context;
    //    private readonly UserManager<AppUser> _userManager;
    //    private readonly ITokenService _tokenService;
    //    private readonly IMapper _mapper;

    //    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
    //    {
    //        _userManager = userManager;
    //        _tokenService = tokenService;
    //        _mapper = mapper;
    //    }




    //    [HttpPost("register")]
    //    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    //    {

    //        if (await UserExists(registerDto.Username)) return BadRequest("username is taken");

    //        //going to appuser fro dto

    //        var user = _mapper.Map<AppUser>(registerDto);

    //        //using call the dispose mehtod



    //        user.UserName = registerDto.Username.ToLower();
    //        //   user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
    //        //     user.PasswordSalt = hmac.Key;


    //        //_context.Users.Add(user);
    //        //await _context.SaveChangesAsync();

    //        var result = await _userManager.CreateAsync(user, registerDto.Password);
    //        if (!result.Succeeded) return BadRequest(result.Errors);

    //        return new UserDto
    //        {
    //            Username = user.UserName,
    //            Token = await _tokenService.CreateToken(user),
    //            KnownAs = user.KnownAs,
    //            Gender = user.Gender

    //        };
    //    }

    //    [HttpPost("login")]
    //    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    //    {
    //        var user = await _userManager.Users.Include(x => x.Photos).SingleOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());
    //        if (user == null) return Unauthorized("Invalid username");



    //        var testt = user.Photos.FirstOrDefault(x => x.IsMain)?.Url;
    //        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

    //        if (!result) return Unauthorized("Invalid username or password");
    //        return new UserDto
    //        {
    //            Username = user.UserName,
    //            Token = await _tokenService.CreateToken(user),
    //            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
    //            KnownAs = user.KnownAs,
    //            Gender = user.Gender

    //        };

    //    }

    //    private async Task<bool> UserExists(string username)
    //    {
    //        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    //    }



    //}

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")] // POST: api/account/register?username=dave&password=pwd
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            //if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            //var user = _mapper.Map<AppUser>(registerDto);

            //user.UserName = registerDto.Username.ToLower();

            //var result = await _userManager.CreateAsync(user, registerDto.Password);

            //if (!result.Succeeded) return BadRequest(result.Errors);

            //var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            //if (!roleResult.Succeeded) return BadRequest(result.Errors);

            //return new UserDto
            //{
            //    Username = user.UserName,
            //    Token = await _tokenService.CreateToken(user),
            //    KnownAs = user.KnownAs,
            //    Gender = user.Gender
            //};

            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("invalid username");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Invalid password");

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}


