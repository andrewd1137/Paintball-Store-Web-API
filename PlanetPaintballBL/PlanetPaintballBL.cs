using PPDL;
using PPModel;
using System.Text.RegularExpressions;

namespace PPBL
{

    public class PlanetPaintballBL : IPlanetPaintballBL
    {

            private IRepository _repo;

            public PlanetPaintballBL(IRepository p_repo)
            {
                _repo = p_repo;
            }

            public Customer AddCustomer(Customer p_customer)
            {
                //Check simple regex for customer name, address and email
                if(Regex.IsMatch(p_customer.Name, @"^[a-z A-Z]+$") == false)
                {
                    throw new Exception("Invalid entry for customer name. Cannot include illegal characters.");
                }
                if(Regex.IsMatch(p_customer.Address, @"^[#.0-9a-zA-Z\s,-]+$") == false)
                {
                    throw new Exception("Invalid entry for customer address. Cannot include illegal characters.");
                }
                if(Regex.IsMatch(p_customer.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") == false)
                {
                    throw new Exception("Invalid entry for customer email. Cannot include illegal characters.");
                }

                //get the list of customers and then add a new customer to the list
                List<Customer> listOfCustomers = _repo.GetAllCustomers();
                var found = listOfCustomers.Find(p => p.Email == p_customer.Email);
                if(found != null)
                {
                    throw new Exception("A customer with this email already exists! Customer emails must be unique.");
                }
                else
                {   
                    return _repo.AddCustomer(p_customer);
                }

            }

        public List<Customer> GetCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public List<Customer> SearchCustomer(string p_customerInfo)
        {
            List<Customer> listOfCustomers = _repo.GetAllCustomers();
            
            //will search for a customer by name if the regex for name matches 
            if(Regex.IsMatch(p_customerInfo, @"^[a-z A-Z]+$") == true)
            {

                var found = listOfCustomers.Find(p => p.Name.Contains(p_customerInfo));
                if(found != null)
                {
                    //validation process using LINQ Library
                    return listOfCustomers
                            .Where(customer => customer.Name.Contains(p_customerInfo))
                            .ToList();
                }
                else
                {
                    throw new Exception("A customer with this name has not been found.");
                }

            }
            
            //will search for a customer by email if the regex for email matches
            else if(Regex.IsMatch(p_customerInfo, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") == true)
            {

                var found = listOfCustomers.Find(p => p.Email == p_customerInfo);
                if(found != null)
                {
                    //validation process using LINQ Library
                    return listOfCustomers
                            .Where(customer => customer.Email.Equals(p_customerInfo))
                            .ToList();
                }
                else
                {
                    throw new Exception("A customer with this email has not been found.");
                }                    

            }

            //If trying to add new search ways and this error ever happens, 
            //make sure that you typed the searchMode string correctly in the SearchCustomerMenu.
            //Otherwise if strings in this searchMode match the string passed by the menu, then this
            //exception should never run unless user's pc is messed up. 
            else
            {
                throw new Exception("Could not search for customer! Some error has occurred. Try restarting program.");
            }
            
        }

        public List<Customer> VerifyCustomer(string p_customerEmail, string p_customerPassword)
        {
            List<Customer> listOfCustomers = _repo.GetAllCustomers();
            var found = listOfCustomers.Find(p => p.Email.Equals(p_customerEmail) && p.Password.Equals(p_customerPassword));
            if(found != null)
            {
                return listOfCustomers
                        .Where(customer => customer.Email.Equals(p_customerEmail) && customer.Password.Equals(p_customerPassword))
                        .ToList();
            }
            else
            {
                throw new Exception("Could not find customer with these credentials!");
            }
        }
    }

}