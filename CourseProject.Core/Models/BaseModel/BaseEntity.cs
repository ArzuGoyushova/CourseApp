using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Core.Models.BaseModel
{
    public abstract class BaseEntity
    {
        public readonly int Id;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        private static int _count = 0;
        public BaseEntity()
        {
            _count++;
            Id = _count;
        }
    }
}
