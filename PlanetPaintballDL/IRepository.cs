using PPModel;

namespace PPDL
{

    /// <summary>
    /// Data layer project is responsible for interfacting with database and doing crud operations
    /// C -- create, R -- read, U -- update, D -- delete
    /// Think of it as a delivery man. You dont want them to touch or use your items, just to deliver it.
    /// </summary>
    public interface IRepository
    {

        /// <summary>
        /// Add customer to the database
        /// </summary>
        /// <param name="p_customer">The customer object we are adding to the database</param>
        /// <returns>Returns the customer that was added</returns>
        Customer AddCustomer(Customer p_customer);

        /// <summary>
        /// Searches for the customer in the database takes in the customer we are searching for
        /// </summary>
        /// <param name="p_customer"></param>
        /// <returns></returns>
        Customer SearchCustomer(Customer p_customer);
        

        /// <summary>
        /// will give all the customers in the database
        /// </summary>
        /// <returns>returns a list collection of customer objects</returns>
        List<Customer> GetAllCustomers();

        /// <summary>
        /// will give all the stores in the database
        /// </summary>
        /// <returns>collection of store objects</returns>
        List<StoreFront> GetStoreFronts();

        /// <summary>
        /// will get all the products that the store has
        /// </summary>
        /// <param name="p_address"></param>
        /// <returns>collection of products</returns>
        List<Products> GetProductsByStoreAddress(string p_address);

        /// <summary>
        /// will get all of the orders available
        /// </summary>
        /// <returns></returns>
        List<Orders> GetAllOrders(); 

        /// <summary>
        /// will view the current items in the order
        /// </summary>
        /// <param name="p_order"></param>
        /// <returns></returns>
        Products ViewOrder(Products p_product);

        /// <summary>
        /// will start the order by adding information to the orders table in database
        /// </summary>
        /// <param name="p_order"></param>
        /// <returns></returns>
        Orders StartOrder(Orders p_order);

        /// <summary>
        /// will make the add the products the customer ordered
        /// </summary>
        /// <param name="p_order"></param>
        /// <returns></returns>
        LineItems MakeOrder(LineItems p_lineItems, int quantityOrdered);

        /// <summary>
        /// will replenish the inventory for a product.
        /// </summary>
        /// <param name="p_productID"></param>
        /// <param name="p_quantity"></param>
        void UpdateInventory(int p_productID, int p_quantity);

        /// <summary>
        /// used to test to see if user can buy this amount of items
        /// </summary>
        /// <param name="p_lineItems"></param>
        /// <returns></returns>
        Boolean TestQuantity(int itemID, int itemQuantity);
    }


}

