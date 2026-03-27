using System.Text;
using RentAgent.Config;
using RentAgent.Enums;
using RentAgent.Models;
using RentAgent.Services.Equipments;
using RentAgent.Services.Users;

namespace RentAgent.Services.Rentals;

public class RentalService: IRentalService
{
    private readonly List<Rental> _rentals = new();
    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;

    public RentalService(IEquipmentService equipmentService, IUserService userService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
    }
  

    public void RentEquipment(int userId, int equipmentId, int rentalDays)
    {
        var user = _userService.GetById(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        var equipment = _equipmentService.GetEquipmentById(equipmentId);
        if (equipment == null)
        {
            Console.WriteLine("Equipment not found.");
            return;
        }

        if (!equipment.IsAvailable())
        {
            Console.WriteLine($"Equipment \"{equipment.Name}\" is not available.");
            return;
        }

        var activeRentals = GetActiveRentalsForUser(userId);
        if (activeRentals.Count >= user.MaxActiveRentals)
        {
            Console.WriteLine($"User \"{(object)user.Id}\" has achived limit of rentals  ({(object)user.MaxActiveRentals}).");            return;
        }

        var rental = new Rental(user,  DateTime.Now,equipment,DateTime.Now.AddDays(rentalDays));
        _rentals.Add(rental);
        equipment.Status = EquipmentStatus.Reserved;
    }

    public void ReturnEquipment(int rentalId, DateTime returnDate)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null)
        {
            Console.WriteLine("Rental not found.");
            return;
        }

        if (!rental.Active)
        {
            Console.WriteLine("Rental is finished.");
            return;
        }

        var overdueDays = (returnDate - rental.DueDate).Days;
        var penalty = overdueDays > 0 ? RentalPolicy.CalculatePenalty(overdueDays) : 0m;

        rental.CompleteReturn(returnDate, penalty);
        rental.Equipment.Status = EquipmentStatus.Available;
    }

    public List<Rental> GetActiveRentalsForUser(int userId)
    {
        return _rentals.Where(r => r.User.Id == userId && r.Active).ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return _rentals.Where(r => r.Overdue).ToList();
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals.ToList();
    }

    public string GenerateReport()
    {
        var sb = new StringBuilder();
        var allEquipment = _equipmentService.GetEquipments();

        sb.AppendLine("---Rent Report---");
        sb.AppendLine($"All eqiupments: {allEquipment.Count}");
        sb.AppendLine($"Available: {allEquipment.Count(e => e.IsAvailable())}");
        sb.AppendLine($"Active Rentals: {_rentals.Count(r => r.Active)}");
        sb.AppendLine($"Overdue: {GetOverdueRentals().Count}");
        sb.AppendLine($"Penalty sum: {_rentals.Sum(r => r.PenaltyFee):C}");

        return sb.ToString();
    }
}