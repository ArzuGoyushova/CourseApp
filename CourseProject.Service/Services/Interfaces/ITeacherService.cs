using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Service.Services.Interfaces
{
    public interface ITeacherService : IService 
    {
        public Task AddTeacherGroupsAsync();
    }
}
