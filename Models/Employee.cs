﻿using UserTask.Enums;

namespace UserTask.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Status Status { get; set; }
}
