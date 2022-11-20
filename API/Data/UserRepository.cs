using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
   
   public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context,IMapper mapper)
        {
            _mapper = mapper;
       
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await  _context.Users.Where(u =>u.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
                            
            
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            var users = await _context.Users
                               .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();

            return users;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync();
        }

        public Task<AppUser> GetUserByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public Task<AppUser> GetUserByUsernameAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }


    }
}