using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WPF_StudentsAchievement2.Resources.MVVM.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int? StudentId { get; set; }
        
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [Required]
        public int? DisciplineId { get; set; }
        
        [ForeignKey("DisciplineId")]
        public Discipline Discipline { get; set; }
        
        [Required]
        public int GradeValue { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
