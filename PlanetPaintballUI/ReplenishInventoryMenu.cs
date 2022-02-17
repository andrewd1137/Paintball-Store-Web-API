using PPBL;
using PPModel;

namespace PPUI
{
    
    public class ReplenishInventoryMenu : IMenu
    {

        private static StoreFront _newStore = new StoreFront();

        //Dependency injection
        private IPlanetPaintballStoresBL _planetPaintballStoresBL;
        public ReplenishInventoryMenu(IPlanetPaintballStoresBL p_planetPaintballStoresBL)
        {
            _planetPaintballStoresBL = p_planetPaintballStoresBL;
        }

        public void Display()
        {

            Console.WriteLine("===Replenish Inventory Menu===");
            Console.WriteLine("Did you want to replenish a store's inventory?");
            Console.WriteLine("Enter Y for yes or N for no:");

        }

        public string UserChoice()
        {

            string userInput = Console.ReadLine().ToUpper();

            switch (userInput)
            {
                case "Y":
                    Console.WriteLine("Enter the store address you wish to view and replenish or if you need to see all stores, type \"all\":");
                    string storeLocation = Console.ReadLine();
                    if(storeLocation == "all")
                    {
                        Log.Information("User asked to see all stores, showing them that infomation now.");
                        List<StoreFront> listOfAllStores = _planetPaintballStoresBL.ViewAllStores();
                        foreach (StoreFront item in listOfAllStores)
                        {
                            Console.WriteLine(item.printStoreInfo());
                        }
                        Console.WriteLine("====================\nFrom the list of stores above, type the address of the store you want to view.");
                        storeLocation = Console.ReadLine();
                    }
                    Log.Information("User entered in a store location.");
                    try
                    {
                        List<StoreFront> listOfStores = _planetPaintballStoresBL.ViewInventory(storeLocation);
                        Console.WriteLine("Here is the list of products that the store has:");
                        List<Products> listOfStoreProducts = _planetPaintballStoresBL.GetProductsByStoreAddress(storeLocation);
                        foreach(var item in listOfStoreProducts)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("Enter the ID for the item you wish to replenish:");
                        int replenishID = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User entered in a product id to replenish the inventory for.");
                        Console.WriteLine("Enter the amount of items you are adding to the quantity:");
                        int replenishAmount = Convert.ToInt32(Console.ReadLine());
                        Log.Information("User entered the amount they want to add to the total inventory of the product.");
                        _planetPaintballStoresBL.UpdateInventory(replenishID, replenishAmount);
                    }
                    catch(System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Log.Warning("The item the user entered has not been found.");
                        Console.WriteLine("Please press any key to continue:");
                        Console.ReadLine();
                    }
                    Console.WriteLine("Item has been replenished! Press any key to continue:");
                    Log.Information("Item successfully replenished.");
                    Console.ReadLine();
                    return "ReplenishInventory";
                case "N":
                    Log.Information("User is going back to the main menu.");
                    return "MainMenu";
                default:
                    Console.WriteLine("You entered an illegal character! Please try again.");
                    Log.Warning("User entered an illegal menu option.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadLine();
                    return "ViewInventory";
            }
        }

    }

}