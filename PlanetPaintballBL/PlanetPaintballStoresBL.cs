using System.Text.RegularExpressions;
using PPDL;
using PPModel;

namespace PPBL
{

    public class PlanetPaintballStoresBL : IPlanetPaintballStoresBL
    {

        private IRepository _repo;

        public PlanetPaintballStoresBL(IRepository p_repo)
        {
            _repo = p_repo;
        }

        public List<StoreFront> ViewAllStores()
        {

            List<StoreFront> listOfAllStores = _repo.GetStoreFronts();
            return listOfAllStores;
        
        }

        public List<StoreFront> ViewInventory(string p_address)
        {

            List<StoreFront> listOfStores = _repo.GetStoreFronts();

            var found = listOfStores.Find(p => p.Address.Equals(p_address));
            if(found != null)
            {
                //validation process using LINQ Library
                return listOfStores
                    .Where(store => store.Address.Equals(p_address))
                    .ToList();
            }
            else
            {
                throw new Exception("A store with this address has not been found.");
            }

        }

        public List<Products> GetProductsByStoreAddress(string p_address)
        {
            return _repo.GetProductsByStoreAddress(p_address);
        }

        public void UpdateInventory(int p_storeID, int p_productID, int p_quantity)
        {
            _repo.UpdateInventory(p_storeID, p_productID, p_quantity);
        }

        public Orders MakeAnOrder(Orders p_order)
        {
            return _repo.MakeAnOrder(p_order);
        }

        public List<Orders> GetOrders(string searchedString, string searchMode)
        {
            List<Orders> filterOrders = new List<Orders>();
            List<Orders> listAllOrder =  _repo.GetAllOrders();
            List<Products> ListAllProducts = _repo.GetAllProducts();

            int customerId = 0;
            int storeId = 0;

            //search by the customer email
            if(Regex.IsMatch(searchedString, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                customerId = _repo.GetAllCustomers().Find(p => p.Email.Equals(searchedString)).ID;
                if(customerId != 0)
                {
                    filterOrders = listAllOrder.FindAll(p => p.CustomerID == customerId);
                }
                else
                {
                     throw new Exception ("Could not find any orders matching this information!");
                }
                
            }
            //search by address (for store)
            else if(Regex.IsMatch(searchedString, @"^[#.0-9a-zA-Z\s,-]+$"))
            {
                storeId = _repo.GetStoreFronts().Find(p => p.Address.Equals(searchedString)).ID;

                if(storeId != 0)
                {
                    filterOrders = listAllOrder.FindAll(p => p.StoreID == storeId);
                }
                else
                {
                    throw new Exception ("Could not find any orders matching this information!");
                }
                
            }
            else
            {
                throw new Exception ("Could not find any orders matching this information!");
            }
            
            if(searchMode == "cost-low2high")
            {
                List<Orders> sortedOrders = new List<Orders>();
                
                sortedOrders = filterOrders.OrderBy(p => p.orderTotalCost).ToList();

                return sortedOrders;
            }
            else if(searchMode == "cost-high2low")
            {
                List<Orders> sortedOrders = new List<Orders>();
                
                sortedOrders = filterOrders.OrderBy(p => p.orderTotalCost).ToList();
                sortedOrders.Reverse();

                return sortedOrders;
            }
            else if(searchMode == "date")
            {
                List<Orders> sortedOrders = new List<Orders>();

                sortedOrders = filterOrders.OrderBy(p => p.createdOrder).ToList();

                return sortedOrders;
            }
            else
            {
                //just return orders
                return filterOrders;
            }
            
        }

        public Orders StartOrder(Orders p_order)
        {
            return _repo.StartOrder(p_order);
        }

        public bool VerifyManager(string p_managerEmail, string p_managerPassword, int p_storeID)
        {

            List<Manager> listOfManagers = _repo.GetAllManagers();

            var found = listOfManagers.Find(p => p.Email.Equals(p_managerEmail) && p.Password.Equals(p_managerPassword) && p.storeID.Equals(p_storeID));
            if(found != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Orders DeleteOrder(Orders p_order)
        {
            return _repo.DeleteOrder(p_order);
        }

        public List<Manager> GetManagers()
        {
            return _repo.GetAllManagers();
        }

        public List<LineItems> GetLineItems()
        {
            return _repo.GetAllLineItems();
        }

    }

}