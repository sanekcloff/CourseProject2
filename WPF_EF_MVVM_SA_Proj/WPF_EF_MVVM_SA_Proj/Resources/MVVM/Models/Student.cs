using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string StudentFIO { get; set; }
        [Required]
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        [NotMapped]
        public Group StudentGroup 
        {
            get
            {
                return DataWorker.GetGroupById((int)GroupId);
            }
        }
    }
}
