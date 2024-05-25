using CourseProject.Core.Models;
using CourseProject.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Data.Repositories.Implementations
{
    public class GroupRepository : GenericRepository<Group> , IGroupRepository
    {
    }
}
