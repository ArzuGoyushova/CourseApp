using CourseProject.Core.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Core.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Student>? Students { get; set; } = new List<Student>();
        public List<TeacherGroup>? TeacherGroups { get; set; } = new List<TeacherGroup> { };
    }
}
