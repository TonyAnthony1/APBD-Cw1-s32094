namespace RentAgent.Models;

public class Employee : User
{
    public string Department {get; set;}

    public Employee(string firstName, string lastName, string department)
        : base(firstName, lastName)
    {
        Department = department;
    }

    public override string UserType => "Employee";
    public override int MaxActiveRentals => 5;

    public override string ToString()
    {
        return $"{base.ToString()} | Department: {Department}";
    }
}