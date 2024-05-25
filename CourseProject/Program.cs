using CourseProject.Service.Services.Implementations;
using CourseProject.Service.Services.Interfaces;

IMenuService menu = new MenuService();

await menu.ShowMenuAsync();