using PPBL;
using PPModel;

namespace PPUI
{
    
    public class ViewOrderHistoryMenu : IMenu
    {

        private static Customer _newCustomer = new Customer();
        private static StoreFront _newStore = new StoreFront();

        private static Orders _newOrder = new Orders();
        
        //Dependency injection
        private IPlanetPaintballBL _planetPaintballBL;
        private IPlanetPaintballStoresBL _planetPaintballStoresBL;
        public ViewOrderHistoryMenu(IPlanetPaintballBL p_planetPaintballBL, IPlanetPaintballStoresBL p_planetPaintballStoresBL)
        {
            _planetPaintballBL = p_planetPaintballBL;
            _planetPaintballStoresBL = p_planetPaintballStoresBL;
        }

        public void Display()
        {
            
            Console.WriteLine("===View Order History Menu===");
            Console.WriteLine("Did you want to view an order history?");
            Console.WriteLine("Enter Y for yes or N for no:");

        }

        public string UserChoice()
        {

            string userInput = Console.ReadLine().ToUpper();

            switch(userInput)
            {
                case "Y":
                    Console.WriteLine("How do you want to search?");
                    Console.WriteLine("1: Search a customer order history.");
                    Console.WriteLine("2: Search a store front order history.");
                    string searchMode = Console.ReadLine();
                    

                    if(searchMode == "1")
                    {
                        Log.Information("User is searching by email.");
                        searchMode = "email";
                        Console.WriteLine("Enter the email you want to search for:");
                        string customerEmail = Console.ReadLine();
                        Log.Information("User has entered in an email.");
                        try
                        {
                            List<Customer> listOfCustomers = _planetPaintballBL.SearchCustomer(searchMode, customerEmail);
                            Console.WriteLine("Customer found. Here is "+ listOfCustomers[0].Name +"'s order history information:");

                            List<Orders> listOfCustomerOrders = _planetPaintballStoresBL.GetOrders(searchMode, customerEmail);
                            if(listOfCustomerOrders.Count == 0)
                            {
                                Console.WriteLine("User has not made any orders yet.");
                            }
                            foreach(var item in listOfCustomerOrders)
                            {
                                Console.WriteLine(item);
                            }
                            
                            Log.Information("The order history is being displayed to the user.");
                            Console.WriteLine("Press any key to continue:");
                            Console.ReadLine();
                        }
                        catch (System.Exception exc)
                        {
                            Console.WriteLine(exc.Message);
                            Log.Warning("The order history was not able to be found. Information entered by user does not exist.");
                            Console.WriteLine("Please press any key to continue:");
                            Console.ReadLine();
                        }

                    }
                    else if(searchMode == "2")
                    {
                        Log.Information("User is searching by store address.");
                        searchMode = "storeAddress";
                        Console.WriteLine("Enter the store location you want to search for or if you need to see all stores, type \"all\":");
                        String storeLocation = Console.ReadLine();
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
                        try
                        {
                            List<StoreFront> listOfStores = _planetPaintballStoresBL.ViewInventory(storeLocation);
                            Console.WriteLine("Store found. Here is the order history of " + listOfStores[0].Name + " " + listOfStores[0].Address+ ":");
 
                            List<Orders> listOfStoreOrders = _planetPaintballStoresBL.GetOrders(searchMode, storeLocation);
                            if(listOfStoreOrders.Count == 0)
                            {
                                Console.WriteLine("Store has not had any orders yet.");
                            }
                            foreach(var item in listOfStoreOrders)
                            {
                                Console.WriteLine(item);
                            }

                            Log.Information("Displaying order history to user.");
                            Console.WriteLine("Press any key to continue:");
                            Console.ReadLine();
                        }  
                        catch(System.Exception exc)
                        {
                            Console.WriteLine(exc.Message);
                            Log.Warning("The order history was not able to be found. Information entered by user does not exist.");
                            Console.WriteLine("Please press any key to continue:");
                            Console.ReadLine();
                        }

                    }
                    else
                    {
                        Console.WriteLine("You did not enter a menu option!");
                        Log.Warning("User entered an illegal menu option.");
                        Console.WriteLine("Press any key to continue:");
                        Console.ReadLine();
                    }
                    
                    return "ViewOrderHistory";
                case "N":
                    Log.Information("User is going back to the main menu.");
                    return "MainMenu";
                default:
                    Console.WriteLine("Please input a valid response of Y or N.");
                    Log.Warning("User entered an illegal menu option.");
                    Console.WriteLine("Press any key to continue:");
                    Console.ReadLine();
                    return "ViewOrderHistoryMenu";

            }
            

        }

    }

}