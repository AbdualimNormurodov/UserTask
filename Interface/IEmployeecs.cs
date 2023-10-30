using UserTask.Models;

namespace UserTask.Interface;

public interface IEmployeecs
{
    Employee Create(Employee employee);
    Employee Update(int id,Employee employee);
    bool Delete(int id);
    bool DeepDelete(int id);
    Employee GetById(int id);
    List<Employee> GetAll();
}
