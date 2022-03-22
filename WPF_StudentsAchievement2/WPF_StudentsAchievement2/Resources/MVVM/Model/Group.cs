using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_StudentsAchievement2.Resources.MVVM.Model
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public int Course { get; set; }

    }
}
