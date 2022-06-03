
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "請輸入名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入順序")]
        [Range(1,100,ErrorMessage ="輸入介於1~100,感謝!!!")]
        public int DisplayOrder { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
