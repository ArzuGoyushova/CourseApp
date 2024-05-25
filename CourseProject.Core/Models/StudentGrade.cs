using CourseProject.Core.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Core.Models
{
    public class StudentGrade : BaseEntity
    {
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public double Grade { get; set; }
    }
}
