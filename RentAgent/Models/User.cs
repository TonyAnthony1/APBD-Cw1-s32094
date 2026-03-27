namespace RentAgent.Models;

public abstract class User
{
    
    public  int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    private static int _nextId = 1;

    protected User(string firstName, string lastName)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
    }

    public abstract string UserType { get; }
    public abstract int MaxActiveRentals { get; }

    public string FullName => $"{FirstName} {LastName}";

    public override string ToString()
    {
        return $"[{Id}] {UserType}: {FullName} (max rental: {MaxActiveRentals})";
    }
}