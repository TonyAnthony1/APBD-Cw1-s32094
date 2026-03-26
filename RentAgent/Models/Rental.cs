namespace RentAgent.Models;

public class Rental
{
    public int Id { get; }
    public User User { get; }
    public DateTime RentDate { get; }
    public Equipment Equipment { get; }
    
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; set; }
    public decimal PenaltyFee { get; set; }

    private static int _nextId = 1;
    public Rental(User user, DateTime rentDate, Equipment equipment, DateTime dueDate)
    {
        Id = _nextId++;
        User = user;
        RentDate = rentDate;
        Equipment = equipment;
        DueDate = dueDate;
        ReturnDate = null;
        PenaltyFee = 0;
    }
    public bool Active => ReturnDate == null;

    public bool Overdue => Active && DateTime.Now > DueDate;

    public bool ReturnedLate => (ReturnDate != null) && (ReturnDate > DueDate);

    public void CompleteReturn(DateTime returnDate, decimal penaltyFee)
    {
        ReturnDate = returnDate;
        PenaltyFee = penaltyFee;
    }
    
    public int GetOverdueDays()
    {
        if (Active)
        {
            var overdue = (DateTime.Now - DueDate).Days;
            return overdue > 0 ? overdue : 0;
        }

        if (ReturnDate != null)
        {
            var overdue = (ReturnDate.Value - DueDate).Days;
            return overdue > 0 ? overdue : 0;
        }

        return 0;
    }

    public override string ToString()
    {
        var status = Active ? "Active" : "Finished";
        var penalty = PenaltyFee > 0 ? $" | Penalty: {PenaltyFee:C}" : "";
        var returnInfo = ReturnDate.HasValue ? $" | Return: {ReturnDate.Value:d}" : "";
        return $"[{Id}] {status} | {User.FullName} -> {Equipment.Name} | " +
               $"Od: {RentDate:d} | Termin: {DueDate:d}{returnInfo}{penalty}";
    }
    
    
    
    
}