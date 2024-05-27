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
using CourseProject.Core.Enums;
using CourseProject.Core.Models;

namespace CourseProject.Service.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService()
        {
            _subjectRepository = new SubjectRepository();
        }
        public async Task AddAsync()
        {
            try
            {
                Helper.HelperMessage(ConsoleColor.Magenta, SubjectConstants.EnterName);
            validName: string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Helper.HelperMessage(ConsoleColor.Red, SubjectConstants.TypoMessage);
                    goto validName;
                }

                Helper.HelperMessage(ConsoleColor.Magenta, SubjectConstants.EnterType);
                Helper.HelperMessage(ConsoleColor.Cyan, SubjectConstants.SubjectTypes);
                int.TryParse(Console.ReadLine(), out int type);

                await _subjectRepository.AddAsync(new()
                {
                    Name = name,
                    Type = (SubjectType)type,
                    CreatedDate = DateTime.Now,

                });
                Helper.HelperMessage(ConsoleColor.Green, $"{name} " + SubjectConstants.SuccesfullyCreated);
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
                Helper.HelperMessage(ConsoleColor.Magenta, SubjectConstants.EnterIdNumber);
            EnterId: int.TryParse(Console.ReadLine(), out int id);

                if (await _subjectRepository.GetByIdAsync(id) == null)
                {
                    Helper.HelperMessage(ConsoleColor.Red, SubjectConstants.RequestedSubjectError);
                    goto EnterId;
                }
                else
                {
                    await _subjectRepository.DeleteAsync(id);
                    Helper.HelperMessage(ConsoleColor.Green, SubjectConstants.SuccesfullyDeleted);
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
                var t = new TablePrinter("Id", "Name", "Type");

                foreach (var subject in await _subjectRepository.GetAllAsync())
                {
                    t.AddRow(subject.Id, subject.Name, subject.Type);
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
            EnterIdNumber: Helper.HelperMessage(ConsoleColor.Magenta, SubjectConstants.EnterIdNumber);
                int.TryParse(Console.ReadLine(), out int id);
                Subject subject = await _subjectRepository.GetByIdAsync(id);
                if (subject == null)
                {
                    Helper.HelperMessage(ConsoleColor.Red, SubjectConstants.RequestedSubjectError);
                    goto EnterIdNumber;
                }
                else
                {
                    Helper.HelperMessage(ConsoleColor.Green, SubjectConstants.RequestedSubject);
                    var t = new TablePrinter("Id", "Name", "Type");
                    t.AddRow(subject.Id, subject.Name, subject.Type);
                    t.Print();
                }
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
            EnterIdNumber: Helper.HelperMessage(ConsoleColor.Magenta, SubjectConstants.EnterIdNumber);
                int.TryParse(Console.ReadLine(), out int id);
                Subject updatedSubject = await _subjectRepository.GetByIdAsync(id);
                if (updatedSubject != null)
                {
                    Helper.HelperMessage(ConsoleColor.Magenta, SubjectConstants.EnterName);
                validName: string name = Console.ReadLine();
                    if (String.IsNullOrEmpty(name))
                    {
                        Helper.HelperMessage(ConsoleColor.Red, SubjectConstants.TypoMessage);
                        goto validName;
                    }

                    Helper.HelperMessage(ConsoleColor.Magenta, SubjectConstants.EnterType);
                    Helper.HelperMessage(ConsoleColor.Cyan, SubjectConstants.SubjectTypes);
                    int.TryParse(Console.ReadLine(), out int type);

                    updatedSubject.Name = name;
                    updatedSubject.Type = (SubjectType)type;
                    updatedSubject.UpdatedDate = DateTime.Now;
                    await _subjectRepository.UpdateAsync(updatedSubject);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
