using FinApp.Entity;
using FinApp.EnumValue;
using FinApp.MiddleEntity;
using FinApp.SearchContext;

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
        /// <param name="sort">Users list page and order sorting.</param>
        /// </summary>
        /// <returns>Get all users.</returns>
        Task<CollectionDto<Users>> GetAll(UserFlowSearchContext sort);

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="id">Desired user ID.</param>
        /// <returns>User with the specified ID.</returns>
        Task<Users> Get(int id);

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
