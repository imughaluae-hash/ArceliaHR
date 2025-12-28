using ArceliaHR.Models;

namespace ArceliaHR.Database.Repositories
{
    public interface IEmployeeRepository
    {
        void Add(EmployeeModel employee);
        IEnumerable<EmployeeModel> GetAll();
        EmployeeModel GetById(int id);
        void Delete(int  id);
    }

}
