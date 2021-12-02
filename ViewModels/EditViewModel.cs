﻿using CSV.Controllers;
using CSV.Data;
using CSV.Models;
using System;
using System.Linq;

namespace CSV.ViewModels
{
    internal class EditViewModel
    {
        public static bool Edit(Employee employee)
        {
            try
            {
                EmployesController empoCtrl = new EmployesController(new EmployeeContext());
                empoCtrl.UpdateOne(employee);

                var em = Employees.EmployeeList.FirstOrDefault(emp => emp.ID == employee.ID);
                em = employee;

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
