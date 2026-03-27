using RentAgent.Models;

namespace RentAgent.Services.Equipments;

public interface IEquipmentService
{
    public List<Equipment> GetEquipments();
    public List<Equipment> GetAvailableEquipments();
    Equipment GetEquipmentById(int id);
    public void AddEquipment(Equipment equipment);
    public void MarkEquipmentAsUnavailable(Equipment equipment);
    
    
    
}