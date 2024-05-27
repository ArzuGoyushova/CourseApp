using CourseProject.Core.Models;
using CourseProject.Data.Repositories.Implementations;
using CourseProject.Data.Repositories.Interfaces;
using CourseProject.Service.Services.Interfaces;
using CourseProject.Service.Utilities.Helper.Constants;
using CourseProject.Service.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Service.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ITeacherGroupRepository _teacherGroupRepository;
        public TeacherService()
        {
            _teacherRepository = new TeacherRepository();
            _groupRepository = new GroupRepository();
            _teacherGroupRepository = new TeacherGroupRepository();
        }
        public async Task AddAsync()
        {
            try
            {
                Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterName);
            validName: string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.TypoMessage);
                    goto validName;
                }
                Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterSurName);
            validSurname: string surname = Console.ReadLine();
                if (String.IsNullOrEmpty(surname))
                {
                    Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.TypoMessage);
                    goto validSurname;
                }

                Helper.HelperMessage(ConsoleColor.Magenta, "Enter teacher's birthday in format MM/DD/YYYY: ");
            validDate: string input = Console.ReadLine();
                DateTime birthday;

                if (DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date in MM/DD/YYYY format.");
                    goto validDate;
                }

                Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterFinCode);
            validfinCode: string fincode = Console.ReadLine();
                if (String.IsNullOrEmpty(fincode) || fincode.Length != 7)
                {
                    Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.TypoMessage);
                    goto validfinCode;
                }

                Teacher teacher = new()
                {
                    Name = name,
                    Surname = surname,
                    Birthday = birthday,
                    FinCode = fincode,
                    CreatedDate = DateTime.Now,
                };
                await _teacherRepository.AddAsync(teacher);

                Helper.HelperMessage(ConsoleColor.Green, $"{name} " + TeacherConstants.SuccesfullyCreated);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task DeleteAsync()
        {
            try
            {
                Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterIdNumber);
            EnterId: int.TryParse(Console.ReadLine(), out int id);

                if (await _teacherRepository.GetByIdAsync(id) == null)
                {
                    Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.RequestedTeacherError);
                    goto EnterId;
                }
                else
                {
                    await _teacherRepository.DeleteAsync(id);
                    Helper.HelperMessage(ConsoleColor.Green, TeacherConstants.SuccesfullyDeleted);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task GetAllAsync()
        {
            try
            {
                var t = new TablePrinter("Id", "Name", "Surname", "Birthday", "Fincode");

                foreach (var teacher in await _teacherRepository.GetAllAsync())
                {
                    t.AddRow(teacher.Id, teacher.Name, teacher.Surname, teacher.Birthday.ToString("dd-MM-yyyy"), teacher.FinCode);
                }
                t.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task GetAsync()
        {
            try
            {
            EnterIdNumber: Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterIdNumber);
                int.TryParse(Console.ReadLine(), out int id);
                Teacher teacher = await _teacherRepository.GetByIdAsync(id);
                if (teacher == null)
                {
                    Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.RequestedTeacherError);
                    goto EnterIdNumber;
                }
                else
                {
                    Helper.HelperMessage(ConsoleColor.Green, TeacherConstants.RequestedTeacher);
                    var t = new TablePrinter("Id", "Name", "Surname", "Birthday", "Fincode");
                    t.AddRow(teacher.Id, teacher.Name, teacher.Surname, teacher.Birthday.ToString("dd-MM-yyyy"), teacher.FinCode);
                    t.Print();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
        public async Task AddTeacherGroupsAsync()
        {
            try
            {
            EnterTeacherId: Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterIdNumber);
                int.TryParse(Console.ReadLine(), out int teacherId);
                Teacher teacher = await _teacherRepository.GetByIdAsync(teacherId);
                if (teacher == null)
                {
                    Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.RequestedTeacherError);
                    goto EnterTeacherId;
                }
            EnterGroupId: Helper.HelperMessage(ConsoleColor.Magenta, GroupConstants.EnterIdNumber);
                int.TryParse(Console.ReadLine(), out int groupId);
                Group group = await _groupRepository.GetByIdAsync(groupId);
                if (teacher == null)
                {
                    Helper.HelperMessage(ConsoleColor.Red, GroupConstants.RequestedGroupError);
                    goto EnterGroupId;
                }
                TeacherGroup teacherGroup = new()
                {
                    Teacher = teacher,
                    Group = group,
                    CreatedDate = DateTime.Now,
                };
                await _teacherGroupRepository.AddAsync(teacherGroup);
                teacher.TeacherGroups.Add(teacherGroup);
                group.TeacherGroups.Add(teacherGroup);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task UpdateAsync()
        {
            try
            {
            EnterIdNumber: Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterIdNumber);
                int.TryParse(Console.ReadLine(), out int id);
                Teacher updatedTeacher = await _teacherRepository.GetByIdAsync(id);
                if (updatedTeacher != null)
                {
                    Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterName);
                validName: string name = Console.ReadLine();
                    if (String.IsNullOrEmpty(name))
                    {
                        Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.TypoMessage);
                        goto validName;
                    }
                    Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterSurName);
                validSurname: string surname = Console.ReadLine();
                    if (String.IsNullOrEmpty(surname))
                    {
                        Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.TypoMessage);
                        goto validSurname;
                    }

                    Helper.HelperMessage(ConsoleColor.Magenta, "Enter teacher's birthday in format MM/DD/YYYY: ");
                validDate: string input = Console.ReadLine();
                    DateTime birthday;

                    if (DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                    {
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please enter the date in MM/DD/YYYY format.");
                        goto validDate;
                    }

                    Helper.HelperMessage(ConsoleColor.Magenta, TeacherConstants.EnterFinCode);
                validfinCode: string fincode = Console.ReadLine();
                    if (String.IsNullOrEmpty(fincode))
                    {
                        Helper.HelperMessage(ConsoleColor.Red, TeacherConstants.TypoMessage);
                        goto validfinCode;
                    }

                    updatedTeacher.Name = name;
                    updatedTeacher.Surname = surname;
                    updatedTeacher.Birthday = birthday;
                    updatedTeacher.FinCode = fincode;
                    updatedTeacher.UpdatedDate = DateTime.Now;
                    await _teacherRepository.UpdateAsync(updatedTeacher);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
