using RentAgent.Models;

namespace RentAgent.Services.Rentals;

public interface IRentalService

{
    
    void RentEquipment(int userId, int equipmentId,int rentalDays);
    void ReturnEquipment(int userId, DateTime returnDate);
    List<Rental> GetActiveRentalsForUser(int userId);
    List<Rental> GetOverdueRentals();
    List<Rental> GetAllRentals();
    string GenerateReport();
}