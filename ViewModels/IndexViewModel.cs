using CSV.Controllers;
using CSV.Data;
using CSV.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSV.ViewModels
{
    public class IndexViewModel
    {
        public async static Task<List<Employee>> LoadFromData()
        {
            try
            {
                if (Employees.EmployeeList.Count != 0)
                {
                    return Employees.EmployeeList;
                }
                else
                {
                    EmployesController employesController = new EmployesController(new EmployeeContext());
                    var list = await employesController.Show();
                    Employees.EmployeeList.AddRange(list);
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public static async Task<List<Employee>> LoadFromCsv(string fileName)
        {
            List<Employee> employes = new List<Employee>();

            try
            {
                using (TextFieldParser csvParser = new TextFieldParser(fileName))
                {

                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    csvParser.ReadLine();

                    await Task.Run(() =>
                    {
                        while (!csvParser.EndOfData)
                        {
                            string[] fields = csvParser.ReadFields();

                            if (fields.Length == 5)
                            {
                                if (Convert.ToInt32(fields[0]) != 0 && fields[1] != null && fields[2] != null && fields[3] != null && Convert.ToInt64(fields[4]) != 0)
                                {
                                    Employee emp = new Employee
                                    {
                                        ID = Convert.ToInt32(fields[0]),
                                        Name = fields[1],
                                        Surname = fields[2],
                                        Email = fields[3],
                                        Phone = Convert.ToInt64(fields[4])
                                    };
                                    employes.Add(emp);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    });

                    EmployesController empoCtrl = new EmployesController(new EmployeeContext());
                    empoCtrl.SetManyInDb(employes);
                }

                employes.ForEach(emp =>
                {
                    var temp = Employees.EmployeeList.Find(e => e.ID == emp.ID);

                    if(temp == null)
                    {
                        Employees.EmployeeList.Add(emp);
                    }
                    else
                    {
                        temp.Name = emp.Name;
                        temp.Surname = emp.Surname;
                        temp.Phone = emp.Phone;
                        temp.Email = emp.Email;
                    }
                });
                
                return employes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}