using ElevatorSystem.Models.Panels;

namespace ElevatorSystem.Models;

public class Floor
{
    public int Number{get;}
    public ExternalPanel ExternalPanel{get;}
    public Floor(int number)
    {
        Number = number;
        ExternalPanel = new ExternalPanel(number);
    }
}