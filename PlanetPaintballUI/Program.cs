global using Serilog;
using Microsoft.Extensions.Configuration;
using PPBL;
using PPDL;
using PPUI;


//creating and configuring our logger
//logger will save to user.txt in logs folder
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("./logs/user.txt") 
    .CreateLogger();

//reading and obtaining connectionString from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string _connectionString = configuration.GetConnectionString("Reference2DB");


bool repeat = true;

IMenu menu = new MainMenu();

while (repeat)
{

    Console.Clear();
    menu.Display();
    string ans = menu.UserChoice();

    switch (ans)
    {
        case "MainMenu":
            Log.Information("Displaying the MainMenu to user.");
            menu = new MainMenu();
            break;
        case "AddCustomer":
            Log.Information("Displaying the AddCustomerMenu to user.");
            menu = new AddCustomerMenu(new PlanetPaintballBL(new SQLRepository(_connectionString)));
            break;
        case "SearchCustomer":
            Log.Information("Displaying the SearchCustomerMenu to user.");
            menu = new SearchCustomerMenu(new PlanetPaintballBL(new SQLRepository(_connectionString)));
            break;
        case "ViewInventory":
            Log.Information("Displaying the ViewInventoryMenu to user.");
            menu = new ViewInventoryMenu(new PlanetPaintballStoresBL(new SQLRepository(_connectionString)));
            break;
        case "PlaceOrder":
            Log.Information("Displaying the PlaceOrderMenu to user.");
            menu = new PlaceOrderMenu(new PlanetPaintballBL(new SQLRepository(_connectionString)), new PlanetPaintballStoresBL(new SQLRepository(_connectionString)));
            break;
        case "ViewOrderHistory":
            Log.Information("Displaying the ViewOrderHistoyMenu to user.");
            menu = new ViewOrderHistoryMenu(new PlanetPaintballBL(new SQLRepository(_connectionString)), new PlanetPaintballStoresBL(new SQLRepository(_connectionString)));
            break;
        case "ReplenishInventory":
            Log.Information("Displaying the ReplenishInventoryMenu to user.");
            menu = new ReplenishInventoryMenu(new PlanetPaintballStoresBL(new SQLRepository(_connectionString)));
            break;
        case "Exit":
            Log.Information("Exiting application.");
            Log.CloseAndFlush(); //closes logger resource
            repeat = false;
            break;
        default:
            Log.Warning("User entered in an illegal menu option.");
            Console.WriteLine("Page does not exist!");
            break;
    }

}