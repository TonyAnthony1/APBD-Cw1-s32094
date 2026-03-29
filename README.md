# RentAgent - Uczelniana Wypożyczalnia Sprzętu

Aplikacja konsolowa w C# do obsługi wypożyczalni sprzętu uczelnianego.
Projekt na APBD, ćwiczenia 2.

## Uruchomienie

    dotnet run --project RentAgent

Wymagany .NET 10 SDK.

## Struktura projektu

  RentAgent/
├── Models/              Klasy domenowe (encje)
│   ├── ModelEquipments/ Typy sprzętu (Laptop, Phone, DisplayMonitor)
│   └── ModelUsers/      Typy użytkowników (Student, Employee)
├── Services/            Logika biznesowa
│   ├── Equipments/      Zarządzanie sprzętem
│   ├── Users/           Zarządzanie użytkownikami
│   └── Rentals/         Wypożyczenia, zwroty, raporty
├── Config/              Reguły biznesowe
├── Enums/               Statusy
└── Program.cs           Scenariusz demonstracyjny

## Podział klas i uzasadnienie

Projekt dzieli się na trzy warstwy: model domenowy (Models), logikę
biznesową (Services) i konfigurację reguł (Config). Program.cs pełni
rolę warstwy prezentacji — wywołuje serwisy i wypisuje wyniki.

Taki podział wybrałem dlatego, że każda warstwa zajmuje się czymś
innym i można ją zmieniać niezależnie. Dodanie nowego typu sprzętu
wymaga tylko nowej klasy w Models. Zmiana stawki kary wymaga edycji
jednego pliku w Config. 

## Kohezja

Każda klasa ma jedną odpowiedzialność. Equipment i jego podtypy
(Laptop, Phone, DisplayMonitor) przechowują dane sprzętu i wiedzą,
jak się opisać (GetDescription). System wypożyczeń wie, czy jest aktywne
i przeterminowane. RentalService zajmuje się wyłącznie logiką
wypożyczeń i zwrotów. EquipmentService zarządza kolekcją sprzętu.
UserService — użytkownikami. Żaden serwis nie mieszają się.

RentalPolicy w katalogu Config przechowuje stawkę kary (5 zł/dzień),
maksymalną karę (200 zł) i domyślny czas wypożyczenia (14 dni) w jednym
miejscu.

## Coupling

Serwisy komunikują się przez interfejsy: IEquipmentService,
IUserService, IRentalService. RentalService przyjmuje interfejsy
przez konstruktor, nie tworzy serwisów samodzielnie i nie zależy
od ich konkretnych implementacji.

## Dziedziczenie

Equipment jest klasą abstrakcyjną, z której dziedziczą Laptop, Phone
i DisplayMonitor. Każdy typ dzieli wspólne cechy (Id, Name, Status)
i dodaje własne pola (np. Laptop: Ram, Processor).

User jest klasą abstrakcyjną z podtypami Student (limit 2 wypożyczeń)
i Employee (limit 5). Limit jest zdefiniowany jako abstrakcyjna
właściwość MaxActiveRentals w typie użytkownika, nie jako stała
w serwisie, bo to cecha danego rodzaju użytkownika.
