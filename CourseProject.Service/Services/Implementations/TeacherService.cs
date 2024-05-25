﻿using CourseProject.Core.Models;
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
        public TeacherService()
        {
            _teacherRepository = new TeacherRepository();
        }
        public async Task AddAsync()
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
            string input = Console.ReadLine();
            DateTime birthday;

            if (DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
            {
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in MM/DD/YYYY format.");
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

        public async Task DeleteAsync()
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

        public async Task GetAllAsync()
        {
            var t = new TablePrinter("Id", "Name", "Surname", "Birthday",  "Fincode");

            foreach (var teacher in await _teacherRepository.GetAllAsync())
            {
                t.AddRow(teacher.Id, teacher.Name, teacher.Surname, teacher.Birthday.ToString("DD-MM-YYYY"), teacher.FinCode);
            }
            t.Print();
        }

        public async Task GetAsync()
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
                t.AddRow(teacher.Id, teacher.Name, teacher.Surname, teacher.Birthday.ToString("DD-MM-YYYY"), teacher.FinCode);
                t.Print();
            }

        }

        public async Task UpdateAsync()
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
                DateTime birthday;
                Helper.HelperMessage(ConsoleColor.Magenta, "Enter teacher's birthday in format MM/DD/YYYY: ");
                birthday = DateTime.Parse(Console.ReadLine());
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
    }
}