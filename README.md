# RentAgent - University Equipment Rental System

Console application in C# for managing university equipment rentals.

## How to run

    dotnet run --project RentAgent

Requires .NET 10 SDK.

## Project structure

    RentAgent/
    ├── Models/              Domain classes (entities)
    │   ├── ModelEquipments/ Equipment types (Laptop, Phone, DisplayMonitor)
    │   └── ModelUsers/      User types (Student, Employee)
    ├── Services/            Business logic
    │   ├── Equipments/      Equipment management
    │   ├── Users/           User management
    │   └── Rentals/         Rentals, returns, reports
    ├── Config/              Business rules
    ├── Enums/               Status enumerations
    └── Program.cs           Demo scenario

## Class structure and reasoning

The project is split into three layers: domain model (Models),
business logic (Services) and rule configuration (Config).
Program.cs acts as the presentation layer — it calls services
and prints results.

This separation was chosen so that each layer handles a different
concern and can be changed independently. Adding a new equipment
type only requires a new class in Models/ModelEquipments. Changing
the penalty rate only requires editing one file in Config.

## Cohesion

Each class has a single responsibility. Equipment and its subtypes
(Laptop, Phone, DisplayMonitor) store equipment data and know how
to describe themselves (GetDescription). Rental knows whether it is
active or overdue. RentalService handles only rental and return
logic. EquipmentService manages the equipment collection.
UserService manages users. No service mixes these responsibilities.

RentalPolicy in the Config folder stores the penalty rate (5 PLN/day),
maximum penalty (200 PLN) and default rental period (14 days) in one
place.

## Coupling

Services communicate through interfaces: IEquipmentService,
IUserService, IRentalService. RentalService receives interfaces
through its constructor, does not create services on its own and
does not depend on their concrete implementations.

## Inheritance

Equipment is an abstract class from which Laptop, Phone and
DisplayMonitor inherit. Each type shares common fields (Id, Name,
Status) and adds its own (e.g. Laptop: Ram, Processor).

User is an abstract class with subtypes Student (limit of 2 rentals)
and Employee (limit of 5). The limit is defined as an abstract
property MaxActiveRentals in the user type, not as a constant in
the service, because it is a characteristic of the user type itself.
