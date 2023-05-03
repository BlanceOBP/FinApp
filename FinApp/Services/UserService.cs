using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.MiddleEntity;
using FinApp.Interface;
using Microsoft.EntityFrameworkCore;
using FinApp.EnumValue;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace FinApp.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CollectionDto<User>> GetAll(UserFlow sort)
        {
            PaginationContext paginationContext = new PaginationContext { Page = sort.Page,};
          
            IQueryable<User> query = _context.user;
            query = sort.Sort switch
            { 
                UserSort.Name => query.OrderBy(user => user.Name),
                UserSort.Email => query.OrderBy(user => user.Email),
                UserSort.LastName => query.OrderBy(user => user.LastName),
                UserSort.MiddleName => query.OrderBy(user => user.MiddleName),
                UserSort.DateOfBirth => query.OrderBy(user => user.DateOfBirth),
                _ => query.OrderBy((SortingDirection?)sort.Sort, propertyName:sort.Sort.GetDescription())
            };

            var orderedUsers = await query.Skip(Convert.ToInt32(paginationContext.OffSet)).Take(paginationContext.PageSize).ToListAsync();

            var response = new CollectionDto<User>
            {
                Items = orderedUsers,
                Total = _context.user.Count()
            };

            return response;
        }

        public async Task<User> Get(int id)
        {
            var user = await _context.user.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new IdIsNotFound();

            return user;
        }

        public async Task<int> Create(UserCreateData userCreateData, int id)
        {
            var userExist = _context.user.SingleOrDefaultAsync(x => x.Id == id);
            if (userExist != null)
                throw new UserExists();

            var newUser = new User
            {
                Email = userCreateData.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userCreateData.Password),
                Login = userCreateData.Login,
                Name = userCreateData.Name,
                MiddleName = userCreateData.MiddleName,
                LastName = userCreateData.LastName,
                DateOfBirth = userCreateData.DateOfBirth,
                CreateOfDate = DateTime.Now,
            };

            _context.user.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task Update(UserUpdateData userUpdateData, int id)
        {
            var user1 = new User();
            var user = _context.user.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new UserNotFounfException();


            if (_context.user.SingleOrDefault(x => x.Email == userUpdateData.Email && x.Id != id) != null)
                throw new InputLoginException();
            user1.Email = userUpdateData.Email;
            user1.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateData.Password);
            if (_context.user.SingleOrDefault(x => x.Login == userUpdateData.Login && x.Id != id) != null)
                throw new InputLoginException();
            user1.Login = userUpdateData.Login;
            user1.CreateOfEdit = DateTime.Now;

            _context.user.Update(user1);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var userToDelete = await _context.user.SingleOrDefaultAsync(x => x.Id == id);
            if (userToDelete == null)
            {
                throw new UserIsDeletedException();
            }

            _context.user.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
