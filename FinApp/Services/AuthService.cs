﻿using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.RepositoryInterface;
using FinApp.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinApp.Repositories
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext acb;

        public AuthService(ApplicationContext _acb)
        {
            this.acb = _acb;
        }

        public JwtSecurityToken Authorization([FromBody] LoginData user)
        {
            if (acb.user.SingleOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password) != null)
            {
                throw new LoginException();
            }

            var claims = new List<Claim> { new(ClaimTypes.Name, user.Email) };
            var jwt = new JwtSecurityToken(
            claims: claims,
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return jwt;
        }

        public async Task Registration([FromBody] RegistrationData user)
        {
            if (acb.user.SingleOrDefault(x => x.Login == user.Login
                                           || x.Email == user.Email) != null)
            { 
                throw new InputLoginException();
            }

            var dateOfCreate = DateTime.Today;
            var DateOfEdit = DateTime.Today;
            var NewUser = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                DateOfBirth = Convert.ToDateTime(user.DateOfBirth),
                Email = user.Email,
                Login = user.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                CreateOfDate = dateOfCreate, 
                CreateOfEdit = DateOfEdit 
            };

            acb.user.Add(NewUser);
            await acb.SaveChangesAsync();
        }

    }
}