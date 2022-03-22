using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_StudentsAchievement2.Resources.MVVM.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string StudentLastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string StudentFirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string StudentMiddleName { get; set; }
        [Required]
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
