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

    }


}

