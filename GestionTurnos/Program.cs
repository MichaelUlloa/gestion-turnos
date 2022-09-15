using GestionTurnos.Entities;

namespace GestionTurnos;

internal class Program
{
    private static readonly Queue<Driver> _drivers = new(new Driver[]
    {
        new() 
        { 
            Name = "John Doe",
            IdentityNumber = "4021231234",
            Motorcycle = new()
            {
                Brand = "Ducati",
                Model = "Scrambler"
            }
        },
        new()
        {
            Name = "Jane Doe",
            IdentityNumber = "4021231234",
            Motorcycle = new()
            {
                Brand = "Yamaha",
                Model = "R 6"
            }
        },
        new()
        {
            Name = "Steve Doe",
            IdentityNumber = "4021231234",
            Motorcycle = new()
            {
                Brand = "Yamamha",
                Model = "FZ 07"
            }
        },
    });
    private static readonly List<Assignment> _assignmentsHistory = new();
    private static int _assignmentCounter = 1;

    static void Main()
    {
        bool shouldContinue = true;

        while (shouldContinue)
        {
            Console.Clear();
            Console.WriteLine("1 - Asignar turno a cliente\n2 - Agregar chofer\n3 - Mostrar choferes disponibles\n4 - Mostrar historial\n0 - Salir");
            string? input = Console.ReadLine();

            Console.Clear();

            switch (input)
            {
                case "0":
                    shouldContinue = false;
                    break;
                case "1":
                    AssignTurn();
                    break;
                case "2":
                    AddDriver();
                    break;
                case "3":
                    ShowAvailableDrivers();
                    break;
                case "4":
                    ShowHistory();
                    break;
                default:
                    Console.WriteLine("Opción incorrecta.\nPulse cualquier botón para continuar.");
                    Console.ReadKey(false);
                    break;
            }
        }
    }

    public static void AssignTurn()
    {
        if (_drivers.Count is 0)
        {
            Console.WriteLine("No hay conductores para asignar.");
            Console.WriteLine("Pulse cualquier botón para continuar.");
            Console.ReadKey(false);
            return;
        }

        Console.Write("Documento de Identidad: ");
        string? identityDocument = Console.ReadLine();

        Console.Write("Nombre: ");
        string? name = Console.ReadLine();

        var assignment = new Assignment()
        {
            Id = _assignmentCounter++,
            Client = new()
            {
                Name = name,
                IdentityNumber = identityDocument
            },
            Driver = _drivers.Dequeue(),
        };
        _assignmentsHistory.Add(assignment);

        Console.Clear();
        Console.WriteLine($"El turno actual es {assignment.Id}, asignado al conductor {assignment.Driver.Name}");
        Console.WriteLine("Pulse cualquier botón para continuar.");
        Console.ReadKey(false);
    }

    private static void AddDriver()
    {
        Console.Write("Documento de Identidad del conductor: ");
        string? identityDocument = Console.ReadLine();

        Console.Write("Nombre del conductor: ");
        string? name = Console.ReadLine();

        Console.Write("Marca de la moto: ");
        string? brand = Console.ReadLine();

        Console.Write("Modelo de la moto: ");
        string? model = Console.ReadLine();

        _drivers.Enqueue(new()
        {
            IdentityNumber = identityDocument,
            Name = name,
            Motorcycle = new()
            {
                Brand = brand,
                Model = model,
            },
        });

        Console.Clear();
        Console.WriteLine("Se ha agregado satisfactoriamente.\nPulse cualquier botón para continuar.");
        Console.ReadKey(false);
    }

    private static void ShowAvailableDrivers()
    {
        Console.WriteLine("Conductores disponibles: ");

        foreach (var driver in _drivers)
        {
            string? brand = driver.Motorcycle?.Brand;
            string? model = driver.Motorcycle?.Model;
            Console.WriteLine($"{driver.Name}, número de identidad {driver.IdentityNumber}. Motor marca {brand} modelo {model}");
        }

        Console.WriteLine("Pulse cualquier botón para continuar.");
        Console.ReadKey(false);
    }

    private static void ShowHistory()
    {
        Console.WriteLine("Historial de asignaciones: ");

        foreach (var assignment in _assignmentsHistory)
        {
            Console.WriteLine($"#{assignment.Id} - Cliente {assignment.Client?.Name} con conductor {assignment.Driver?.Name}");
        }

        Console.WriteLine("Pulse cualquier botón para continuar.");
        Console.ReadKey(false);
    }
}