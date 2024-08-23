using System.ComponentModel.DataAnnotations.Schema;

namespace coin_api.Domain.Model
{
    [Table("user")]
    public class User
    {
        public User()
        {
        }

        public User(string name, string email)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.email = email;
        }

        [Key]
        public int id { get; private set; }
        public string name { get; private set; }
        public string email { get; private set; }


    }
}