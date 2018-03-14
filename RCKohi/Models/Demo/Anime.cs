using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCKohi.Models.Demo
{
    public class Anime
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须多于 {2} 位并且少于 {1} 位。", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "卷均销量")]
        [RegularExpression(@"^([1-9]\d*)$", ErrorMessage = "只能输入整数。")]
        public int Number { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "播放时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BroadcastDate { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "首页显示")]
        public bool IndexShow { get; set; }
    }
}
