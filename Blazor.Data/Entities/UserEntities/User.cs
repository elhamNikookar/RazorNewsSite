using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Data.Entities.UserEntities
{
    public class User
    {
        public User()
        {

        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        public bool IsActive { get; set; } = false;

        [MaxLength(200)]
        public string? UserAvatar { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false;

        public int RoleId { get; set; }


        #region Relations

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        #endregion


    }
}
