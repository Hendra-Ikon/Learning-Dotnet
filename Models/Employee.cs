namespace EmployeeCRUD.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Alamat { get; set; }

        public Employee(string name, string position, string alamat)
        {
            Name = name;
            Position = position;
            Alamat = alamat;
        }
    }
}
