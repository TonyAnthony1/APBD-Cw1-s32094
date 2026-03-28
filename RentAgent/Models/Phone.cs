namespace RentAgent.Models;

public class Phone : Equipment
{
    public int Weight {get; set;}
    public string Color { get; set; }

    public Phone(string name, string description, int productionYear, int weight, string color) : base(name,
        description, productionYear)
    {
        Weight = weight;
        Color = color;
    }

    public override string TypeName => "Phone";

    public override string GetDescription()
    {
        return $"{this} | Weight: {Weight} gram, Color: {Color}";
    }

    
}