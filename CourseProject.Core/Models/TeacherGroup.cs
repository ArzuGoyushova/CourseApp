using CourseProject.Core.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Core.Models
{
    public class TeacherGroup : BaseEntity
    {
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}
