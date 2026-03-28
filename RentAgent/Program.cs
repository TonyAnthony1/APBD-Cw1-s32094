using RentAgent.Config;
using RentAgent.Models;
using RentAgent.Services.Equipments;
using RentAgent.Services.Rentals;
using RentAgent.Services.Users;


IEquipmentService equipmentService = new EquipmentService();
IUserService userService = new UserService();
IRentalService rentalService = new RentalService(equipmentService, userService);

Console.WriteLine("\n---ADDING EQUIPMENT---");

var laptop1 = new Laptop("ThinkPad X1", "ultrabook", 2023, 16, "Intel i7-1365U");
var laptop2 = new Laptop("MacBook Pro 14", "laptop", 2024, 32, "Apple M3 Pro");
var phone1 = new Phone("Galaxy S24", "smartphone", 2024, 168, "Black");
var phone2 = new Phone("iPhone 15 Pro", "smartphone", 2024, 187, "Silver");
var monitor1 = new DisplayMonitor("Dell U2723QE", "monitor", 2023, 60, "3840x2160");
var monitor2 = new DisplayMonitor("LG 27GP950", " monitor", 2023, 144, "3840x2160");

equipmentService.AddEquipment(laptop1);
equipmentService.AddEquipment(laptop2);
equipmentService.AddEquipment(phone1);
equipmentService.AddEquipment(phone2);
equipmentService.AddEquipment(monitor1);
equipmentService.AddEquipment(monitor2);

Console.WriteLine($"Added {laptop1.TypeName}: {laptop1.Name} (ID: {laptop1.Id.ToString()})");
Console.WriteLine($"Added {laptop2.TypeName}: {laptop2.Name} (ID: {laptop2.Id.ToString()})");
Console.WriteLine($"Added {phone1.TypeName}: {phone1.Name} (ID: {phone1.Id.ToString()})");
Console.WriteLine($"Added {phone2.TypeName}: {phone2.Name} (ID: {phone2.Id.ToString()})");
Console.WriteLine($"Added {monitor1.TypeName}: {monitor1.Name} (ID: {monitor1.Id.ToString()})");
Console.WriteLine($"Added {monitor2.TypeName}: {monitor2.Name} (ID: {monitor2.Id.ToString()})");

Console.WriteLine("\n---ADDING USERS---");

var student1 = new Student("Anna", "Kowalska", "s12345");
var student2 = new Student("Marek", "Nowak", "s67890");
var employee1 = new Employee("Joanna", "Wisniewska", "IT Department");
var employee2 = new Employee("Piotr", "Zielinski", "Mathematics Department");

userService.AddUser(student1);
userService.AddUser(student2);
userService.AddUser(employee1);
userService.AddUser(employee2);

Console.WriteLine($"Added {student1.UserType}: {student1.LastName} (ID: {student1.Id.ToString()})");
Console.WriteLine($"Added {student2.UserType}: {student2.LastName} (ID: {student2.Id.ToString()})");
Console.WriteLine($"Added {employee1.UserType}: {employee1.LastName} (ID: {employee1.Id.ToString()})");
Console.WriteLine($"Added {employee2.UserType}: {employee2.LastName} (ID: {employee2.Id.ToString()})");

Console.WriteLine("\n--- ALL EQUIPMENT ---");
foreach (var e in equipmentService.GetEquipments())
    Console.WriteLine($"{e.GetDescription()}");

Console.WriteLine("\n--- AVAILABLE EQUIPMENT ---");
foreach (var e in equipmentService.GetAvailableEquipments())
    Console.WriteLine($"  {e}");

Console.WriteLine("\n--- SUCCESSFUL RENTALS ---");

Console.WriteLine(rentalService.Rent(student1.Id, laptop1.Id, RentalPolicy.DefaultRentalDays));


Console.WriteLine(rentalService.Rent(employee1.Id, monitor1.Id, 7));


Console.WriteLine(rentalService.Rent(employee1.Id, phone1.Id, 10));


Console.WriteLine("\n--- INVALID OPERATIONS ---");

Console.WriteLine(rentalService.Rent(student2.Id, laptop1.Id, 7));

Console.WriteLine(rentalService.Rent(student1.Id, phone2.Id, 14));

Console.WriteLine(rentalService.Rent(student1.Id, laptop2.Id, 7));

equipmentService.MarkEquipmentAsUnavailable(monitor2);

Console.WriteLine(rentalService.Rent(student2.Id, monitor2.Id, 7));


Console.WriteLine("\n--- ON-TIME RETURN ---");

var allRentals = rentalService.GetAllRentals();
var rentalOnTime = allRentals.First(r => r.Equipment.Id == monitor1.Id && r.Active);
var onTimeDate = rentalOnTime.DueDate.AddDays(-2);

Console.WriteLine(rentalService.ReturnEquipment(rentalOnTime.Id, onTimeDate));


Console.WriteLine("\n--- LATE RETURN ---");

var rentalLate = allRentals.First(r => r.Equipment.Id == phone1.Id && r.Active);
var lateDate = rentalLate.DueDate.AddDays(5);

Console.WriteLine(rentalService.ReturnEquipment(rentalLate.Id, lateDate));

Console.WriteLine("\n--- OVERDUE RENTALS ---");
rentalService.Rent(employee2.Id, phone2.Id, RentalPolicy.DefaultRentalDays);

var overdue = rentalService.GetOverdueRentals();
if (overdue.Count == 0)
    Console.WriteLine("No overdue rentals.");
else
    foreach (var r in overdue)
        Console.WriteLine($"{r} (overdue: {r.GetOverdueDays().ToString()} days)");


Console.WriteLine(rentalService.GenerateReport());

