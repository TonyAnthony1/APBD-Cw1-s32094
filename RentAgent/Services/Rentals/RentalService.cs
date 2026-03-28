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
  

    public string Rent(int userId, int equipmentId, int rentalDays)
    {
        var user = _userService.FindUser(userId);
        if (user == null)
        {

            return "User not found.";
        }

        var equipment = _equipmentService.GetEquipmentById(equipmentId);
        if (equipment == null)
        {
            
            return "Equipment not found.";
        }

        if (!equipment.IsAvailable())
        {

            return $"Equipment \"{equipment.Name}\" is not available.";
        }

        var activeRentals = GetActiveRentalsForUser(userId);
        if (activeRentals.Count >= user.MaxActiveRentals)
        {

            return $"User \"{user.Id}\" has achived limit of rentals ({user.MaxActiveRentals}).";
        }

        var rental = new Rental(user,  DateTime.Now,equipment,DateTime.Now.AddDays(rentalDays));
        _rentals.Add(rental);
        equipment.Status = EquipmentStatus.Reserved;
        return $"Success: '{equipment.Name}' rented to {user.FirstName} {user.LastName} until {rental.DueDate}.";
    }

    public string ReturnEquipment(int rentalId, DateTime returnDate)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null)
        {
           
            return "Rental not found.";
        }

        if (!rental.Active)
        {
            return "Rental is finished.";
        }

        var overdueDays = (returnDate - rental.DueDate).Days;
        var penalty = overdueDays > 0 ? RentalPolicy.CalculatePenalty(overdueDays) : 0m;

        rental.CompleteReturn(returnDate, penalty);
        rental.Equipment.Status = EquipmentStatus.Available;

        if (penalty > 0)
            return $"Returned '{rental.Equipment.Name}' late by {overdueDays} day(s). Penalty: {penalty}";

        return $"Returned '{rental.Equipment.Name}' on time. No penalty.";
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