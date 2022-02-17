using PPDL;
using PPModel;

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

            public List<Customer> SearchCustomer(string searchMode, string p_string)
            {
                List<Customer> listOfCustomers = _repo.GetAllCustomers();
                
                //search by name (mode will = name) 
                if(searchMode == "name")
                {

                    var found = listOfCustomers.Find(p => p.Name.Contains(p_string));
                    if(found != null)
                    {
                        //validation process using LINQ Library
                        return listOfCustomers
                                .Where(customer => customer.Name.Contains(p_string))
                                .ToList();
                    }
                    else
                    {
                        throw new Exception("A customer with this name has not been found.");
                    }

                }
                
                //search by email (mode will = email)
                else if(searchMode == "email")
                {

                    var found = listOfCustomers.Find(p => p.Email == p_string);
                    if(found != null)
                    {
                        //validation process using LINQ Library
                        return listOfCustomers
                                .Where(customer => customer.Email.Equals(p_string))
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

    }

}