using CourseProject.Core.Enums;
using CourseProject.Core.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Core.Models
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public SubjectType Type { get; set; }
    }
}
