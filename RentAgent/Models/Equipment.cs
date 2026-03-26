using RentAgent.Enums;

namespace RentAgent.Models;

public abstract class Equipment
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductionYear { get; set; }
    public EquipmentStatus Status { get; set; }
    
    private static int _nextId = 1;

    protected Equipment(string name, string description, int productionYear)
    {
        Id = _nextId++;
        Name = name;
        Description = description;
        ProductionYear = productionYear;
        Status = EquipmentStatus.Available;
    }
    
    public bool IsAvailable(){
        return Status == EquipmentStatus.Available;
    }
    
    public abstract string GetDescription();
    public abstract string TypeName { get; }

    public override string ToString()
    {
        return $"[{Id}] {TypeName}: {Name} ({Description}, {ProductionYear}) - Status: {Status}";
    }
}