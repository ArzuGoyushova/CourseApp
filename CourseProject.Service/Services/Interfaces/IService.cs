using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Service.Services.Interfaces
{
    public interface IService
    {
        public Task AddAsync();
        public Task UpdateAsync();
        public Task DeleteAsync();
        public Task GetAsync();
        public Task GetAllAsync();
    }
}
