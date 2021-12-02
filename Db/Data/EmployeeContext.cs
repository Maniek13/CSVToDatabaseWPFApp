using CSV.DBModels;
using System.Data.Entity;

namespace CSV.Data
{
    class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("CSVFiles")
        {
        }

        public DbSet<Employee> Employes { get; set; }

    }
}
