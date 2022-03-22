using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_StudentsAchievement2.Resources.MVVM.Model
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("StudentId")]
        public Student StudentId { get; set; }
        [Required]
        [ForeignKey("DisciplineId")]
        public Discipline DisciplineId { get; set; }
        [Required]
        public int GradeValue { get; set; }
        [Required]
        public DateOnly Date { get; set; }
    }
}
