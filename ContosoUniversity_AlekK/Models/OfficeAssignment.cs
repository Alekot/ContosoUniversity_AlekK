using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity_AlekK.Models
{
    public class OfficeAssignment
    {
        [Key, ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        [StringLength(50), Display(Name = "Office Location")]
        public string Location { get; set; }
        [Required]
        public virtual Instructor Instructor { get; set; }
    }
}