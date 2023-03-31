using FinApp.Entity;
using FinApp.MiddleEntity;

namespace FinApp.Interface
{
    /// <summary>
    /// Defines methods associated with users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">Desired user ID.</param>
        Task Delete(int id);

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Get all users.</returns>
        Task<List<User>> GetAll();

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="id">Desired user ID.</param>
        /// <returns>User with the specified ID.</returns>
        Task<User> Get(int id);

        /// <summary>
        /// Updates current user data.
        /// </summary>
        /// <param name="id">Desired user ID.</param>
        /// <param name="userUpdateData">Desirable new user data.</param>
        Task Update(UserUpdateData userUpdateData, int id);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="id">Current user ID.</param>
        /// <param name="userCreateData">Desired user data.</param>
        /// <returns>Created user.</returns>
        Task<int> Create(UserCreateData userCreateData, int id);

    }
}
