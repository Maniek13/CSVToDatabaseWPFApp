using CSV.Data;
using CSV.Models;
using System;
using System.Collections.Generic;

namespace CSV.Controllers
{
    class EmployesController
    {
        private readonly EmployeeContext _context;

        public EmployesController(EmployeeContext context)
        {
            _context = context;
        }

        public void SetManyInDb(List<Employee> employe)
        {
            try
            {
                _context.Employes.AddRange(employe);
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ContextMarshalException(e.Message);
            }
        }

        public void UpdateOne(Employee employee)
        {
            try
            {
                Employee ec = _context.Employes.Find(employee.ID);
                _context.Employes.Remove(ec);
                _context.Employes.Add(employee);
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ContextMarshalException(e.Message);
            }
        }
    }
}
