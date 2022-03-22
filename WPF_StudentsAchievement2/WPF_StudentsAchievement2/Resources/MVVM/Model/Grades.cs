using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_StudentsAchievement2.Resources.MVVM.Model
{
    public class Grades
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("StudentId")]
        public Students StudentId { get; set; }
        [Required]
        [ForeignKey("DisciplineId")]
        public Disciplines DisciplineId { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required]
        public DateOnly Date { get; set; }
    }
}
