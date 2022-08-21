using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repo;
        public UsersController(IUserRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("id", Name = "GetUserById")]
        public ActionResult<User> GetUserById(string id)
        {
            var user = _repo.GetUserById(id);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_repo.GetAllUsers());
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _repo.CreateUser(user);
            return CreatedAtRoute(nameof(GetUserById), new { Id = user.Id }, user);
        }


    }
}
