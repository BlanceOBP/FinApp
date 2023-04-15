using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.MiddleEntity;
using FinApp.Interface;
using Microsoft.EntityFrameworkCore;
using FinApp.EnumValue;

namespace FinApp.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResponseType<User>> GetAll(UserSort sort, int page)
        {
            var pageResults = 5f;
            var pageCount = Math.Ceiling(_context.user.Count() / pageResults);

            IQueryable<User> query = _context.user;
            query = sort switch
            {
                UserSort.EmailAsc => query.OrderBy(x => x.Email),
                UserSort.EmailDesc => query.OrderByDescending(x => x.Email),
                UserSort.NameDesc => query.OrderByDescending(x => x.Name),
                UserSort.NameAsc => query.OrderBy(x => x.Name),
                UserSort.LastNameAsc => query.OrderBy(x => x.LastName),
                UserSort.LastNameDesc => query.OrderByDescending(x => x.LastName),
                UserSort.MiddleNameAsc => query.OrderBy(x => x.MiddleName),
                UserSort.MiddleNameDesc => query.OrderByDescending(x => x.MiddleName),
                UserSort.DateOfBirthAsc => query.OrderBy(x => x.DateOfBirth),
                UserSort.DateOfBirthDesc => query.OrderByDescending(x => x.DateOfBirth),
            };

            var orderedUsers = await query.Skip((page - 3) * Convert.ToInt32(pageResults)).Take(Convert.ToInt32(pageResults)).ToListAsync();

            var response = new ResponseType<User>
            {
                ListOfType = orderedUsers,
                CurrentPage = page,
                CountPage = Convert.ToInt32(pageCount)
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
