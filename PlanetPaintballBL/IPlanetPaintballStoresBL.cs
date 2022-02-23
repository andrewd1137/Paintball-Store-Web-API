using PPDL;
using PPModel;

namespace PPBL
{

    public interface IPlanetPaintballStoresBL
    {

        /// <summary>
        /// will display the inventory of the store along with the store information.
        /// </summary>
        /// <param name="p_address"></param>
        /// <returns></returns>
        List<StoreFront> ViewInventory(string p_address);

        /// <summary>
        /// will display the products that a specific store
        /// </summary>
        /// <param name="p_address"></param>
        /// <returns></returns>
        List<Products> GetProductsByStoreAddress(string p_address);

        /// <summary>
        /// will replenish the inventory of a store with the amount given
        /// </summary>
        /// <param name="p_productID"></param>
        /// <param name="p_quantity"></param>
        void UpdateInventory(int p_storeID, int p_productID, int p_quantity);

        /// <summary>
        /// will view the product from an order
        /// </summary>
        /// <param name="p_order"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        List<Products> ViewOrder(int p_productID, string storeAddress);

        /// <summary>
        /// will add the customer order to the orders tab first.
        /// </summary>
        /// <param name="p_order"></param>
        /// <returns></returns>
        public Orders StartOrder(Orders p_order);

        /// <summary>
        /// will make the order of the items the customer has ordered
        /// </summary>
        /// <param name="newOrder"></param>
        LineItems MakeOrder(int p_storeID, LineItems p_lineItems, int orderID);

        Orders MakeAnOrder(Orders p_order);

        /// <summary>
        /// will get the orders of either a store or customer based on their search mode
        /// </summary>
        /// <param name="searchMode"></param>
        /// <param name="storeLocation"></param>
        /// <returns></returns>
        List<Orders> GetOrders(string seachedString);

        /// <summary>
        /// will list all the stores for the user if they do not know the address of the one they want to view
        /// </summary>
        /// <param name="p_address"></param>
        /// <returns></returns>
        List<StoreFront> ViewAllStores();

        /// <summary>
        /// will verify if the manager's credentials match
        /// </summary>
        /// <param name="p_managerEmail"></param>
        /// <param name="p_managerPassword"></param>
        /// <returns></returns>
        Boolean VerifyManager(string p_managerEmail, string p_managerPassword, int p_storeID);

        /// <summary>
        /// will test to see if the quantity of an store product can be purchased
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="itemQuantity"></param>
        /// <returns></returns>
        Boolean TestQuantity(int itemID, int itemQuantity);
    }

}