using RentAgent.Models;

namespace RentAgent.Services.Rentals;

public interface IRentalService

{
    
    string Rent(int userId, int equipmentId,int rentalDays);
    string ReturnEquipment(int userId, DateTime returnDate);
    List<Rental> GetActiveRentalsForUser(int userId);
    List<Rental> GetOverdueRentals();
    List<Rental> GetAllRentals();
    string GenerateReport();
}