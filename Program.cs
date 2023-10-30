using UserTask.Models;
using UserTask.Services;
using UserTask.Enums;
        //Employee newEmployee = new Employe
        //{
        //    Name = "John",
        //    Surname = "Doe",
        //    Email = "johndoe@example.com",
        //    Login = "johndoe",
        //    Password = "password123",
        //    Status = Status.Deleted
        //};

         EmployeeService employeeService = new EmployeeService();
//employeeService.Create(newEmployee);
//employeeService.DeepDelete(2);

//employeeService.Delete(3);


//EmployeeService employeeService = new EmployeeService();

//Employee employeeToUpdate = new Employee
//{
//    Name = "Abdualim",
//    Surname = "Normurodov",
//    Email = "abdualimnormurodov1@gmail.com",
//    Login = "Abdualim",
//    Password = "19788201",
//    Status = Status.Updated
//};

//employeeService.Update(4, employeeToUpdate);


var result = employeeService.GetAll();

foreach (var i in result)
{
    Console.WriteLine($"{i.Id}   {i.Name}   {i.Surname}   {i.Email}   {i.Login}   {i.Password}   {i.Status}");
}

//Employee employee = new Employee
//{
//    Name = "Ozod",
//    Surname = "Sharipov",
//    Password = "89798789",
//    Email = "sharapov@gmail.com",
//    Login = "OzodSharopov",
//    Status = Status.Created
//};
//employeeService.Create(employee);


