namespace RentAgent.Models;

public class DisplayMonitor : Equipment
{
    public int RefreshRate { get; set; }
    public string Resolution { get; set; }

    public DisplayMonitor(string name, string description, int productionYear, int refreshRate, string resolution) : base(name, description, productionYear)
    {
        RefreshRate = refreshRate;
        Resolution = resolution;
    }

    public override string TypeName => "Monitor";

    public override string GetDescription()
    {
        return $"{this} | RefreshRate:  {Name} Hz, Resolution: {Resolution}";
    }
}