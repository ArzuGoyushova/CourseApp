using CourseProject.Core.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Core.Models
{
    public class Teacher : Person
    {
        public List<TeacherGroup>? TeacherGroups { get; set; } = new List<TeacherGroup>();
    }
}
