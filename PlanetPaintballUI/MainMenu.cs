namespace PPUI
{

    public class MainMenu : IMenu
    {

        public void Display()
        {

            Console.WriteLine("Welcome to PLanet Paintball's Store!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1: Add a customer.");
            Console.WriteLine("2: Search for a customer.");
            Console.WriteLine("3: View a Store's inventory.");
            Console.WriteLine("4: Place an order.");
            Console.WriteLine("5: View order history.");
            Console.WriteLine("6: Replenish inventory.");
            Console.WriteLine("7: Quit the application.");

        }

        public string UserChoice()
        {

            string userInput = Console.ReadLine();
            
            switch (userInput)
            {                
                case "1":
                    return "AddCustomer";
                case "2":
                    return "SearchCustomer";
                case "3":
                    return "ViewInventory";
                case "4":
                    return "PlaceOrder";
                case "5":
                    return "ViewOrderHistory";
                case "6":
                    return "ReplenishInventory";
                case "7":
                    Console.Clear();
                    Console.WriteLine("Exiting program...");
                    Console.WriteLine("Goodbye.");
                    return "Exit";
                default:
                    Console.WriteLine("Please input a valid response number!");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    return "MainMenu";
            }

        }
        

    }

}