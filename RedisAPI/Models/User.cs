using System.ComponentModel.DataAnnotations;

namespace RedisAPI.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; } = $"user:{Guid.NewGuid()}";

        [Required]
        public string Name { get; set; } = String.Empty;

    }
}
