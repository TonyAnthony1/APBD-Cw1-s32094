namespace RentAgent.Models;

public class Laptop : Equipment
{
    public int Ram{get;set;}
    public string Processor{get;set;}

    public Laptop(string name, string description, int productionYear, int ram, string processor) : base(name, description, productionYear)
    {
        Ram = ram;
        Processor = processor;
    }
    
    public override string TypeName => "Laptop";

    public override string GetDescription()
    {
        return $"{this} | RAM: {Ram} GB, Procesor: {Processor}";
    }
    
}