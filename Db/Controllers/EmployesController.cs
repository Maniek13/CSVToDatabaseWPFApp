using CSV.Data;
using CSV.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CSV.Controllers
{
    class EmployesController
    {
        private readonly EmployeeContext _context;

        public EmployesController(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Employee>> Show()
        {
            try
            {
                List<Models.Employee> employees = new List<Models.Employee>();
                var list =  await _context.Employes.ToListAsync<Employee>();

                foreach(Employee employee in list)
                {
                    Models.Employee emp = new Models.Employee
                    {
                        ID = employee.ID,
                        Name = employee.Name,
                        Surname = employee.Surname,
                        Email = employee.Email,
                        Phone = employee.Phone
                    };
                    employees.Add(emp);
                }

                return employees;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SetManyInDb(List<Models.Employee> employe)
        {
            try
            {
                employe.ForEach(el =>
                {
                    Employee emp = new Employee
                    {
                        ID = el.ID,
                        Name = el.Name,
                        Surname = el.Surname,
                        Email = el.Email,
                        Phone = el.Phone
                    };
                    Employee e = _context.Employes.Find(el.ID);

                    if (e != null)
                    {
                        _context.Employes.Remove(e);
                        _context.Employes.Add(emp);
                    }
                    else
                    {
                        _context.Employes.Add(emp);
                    }
                });
                _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateOne(Models.Employee employee)
        {
            try
            {
                Employee emp = new Employee
                {
                    ID = employee.ID,
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Email = employee.Email,
                    Phone = employee.Phone
                };

                Employee ec = _context.Employes.Find(emp.ID);
                _context.Employes.Remove(ec);
                _context.Employes.Add(emp);
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
