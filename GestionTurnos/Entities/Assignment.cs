namespace GestionTurnos.Entities;

public class Assignment
{
    public int? Id { get; set; }
    public Client? Client { get; set; }
    public Driver? Driver { get; set; }
}
