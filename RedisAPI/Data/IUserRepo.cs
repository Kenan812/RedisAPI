using RedisAPI.Models;

namespace RedisAPI.Data
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        User? GetUserById(string id);
        IEnumerable<User?>? GetAllUsers();
    }
}
