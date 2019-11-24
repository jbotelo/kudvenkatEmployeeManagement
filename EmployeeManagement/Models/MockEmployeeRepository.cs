using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeelist;

        public MockEmployeeRepository()
        {
            _employeelist = new List<Employee>()
            {
                new Employee(){Id=1,Name="Mary",Department="HR",Email="mary@abc.abc"},
                new Employee(){Id=2,Name="John",Department="IT",Email="john@abc.abc"},
                new Employee(){Id=3,Name="Sam",Department="IT",Email="sam@abc.abc"},

            };
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeelist;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeelist.FirstOrDefault(e=>e.Id==Id);
        }
    }
}
