using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.MiddleEntity;
using FinApp.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace FinApp.Repositories
{
    public class UserService : IUserService
    {

        private readonly ApplicationContext acb;

        public UserService(ApplicationContext _acb)
        {
            acb = _acb;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await acb.user.ToListAsync();
            return(users);
        }

        public async Task<User> Get(int id)
        {
            var user = await acb.user.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new IdIsNotFound();
            return(user);
        }

        public async Task<int> Create(UserCreateData userCreateData,int id)
        {
            var userExist = acb.user.SingleOrDefaultAsync(x => x.Id == id );
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

            acb.user.AddAsync(newUser);
            await acb.SaveChangesAsync();
            
            return newUser.Id;
        }

        public async Task Update(UserUpdateData userUpdateData, int id)
        {
            var user1 = new User();
            var user = acb.user.SingleOrDefaultAsync(x =>x.Id == id);
            if (user == null)
                throw new UserNotFounfException();


            if (acb.user.SingleOrDefault(x => x.Email == userUpdateData.Email && x.Id != id) != null)
                throw new InputLoginException();
            user1.Email = userUpdateData.Email;
            user1.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateData.Password);
            if (acb.user.SingleOrDefault(x => x.Login == userUpdateData.Login && x.Id != id) != null)
                throw new InputLoginException();
            user1.Login = userUpdateData.Login;
            user1.CreateOfEdit = DateTime.Now;
            acb.user.Update(user1);
            await acb.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var userToDelete = await acb.user.SingleOrDefaultAsync(x => x.Id == id);
            if (userToDelete == null)
            {
                throw new UserIsDeletedException();
            }
            acb.user.Remove(userToDelete);
            await acb.SaveChangesAsync();
        }
    }
}
