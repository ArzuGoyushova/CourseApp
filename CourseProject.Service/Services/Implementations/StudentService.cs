using CourseProject.Data.Repositories.Implementations;
using CourseProject.Data.Repositories.Interfaces;
using CourseProject.Service.Services.Interfaces;
using CourseProject.Service.Utilities.Helper.Constants;
using CourseProject.Service.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Core.Models;
using System.Globalization;
using System.Xml.Linq;

namespace CourseProject.Service.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentGradeRepository _studentGradeRepository;
        public StudentService()
        {
            _studentRepository = new StudentRepository();
            _groupRepository = new GroupRepository();
            _subjectRepository = new SubjectRepository();
            _studentGradeRepository = new StudentGradeRepository();
        }
        public async Task AddAsync()
        {
            Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterName);
        validName: string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name))
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.TypoMessage);
                goto validName;
            }
            Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterSurName);
        validSurname: string surname = Console.ReadLine();
            if (String.IsNullOrEmpty(surname))
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.TypoMessage);
                goto validSurname;
            }

            Helper.HelperMessage(ConsoleColor.Magenta, "Enter student's birthday in format MM/DD/YYYY: ");
            string input = Console.ReadLine();
            DateTime birthday;

            if (DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
            {
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in MM/DD/YYYY format.");
            }

            Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterFinCode);
        validfinCode: string fincode = Console.ReadLine();
            if (String.IsNullOrEmpty(fincode) || fincode.Length!=7)
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.TypoMessage);
                goto validfinCode;
            }

            Helper.HelperMessage(ConsoleColor.Magenta, "Enter group Id: ");
            EnterGroupId:  int.TryParse(Console.ReadLine(), out int groupId);
            Group group = await _groupRepository.GetByIdAsync(groupId);
            if (group == null)
            {
                Console.WriteLine("There is no such a group with entered id.");
                goto EnterGroupId;
            }

            Student student = new()
            {
                Name = name,
                Surname = surname,
                Birthday = birthday,
                FinCode = fincode,
                Group = group,
                CreatedDate = DateTime.Now,
            };
            await _studentRepository.AddAsync(student);

            group.Students.Add(student);

            Helper.HelperMessage(ConsoleColor.Green, $"{name} " + StudentConstants.SuccesfullyCreated);
        }

        public async Task DeleteAsync()
        {
            Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterIdNumber);
        EnterId: int.TryParse(Console.ReadLine(), out int id);

            if (await _studentRepository.GetByIdAsync(id) == null)
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.RequestedStudentError);
                goto EnterId;
            }
            else
            {
                await _studentRepository.DeleteAsync(id);
                Helper.HelperMessage(ConsoleColor.Green, StudentConstants.SuccesfullyDeleted);
            }
        }

        public async Task GetAllAsync()
        {
            var t = new TablePrinter("Id", "Name", "Surname", "Birthday", "Group Name", "Fincode");

            foreach (var student in await _studentRepository.GetAllAsync())
            {
                t.AddRow(student.Id, student.Name, student.Surname, student.Birthday.ToString("dd-MM-yyyy"), student.Group.Name, student.FinCode);
            }
            t.Print();
        }

        public async Task GetAsync()
        {
        EnterIdNumber: Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterIdNumber);
            int.TryParse(Console.ReadLine(), out int id);
            Student student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.RequestedStudentError);
                goto EnterIdNumber;
            }
            else
            {
                Helper.HelperMessage(ConsoleColor.Green, StudentConstants.RequestedStudent);
                var t = new TablePrinter("Id", "Name", "Surname", "Birthday", "Group Name", "Fincode");
                t.AddRow(student.Id, student.Name, student.Surname, student.Birthday.ToString("dd-MM-yyyy"), student.Group.Name, student.FinCode);
                t.Print();
            }

        }
        public async Task AddStudentGradesAsync()
        {
        EnterStudentId: Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterIdNumber);
            int.TryParse(Console.ReadLine(), out int studentId);
            Student student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.RequestedStudentError);
                goto EnterStudentId;
            }
        EnterSubjectId: Helper.HelperMessage(ConsoleColor.Magenta, SubjectConstants.EnterIdNumber);
            int.TryParse(Console.ReadLine(), out int subjectId);
            Subject subject = await _subjectRepository.GetByIdAsync(subjectId);
            if (subject == null)
            {
                Helper.HelperMessage(ConsoleColor.Red, SubjectConstants.RequestedSubjectError);
                goto EnterSubjectId;
            }
        EnterGrade: Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterGrade);
            double.TryParse(Console.ReadLine(), out double grade);
            if (grade < 0 || grade > 100)
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.TypoMessage);
                goto EnterGrade;
            }
            StudentGrade studentGrade = new()
            {
                Grade = grade,
                Student = student,
                Subject = subject,
                CreatedDate = DateTime.Now
            };
            await _studentGradeRepository.AddAsync(studentGrade);
            student.Grades.Add(studentGrade);
            Helper.HelperMessage(ConsoleColor.Green, StudentConstants.GradesAdded);
        }
        public async Task GetStudentGradesAsync()
        {
        EnterStudentId: Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterIdNumber);
            int.TryParse(Console.ReadLine(), out int studentId);
            Student student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.RequestedStudentError);
                goto EnterStudentId;
            }
            ICollection<StudentGrade> studentGrades = await _studentGradeRepository.GetAllAsync();
            ICollection<StudentGrade> filteredStudentGrades = studentGrades.Where(x=>x.Student == student).ToList();
            
            var t = new TablePrinter("Id", "Student Name", "Subject Name", "Grade" );
            foreach (var item in filteredStudentGrades)
            {
                t.AddRow(item.Id, item.Student.Name, item.Subject.Name, item.Grade);
            }
            t.Print();
        }
        public async Task GetStudentGPAAsync()
        {
        EnterStudentId: Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterIdNumber);
            int.TryParse(Console.ReadLine(), out int studentId);
            Student student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                Helper.HelperMessage(ConsoleColor.Red, StudentConstants.RequestedStudentError);
                goto EnterStudentId;
            }
            ICollection<StudentGrade> studentGrades = await _studentGradeRepository.GetAllAsync();
            ICollection<StudentGrade> filteredStudentGrades = studentGrades.Where(x => x.Student == student).ToList();
            double gpa = filteredStudentGrades.Average(x => x.Grade);
            
            var t = new TablePrinter("Student Id", "Student Name", "GPA");

            t.AddRow(student.Id, student.Name, gpa);
            
            t.Print();
        }
        public async Task UpdateAsync()
        {
        EnterIdNumber: Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterIdNumber);
            int.TryParse(Console.ReadLine(), out int id);
            Student updatedStudent = await _studentRepository.GetByIdAsync(id);
            if (updatedStudent != null)
            {
                Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterName);
            validName: string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Helper.HelperMessage(ConsoleColor.Red, StudentConstants.TypoMessage);
                    goto validName;
                }
                Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterSurName);
            validSurname: string surname = Console.ReadLine();
                if (String.IsNullOrEmpty(surname))
                {
                    Helper.HelperMessage(ConsoleColor.Red, StudentConstants.TypoMessage);
                    goto validSurname;
                }
                DateTime birthday;
                Helper.HelperMessage(ConsoleColor.Magenta, "Enter student's birthday in format MM/DD/YYYY: ");
                birthday = DateTime.Parse(Console.ReadLine());
                Helper.HelperMessage(ConsoleColor.Magenta, StudentConstants.EnterFinCode);
            validfinCode: string fincode = Console.ReadLine();
                if (String.IsNullOrEmpty(fincode))
                {
                    Helper.HelperMessage(ConsoleColor.Red, StudentConstants.TypoMessage);
                    goto validfinCode;
                }
                Helper.HelperMessage(ConsoleColor.Magenta, "Enter group Id: ");
            EnterGroupId: int.TryParse(Console.ReadLine(), out int groupId);
                Group group = await _groupRepository.GetByIdAsync(groupId);
                if (group == null)
                {
                    Console.WriteLine("Bu Id-e uygun qrup yoxdur.");
                    goto EnterGroupId;
                }

                updatedStudent.Name = name;
                updatedStudent.Surname = surname;
                updatedStudent.Birthday = birthday;
                updatedStudent.FinCode = fincode;
                updatedStudent.Group = group;
                updatedStudent.UpdatedDate = DateTime.Now;
                await _studentRepository.UpdateAsync(updatedStudent);
            }
        }
    }
}
