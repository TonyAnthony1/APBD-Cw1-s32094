using RentAgent.Enums;
using RentAgent.Models;

namespace RentAgent.Services.Equipments;

public class EquipmentService : IEquipmentService
{
    private List<Equipment> _equipments = new();

    public void AddEquipment(Equipment equipment)
    {
        _equipments.Add(equipment);
    }

    public List<Equipment> GetEquipments()
    {
        return _equipments.ToList();
    }

    public List<Equipment> GetAvailableEquipments()
    {
        return _equipments.Where(e => e.IsAvailable()).ToList();
    }

    public Equipment GetEquipmentById(int id)
    {
        return _equipments.FirstOrDefault(e => e.Id == id);
    }

    public void MarkEquipmentAsUnavailable(Equipment equipment)
    {
        var found = GetEquipmentById(equipment.Id);
        if (found == null)
            Console.WriteLine("Equipment not found");
        if (found.Status == EquipmentStatus.Reserved)
            Console.WriteLine("Equipment is reserved at the moment");

        found.Status = EquipmentStatus.Unavailable;
    }
    
}