using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_StudentsAchievement2.Resources.MVVM.Models
{
    public class Discipline
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string DisciplineName { get; set; }
    }
}
