﻿using CourseProject.Core.Models;
using CourseProject.Service.Services.Interfaces;
using CourseProject.Service.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Service.Services.Implementations
{
    public class MenuService : IMenuService
    {
        public async Task ShowMenuAsync()
        {
            bool isContinue = true;
            while (isContinue)
            {
                PrintMenu();

                Console.Write("Enter operation number: ");
                int.TryParse(Console.ReadLine(), out int step);
                Console.Clear();

                switch (step)
                {
                    case 1:
                        IGroupService groupService = new GroupService();
                        await SubMenuAsync(groupService);
                        break;
                    case 2:
                        IStudentService studentService = new StudentService();
                        await SubMenuAsync(studentService);
                        break;
                    case 3:
                        ITeacherService teacherService = new TeacherService();
                        await SubMenuAsync(teacherService);
                        break;
                    case 4:
                        ISubjectService subjectService = new SubjectService();
                        await SubMenuAsync(subjectService);
                        break;
                    case 0:
                        isContinue = false;
                        break;
                    default:
                        Console.WriteLine("Enter valid operation number!!!");
                        break;

                }
            }
        }
        private async Task SubMenuAsync(IService service)
        {
            bool isContinue = true;
            string type = service.GetType().Name.Split("Service")[0];
            //bool result = service.GetType().ToString() == typeof(OrderService).ToString();

            while (isContinue)
            {
                PrintSubMenu(type);

                //if (result)
                //{
                //    Console.Write($"6.Change Status {type}s\n");
                //}

                Console.Write("Enter operation number: ");
                int.TryParse(Console.ReadLine(), out int step);
                Console.Clear();

                switch (step)
                {
                    case 1:
                        await service.AddAsync();
                        break;
                    case 2:
                        await service.UpdateAsync();
                        break;
                    case 3:
                        await service.DeleteAsync();
                        break;
                    case 4:
                        await service.GetAsync();
                        break;
                    case 5:
                        await service.GetAllAsync();
                        break;
                    case 0:
                        isContinue = false;
                        break;
                    default:
                        Console.WriteLine("Enter valid operation number!!!");
                        break;

                }
            }

        }

        public static void PrintMenu()
        {
            Helper.HelperMessage(ConsoleColor.Cyan, "---------------------------------------------COURSE APPLICATION-------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var t1 = new TablePrinter("1 - Group Menu");
            t1.AddRow("2 - Student Menu");
            t1.AddRow("3 - Teacher Menu");
            t1.AddRow("4 - Subject Menu");
            t1.AddRow("0 - EXIT");
           
            t1.PrintMenu();

            Console.ResetColor();
            Helper.HelperMessage(ConsoleColor.Cyan, "-----------------------------------------------------------------------------------------------------------");
        }
        public static void PrintSubMenu(string type)
        {
            Helper.HelperMessage(ConsoleColor.Cyan, "---------------------------------------------COURSE APPLICATION-------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var t1 = new TablePrinter($"1 - Add {type}");
            t1.AddRow($"2 - Update {type}");
            t1.AddRow($"3 - Delete {type}");
            t1.AddRow($"4 - Get {type} by Id");
            t1.AddRow($"5 - Get All {type}");
            t1.AddRow($"0 - EXIT {type} Menu");

            t1.PrintMenu();

            Console.ResetColor();
            Helper.HelperMessage(ConsoleColor.Cyan, "-----------------------------------------------------------------------------------------------------------");
        }
    }
}