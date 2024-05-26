using CourseProject.Core.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Core.Models
{
    public class Student : Person
    {
        public Group Group { get; set; }
        public List<StudentGrade>? Grades { get; set; } = new List<StudentGrade>();
    }
}
