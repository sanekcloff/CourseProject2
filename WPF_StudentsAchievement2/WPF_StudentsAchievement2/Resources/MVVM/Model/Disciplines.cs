using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_StudentsAchievement2.Resources.MVVM.Model
{
    public class Disciplines
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string DisciplineName { get; set; }
    }
}
