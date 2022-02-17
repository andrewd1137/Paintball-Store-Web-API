using PPModel;

namespace PPBL
{

    /// <summary>
    /// Business layer is responsible for further validation or processing of data obtained from either he database or the user.
    /// 
    /// </summary>
    public interface IPlanetPaintballBL
    {

        /// <summary>
        /// will add a customer data to the database
        /// </summary>
        /// <param name="p_customer">this is the customer information</param>
        /// <returns>customer information</returns>
        Customer AddCustomer(Customer p_customer);

        /// <summary>
        /// will search for a customer by email in the list of customer objects 
        /// </summary>
        /// <param name="p_customer">this is customer information</param>
        /// <returns>customer information</returns>
        List<Customer> SearchCustomer(string searchMode, string p_string);

        /// <summary>
        /// Will search for customer(s) by name
        /// </summary>
        /// <param name="p_customerName"></param>
        /// <returns></returns>
        List<Customer> SearchCustomerByName(string p_customerName);

        /// <summary>
        /// will search for a customer by email (should only return one customer because email should be unique)
        /// </summary>
        /// <param name="p_customerEmail"></param>
        /// <returns></returns>
        List<Customer> SearchCustomerByEmail(string p_customerEmail);

        /// <summary>
        /// will get all the customers
        /// </summary>
        /// <returns></returns>
        List<Customer> GetCustomers();

    }


}

