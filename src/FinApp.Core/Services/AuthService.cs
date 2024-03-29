﻿using FinApp.Core.Exceptions;
using FinApp.Core.Interfaces;
using FinApp.Core.Models;
using FinApp.Core.Options;
using FinApp.Entities;
using FinApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;

        public AuthService(ApplicationContext context)
        {
            _context = context;
        }

        public JwtSecurityToken Authorization(LoginData user)
        {
            if (_context.Users.AnyAsync(x => x.Email == user.Email && x.Password == user.Password) != null)
            {
                throw new LoginException();
            }

            var claims = new List<Claim> { new(ClaimTypes.Name, user.Email) };
            var jwt = new JwtSecurityToken(
            claims: claims,
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return jwt;
        }

        public async Task Registration(RegistrationData user)
        {
            if (_context.Users.AnyAsync(x => x.Login == user.Login
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

            _context.Users.Add(NewUser);
            await _context.SaveChangesAsync();
        }

    }
}
