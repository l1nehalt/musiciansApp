using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansApp.Model
{
    public class User 
    {
        [Key]
        [Column("id")]  
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [Column("name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        [Column("password")]
        public string Password { get; set; }
    }
}
