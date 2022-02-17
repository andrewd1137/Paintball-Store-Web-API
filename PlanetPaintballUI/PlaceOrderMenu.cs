using PPBL;
using PPModel;

namespace PPUI
{
    
    public class PlaceOrderMenu : IMenu
    {

        private static Customer _newCustomer = new Customer();
        private static Orders _newOrder = new Orders();
        private static StoreFront _newStore = new StoreFront();
        private LineItems _newLineItem = new LineItems();

        //Dependency injection
        private IPlanetPaintballBL _planetPaintballBL;
        private IPlanetPaintballStoresBL _planetPaintballStoresBL;
        public PlaceOrderMenu(IPlanetPaintballBL p_planetPaintballBL, IPlanetPaintballStoresBL p_planetPaintballStoresBL)
        {
            _planetPaintballBL = p_planetPaintballBL;
            _planetPaintballStoresBL = p_planetPaintballStoresBL;
        }

        public void Display()
        {

            Console.WriteLine("===Place Order Menu===");
            Console.WriteLine("Did you want to place an order?");
            Console.WriteLine("Enter Y for yes or N for no:");
            
        }

        public string UserChoice()
        {

            string userInput = Console.ReadLine().ToUpper();

            switch(userInput)
            {
                case "Y":
                    Console.WriteLine("Enter your customer email:");
                    string customerEmail = Console.ReadLine();
                    Log.Information("User entered in their email.");
                    try
                    {
                        List<Customer> listOfCustomers = _planetPaintballBL.SearchCustomer("email", customerEmail);
                        //listOfCustomers[0] = the customer's name
                        Console.WriteLine("Shopping as: " + listOfCustomers[0].Name);
                    }
                    catch (System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Log.Warning("User entered in an email that does not exist.");
                        Console.WriteLine("Taking you back to the Place Order Menu.");
                        Console.WriteLine("Please press any key to continue:");
                        Console.ReadLine();
                        return "PlaceOrder";
                    }


                    Console.WriteLine("Enter the Store location You wish to buy from or if you need to see all stores, type \"all\":");
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
                        Console.WriteLine("Here is the list of possible products you can order:");
                        List<Products> listOfStoreProducts = _planetPaintballStoresBL.GetProductsByStoreAddress(storeLocation);
                        if(listOfStoreProducts.Count == 0)
                        {
                            Console.WriteLine("This store has no products at this time.");
                            Console.WriteLine("Taking you back to the Place Order Menu");
                            Console.WriteLine("Please press any key to continue: ");
                            Console.ReadLine();
                            return "PlaceOrder";
                        }
                        else
                        {
                            foreach(var item in listOfStoreProducts)
                            {
                                Console.WriteLine(item);
                            }
                            Log.Information("Displayed list of items the user can buy.");
                        }
                        
                    }
                    catch(System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Log.Warning("Could not find store user was looking for.");
                        Console.WriteLine("Taking you back to the Place Order Menu.");
                        Console.WriteLine("Please press any key to continue:");
                        Console.ReadLine();
                        return "PlaceOrder";
                    }

                    List<LineItems> itemsOrdered = new List<LineItems>();
                    bool userIsShopping = true; 
                    while (userIsShopping)
                    {
                
                        Console.WriteLine("What do you want to do?");
                        Console.WriteLine("1: Add a product to my order.");
                        Console.WriteLine("2: View my current Order.");
                        Console.WriteLine("3: Check out.");
                        Console.WriteLine("4: Cancel order(will bring you back to the main menu).");
                        string orderMode = Console.ReadLine();

                        if(orderMode == "1")
                        {
                            Log.Information("User is adding an item to their order.");
                            Console.WriteLine("Please enter in the ID number for the item you want to add to your cart:");
                            int itemIDNum = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                Console.WriteLine("How many would you like to buy?");
                                int quantityOrdered = Convert.ToInt32(Console.ReadLine());
                                
                                if(_planetPaintballStoresBL.TestQuantity(itemIDNum, quantityOrdered))
                                {
                                    LineItems _newLineItem = new LineItems();
                                    _newLineItem.ProductID = itemIDNum;
                                    _newLineItem.ProductQuantity = quantityOrdered;
                                    
                                    itemsOrdered.Add(_newLineItem);
                                    Log.Information("The item has been added.");
                                    Console.WriteLine("Adding your item!");
                                }
                                else
                                {
                                    Console.WriteLine("The quantity for the item you tried adding does not have enough in stock! Please re-add the item with available quantity.");
                                    Console.WriteLine("Please press any key to continue: ");
                                    Console.ReadLine();
                                
                                }
                                
                            }
                            catch (System.Exception exc)
                            {
                                Console.WriteLine(exc.Message);
                                Log.Warning("The item the user added was not found.");
                                Console.WriteLine("Please press any key to continue:");
                                Console.ReadLine();
                            }

                        }
                        else if(orderMode == "2")
                        {
                            try
                            {
                                Log.Information("User is viewing their current order.");
                                Console.WriteLine("Here is your current order:");
                                //declare some index value since view order takes in an index and does one product at a time
                                int index = 0;
                                //will be used to calculate the total cost of items in the user's cart
                                decimal cartTotal = 0;
                                foreach(var itemInOrder in itemsOrdered)
                                {
                                    List<Products> listOfItemsOrdered = _planetPaintballStoresBL.ViewOrder(itemInOrder.ProductID, storeLocation);
                                        foreach(var items in listOfItemsOrdered)
                                        {
                                            int itemNum = index + 1;
                                            Console.WriteLine("\nItem " + itemNum + ":");
                                            Console.WriteLine("ID: " + items.ID);
                                            Console.WriteLine("Name: " + items.Name);
                                            Console.WriteLine("Price: $" + items.Price.ToString("0.00"));
                                            Console.WriteLine("Amount in your cart: " + itemInOrder.ProductQuantity);
                                            
                                            //will calculate the total cost of the items based on how many were purchased
                                            cartTotal = cartTotal + (items.Price * itemInOrder.ProductQuantity);
                                            index ++;
                                        }
                                }
                                
                                //display the total cost for their current cart
                                Console.WriteLine("\nCart Total: $" + cartTotal.ToString("0.00"));
                                Log.Information("The current order has been displayed to the user.");

                                Console.WriteLine("Please press any key to continue:");
                                Console.ReadLine(); 
                            }
                            catch (System.Exception exc)
                            {
                                Console.WriteLine(exc.Message);
                                Log.Warning("User order was not able to be displayed.");
                                Console.WriteLine("Please press any key to continue:");
                                Console.ReadLine();
                            }            
                        }
                        else if (orderMode == "3")
                        {

                            Log.Information("User is finalizing their order and checking out.");
                            userIsShopping = false;
                            Console.WriteLine("Checking out!");

                            Console.WriteLine("Here was your completed order:");
                            //declare some index value since view order takes in an index and does one product at a time
                            int index = 0;
                            //will be used to calculate the total cost of items in the user's cart
                            decimal cartTotal = 0;

                            //display what the user purchased as well as their cart total, then store that total in orders obj
                            foreach(var itemInOrder in itemsOrdered)
                            {
                                List<Products> listOfItemsOrdered = _planetPaintballStoresBL.ViewOrder(itemInOrder.ProductID, storeLocation);
                                    foreach(var items in listOfItemsOrdered)
                                    {
                                        int itemNum = index + 1;
                                        Console.WriteLine("\nItem " + itemNum + ":");
                                        Console.WriteLine("ID: " + items.ID);
                                        Console.WriteLine("Name: " + items.Name);
                                        Console.WriteLine("Price: $" + items.Price.ToString("0.00"));
                                        Console.WriteLine("Amount in your cart: " + itemInOrder.ProductQuantity);
                                        
                                        //will calculate the total cost of the items based on how many were purchased
                                        cartTotal = cartTotal + (items.Price * itemInOrder.ProductQuantity);
                                        index ++;
                                    }

                                
                            }
                    
                            //display the total cost for their current cart
                            Console.WriteLine("\nTotal Spent: $" + cartTotal.ToString("0.00"));
                            Log.Information("Final order has been displayed to the user.");

                            List<Customer> listOfCustomers = _planetPaintballBL.SearchCustomer("email", customerEmail);
                            List<StoreFront> listOfStores = _planetPaintballStoresBL.ViewInventory(storeLocation);
                            _newOrder.CustomerID = listOfCustomers[0].ID;
                            _newOrder.StoreID = listOfStores[0].ID;
                            _newOrder.orderTotalCost = cartTotal;
                            _newOrder.LineItems = itemsOrdered;
                            
                            if(cartTotal == 0.00m)
                            {
                                Console.WriteLine("Cannot place order with 0 items in cart! Please add at least one item before checking out.");
                                Console.WriteLine("Press any key to continue:");
                                userIsShopping = true;
                                Console.ReadLine();
                            }
                            else
                            {

                                _planetPaintballStoresBL.StartOrder(_newOrder);
                                foreach(var item in itemsOrdered)
                                {
                                    try
                                    {
                                        _planetPaintballStoresBL.MakeOrder(item, _newOrder.OrderID);
                                    }
                                    catch(System.Exception exc)
                                    {
                                        Console.WriteLine(exc.Message);
                                        Log.Warning("User order was not able to be made. User most likely entered more items than available");
                                        Console.WriteLine("Please press any key to continue:");
                                        Console.ReadLine();
                                    }
                                }
                                Log.Information("Order has been made.");
                                Console.WriteLine("Please press any key to continue:");
                                Console.ReadLine();
                            }

                        }
                        else if (orderMode == "4")
                        {
                            Console.WriteLine("Your order has been cancelled. Taking you back to the main menu.");
                            Console.WriteLine("Press any key to continue: ");
                            Console.ReadLine();
                            return "MainMenu";
                        }
                        else
                        {
                            Console.WriteLine("Please input a valid menu option of 1,2, 3 or 4.");
                            Log.Information("User has entered in an illegal menu option.");
                            Console.WriteLine("Press any key to continue:");
                            Console.ReadLine();
                        }


                    }
                    return "PlaceOrder";

                case "N":
                    Log.Information("User is going back to the main menu.");
                    return "MainMenu";
                default:
                    Console.WriteLine("Please input a valid response of Y or N.");
                    Log.Information("User has entered in an illegal menu option.");
                    Console.WriteLine("Press any key to continue:");
                    Console.ReadLine();
                    return "PlaceOrder";
            }


        }

    }

}