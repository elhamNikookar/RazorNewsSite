using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.Entities.UserEntities
{
    public class Role
    {
        public Role()
        {
            
        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name ="عنوان نقش")]
        [Required(ErrorMessage ="لطفا{0} را وارد کنید.")]
        [MaxLength(200,ErrorMessage ="{0} نمی تواند بیش از{1} کاراکتر  اشته باشد.")]
        public string RoleTitle { get; set; }

        public bool IsDelete { get; set; } = false;

        #region Relations


        #endregion
    }
}
