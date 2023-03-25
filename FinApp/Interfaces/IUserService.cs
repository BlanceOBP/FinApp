using FinApp.Entity;
using FinApp.MiddleEntity;

namespace FinApp.Interface
{
    public interface IUserService
    {
        Task Delete(int id);
        Task<List<User>> GetAll();
        Task<User> Get(int id);
        Task Update(UserUpdateData userUpdateData, int id);
        Task<int> Create(UserCreateData userCreateData, int id);
    }
}
