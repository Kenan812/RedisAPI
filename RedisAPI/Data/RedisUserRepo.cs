using RedisAPI.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisAPI.Data
{
    public class RedisUserRepo : IUserRepo
    {
        private readonly IConnectionMultiplexer _redis;
        public RedisUserRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var db = _redis.GetDatabase();
            var serialUser= JsonSerializer.Serialize(user);

            // created data will be deleted in 30 seconds
            db.StringSet(user.Id, serialUser, TimeSpan.FromSeconds(30));

            // adding value to set(to get all users)
            db.SetAdd("UserSet", serialUser);
        }

        public IEnumerable<User?>? GetAllUsers()
        {
            var db = _redis.GetDatabase();

            var completeSet = db.SetMembers("UserSet");

            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<User>(val)).ToList();
                return obj;
            }

            return null;
        }

        public User? GetUserById(string id)
        {
            var db = _redis.GetDatabase();
            var user = db.StringGet(id);

            if (!String.IsNullOrEmpty(user))
            {
                return JsonSerializer.Deserialize<User>(user);
            }
            return null;
        }
    }
}
