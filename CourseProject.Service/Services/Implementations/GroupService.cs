using CourseProject.Core.Models;
using CourseProject.Data.Repositories.Implementations;
using CourseProject.Data.Repositories.Interfaces;
using CourseProject.Service.Services.Interfaces;
using CourseProject.Service.Utilities.Helper;
using CourseProject.Service.Utilities.Helper.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Service.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService()
        {
            _groupRepository = new GroupRepository();
        }
        public async Task AddAsync()
        {
            Helper.HelperMessage(ConsoleColor.Magenta, GroupConstants.EnterName);
        validName: string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name))
            {
                Helper.HelperMessage(ConsoleColor.Red, GroupConstants.TypoMessage);
                goto validName;
            }

            Helper.HelperMessage(ConsoleColor.Magenta, GroupConstants.EnterDesc);
            string desc = Console.ReadLine();

            await _groupRepository.AddAsync(new()
            {
                Name = name,
                Description = desc,
                CreatedDate = DateTime.Now,

            });
            Helper.HelperMessage(ConsoleColor.Green, $"{name} " + GroupConstants.SuccesfullyCreated);
        }

        public async Task DeleteAsync()
        {
            Helper.HelperMessage(ConsoleColor.Magenta, GroupConstants.EnterIdNumber);
            EnterId: int.TryParse(Console.ReadLine(), out int id);

            if (await _groupRepository.GetByIdAsync(id) == null)
            {
                Helper.HelperMessage(ConsoleColor.Red, GroupConstants.RequestedGroupError);
                goto EnterId;
            }
            else
            {
                await _groupRepository.DeleteAsync(id);
                Helper.HelperMessage(ConsoleColor.Green, GroupConstants.SuccesfullyDeleted);
            }
        }

        public async Task GetAllAsync()
        {
            var t1 = new TablePrinter("Id", "Name", "Description");
            var t2 = new TablePrinter("Group ID", "Students");
            var t3 = new TablePrinter("Group ID", "Teachers");

            foreach (var group in await _groupRepository.GetAllAsync())
            {
                t1.AddRow(group.Id, group.Name, group.Description);

                if (group.Students != null && group.Students.Count != 0)
                {
                    foreach (var student in group.Students)
                    {
                        t2.AddRow(group.Id, student.Id + " - " + student.Surname + " " + student.Name);
                    }
                }
                else
                {
                    t2.AddRow(group.Id, "No students");
                }

                if (group.TeacherGroups != null && group.TeacherGroups.Count != 0)
                {
                    foreach (var teacherGroup in group.TeacherGroups)
                    {
                        t3.AddRow(group.Id, teacherGroup.Teacher.Id + " - " + teacherGroup.Teacher.Name);
                    }
                }
                else
                {
                    t3.AddRow(group.Id, "No teachers");
                }
            }

            t1.Print();
            TablePrinter.PrintSideBySide(t2, t3);
        }


        public async Task GetAsync()
        {
        EnterIdNumber: Helper.HelperMessage(ConsoleColor.Magenta, GroupConstants.EnterIdNumber);
            int.TryParse(Console.ReadLine(), out int id);
            Group group = await _groupRepository.GetByIdAsync(id);
            if (group == null)
            {
                Helper.HelperMessage(ConsoleColor.Red, GroupConstants.RequestedGroupError);
                goto EnterIdNumber;
            }
            else
            {
                Helper.HelperMessage(ConsoleColor.Green, GroupConstants.RequestedGroup);
                var t1 = new TablePrinter("Id", "Name", "Description");
                t1.AddRow(group.Id, group.Name, group.Description);

                var t2 = new TablePrinter("Group ID", "Students");
                var t3 = new TablePrinter("Group ID", "Teachers");

                if (group.Students.Count != 0)
                {
                    foreach (var student in group.Students)
                    {
                        t2.AddRow(group.Id, student.Name);
                    }
                } else
                {
                    t2.AddRow(group.Id, "No students");
                }
                if (group.TeacherGroups.Count != 0)
                {

                    foreach (var teacherGroup in group.TeacherGroups)
                    {
                        t3.AddRow(group.Id, teacherGroup.Teacher.Id + " - " + teacherGroup.Teacher.Name);
                    }
                }
                else
                {
                    t3.AddRow(group.Id, "No teachers");
                }

                t1.Print();
                TablePrinter.PrintSideBySide(t2, t3);
            }

        }

        public async Task UpdateAsync()
        {
        EnterIdNumber: Helper.HelperMessage(ConsoleColor.Magenta, GroupConstants.EnterIdNumber);
            int.TryParse(Console.ReadLine(), out int id);
            Group updatedGroup = await _groupRepository.GetByIdAsync(id);
            if (updatedGroup != null)
            {
                Helper.HelperMessage(ConsoleColor.Magenta, GroupConstants.EnterName);
            validName: string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Helper.HelperMessage(ConsoleColor.Red, GroupConstants.TypoMessage);
                    goto validName;
                }

                Helper.HelperMessage(ConsoleColor.Magenta, GroupConstants.EnterDesc);
                string desc = Console.ReadLine();

                updatedGroup.Name = name;
                updatedGroup.Description = desc;
                updatedGroup.UpdatedDate = DateTime.Now;
                await _groupRepository.UpdateAsync(updatedGroup);
                Helper.HelperMessage(ConsoleColor.Green, $"{name} " + GroupConstants.SuccesfullyUpdated);
            }
        }
    }
}
