using AccessLayer.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using ModelsLayer.Dtos;
using ModelsLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public enum LoginStatus
    {
        [Description("Login Successfully")]
        Sucess,
        [Description("User Name Invalid ?")]
        UserNameInvalid,
        [Description("Password Check Invalid ....")]
        PasswordInvalid
    }
    public interface ISystemUserService 
    {
        bool CheckPassword(SystemUser user, string Password);
        SystemUserDto Login(SystemUserLogin loginDto, out LoginStatus State);
        Task<int> Add(SystemUserInsert systemUser);
        Task<SystemUserDto> GetAsync();

    }
    public class SystemUserService : BaseReprository, ISystemUserService
    {
        public SystemUserService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public bool CheckPassword(SystemUser user, string Password)
        {
            using var Hmac = new HMACSHA512(user.PasswordSalt);

            var ComputedHash = Hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));

            for (int i = 0; i < ComputedHash.Length; i++)
                if (ComputedHash[i] != user.PasswordHash[i])
                    return false;
            return true;
        }

        public SystemUserDto Login(SystemUserLogin loginDto, out LoginStatus State)
        {
            State = LoginStatus.Sucess;

            var user = _context.SystemUsers.SingleOrDefault(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null)
            {
                State = LoginStatus.UserNameInvalid;
                return new SystemUserDto { Id = -1 };
            }

            if (!CheckPassword(user, loginDto.Password))
            {
                State = LoginStatus.PasswordInvalid;
                return new SystemUserDto { Id = -1 };
            }

            return _mapper.Map<SystemUserDto>(user);
        }

        public async Task<int> Add(SystemUserInsert systemUser)
        {
            var data = await _context.SystemUsers.Where(r => r.UserName == systemUser.UserName).FirstOrDefaultAsync();
            if (data != null)
                return data.Id;

            using var hamc = new HMACSHA512();
            var item = new SystemUser()
            {
                PasswordHash = hamc.ComputeHash(Encoding.UTF8.GetBytes(systemUser.Password)),
                PasswordSalt = hamc.Key
            };
            _mapper.Map(systemUser, item);
            _context.SystemUsers.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<SystemUserDto> GetAsync()
        {
            var query = _context.SystemUsers.AsQueryable();

            return await query.ProjectTo<SystemUserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        
    }
}
