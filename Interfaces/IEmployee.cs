using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV.Interfaces
{
    interface IEmployee
    {
        int ID { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        long Phone { get; set; }
    }
}
