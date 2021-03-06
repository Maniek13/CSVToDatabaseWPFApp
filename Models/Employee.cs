using CSV.Interfaces;

namespace CSV.Models
{
    public class Employee : IEmployee
    { 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
    }
}
