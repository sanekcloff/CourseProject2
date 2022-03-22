using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_StudentsAchievement2.Resources.MVVM.Model
{
    public class Student
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        public string FirstName{ get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        [ForeignKey("GroupId")]
        public Group GroupId { get; set; }
    }
}
