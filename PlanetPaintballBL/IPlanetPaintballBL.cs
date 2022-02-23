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
        /// will search for a customer by email or name in the list of customer objects 
        /// </summary>
        /// <param name="p_customer">this is customer information</param>
        /// <returns>customer information</returns>
        List<Customer> SearchCustomer(string p_customerInfo);

        /// <summary>
        /// Will search if the customer's credentials match
        /// </summary>
        /// <param name="p_customerName"></param>
        /// <returns></returns>
        List<Customer> VerifyCustomer(string p_customerEmail, string p_customerPassword);

        /// <summary>
        /// will get all customers
        /// </summary>
        /// <returns></returns>
        List<Customer> GetCustomers();

    }


}

