using System.Data.SqlClient;
using PPModel;

namespace PPDL
{
    public class SQLRepository : IRepository
    {

        private readonly string _connectionStrings;
        public SQLRepository(string p_connectionStrings)
        {
            _connectionStrings = p_connectionStrings;
        }

        public Customer AddCustomer(Customer p_customer)
        {

            string sqlQuery = @"insert into Customer
                                values(@customerName, @customerAddress, @customerEmail)";
        
            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                
                //open the connection, do not need to close connection due to using
                con.Open();
 
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@customerName", p_customer.Name);
                command.Parameters.AddWithValue("@customerAddress", p_customer.Address);
                command.Parameters.AddWithValue("@customerEmail", p_customer.Email);

                //execute the SQL statement
                command.ExecuteNonQuery();

            }

            return p_customer;
            
        } 

        public List<Customer> GetAllCustomers()
        {

            List<Customer> listOfCustomers = new List<Customer>();

            string sqlQuery = @"select * from Customer";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                //open the connection
                con.Open();

                //command object that has our query and con obj
                SqlCommand command = new SqlCommand(sqlQuery, con);

                //read outputs from sql statement using special class
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    listOfCustomers.Add(new Customer()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Email = reader.GetString(3)
                    });

                }

            }

            return listOfCustomers;

        }

        public List<StoreFront> GetStoreFronts()
        {
            
            List<StoreFront> listOfStores = new List<StoreFront>();

            string sqlQuery = @"select * from StoreFront";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                //open the connection
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    listOfStores.Add(new StoreFront()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2)
                    });

                }

            }

            return listOfStores;

        }

        public Orders StartOrder(Orders p_order)
        {
            
            //insert the values into orders table
            string sqlQuery = @"insert into Orders
                                values(@customerID, @storeFrontID, @totalSpent)";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@customerID", p_order.CustomerID);
                command.Parameters.AddWithValue("@storeFrontID", p_order.StoreID);
                command.Parameters.AddWithValue("@totalSpent", p_order.orderTotalCost);

                //execute the SQL statement
                command.ExecuteNonQuery();

            }

            //now grab the latest order in the table
            sqlQuery = @"select max(o.orderID) from Orders o";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    p_order.OrderID = reader.GetInt32(0);
                }

            }

            return p_order;

        }

        public Boolean TestQuantity(int itemID, int itemQuantity)
        {

            //get the quantity from the database and see if that you have that many items in stock to buy
            string sqlQuery = @"select sfp.quantity from storeFront_product sfp
                            where sfp.productID = @productID";

            //make a temp quantity to store that value for later
            int tempQuantity = 0;
            
            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@productID", itemID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tempQuantity = reader.GetInt32(0);
                }

                tempQuantity = tempQuantity - itemQuantity;
                if(tempQuantity < 0)
                {
                    //will return false if the action cannot be done
                    return false;
                }
                else
                {
                    //will return true if action can be done
                    return true;
                }
                
            }
            
            
        }

        public LineItems MakeOrder(LineItems p_lineItems, int p_orderID)
        {         
            //get the quantity from the database and see if that you have that many items in stock to buy
            string sqlQuery = @"select sfp.quantity from storeFront_product sfp
                            where sfp.productID = @productID";

            //make a temp quantity to store that value for later
            int tempQuantity = 0;
            
            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@productID", p_lineItems.ProductID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tempQuantity = reader.GetInt32(0);
                }

                tempQuantity = tempQuantity - p_lineItems.ProductQuantity;
                if(tempQuantity >= 0)
                {
                    //This will make the value of the quantity the same number with a negative in front of it.
                    //That way we will take away the number from the total, allows us to reuse the code used to replenish, only instead we are taking away the value since it's now negative.
                    int subtractFromTotalInventory = 0 - p_lineItems.ProductQuantity;
                    UpdateInventory(p_lineItems.ProductID, subtractFromTotalInventory);
                }
                else
                {
                    Exception exc = new Exception("Cannot purchase more items than the store has available!");
                     return p_lineItems;
                }
               
            }

            //now that we know the items can be purchased, we will now add them to the line items table and then get the total price.
            sqlQuery = @"insert into LineItems
                        values(@orderID, @productID, @quantity)";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@orderID", p_orderID);
                command.Parameters.AddWithValue("@productID", p_lineItems.ProductID);
                command.Parameters.AddWithValue("@quantity", p_lineItems.ProductQuantity);

                //execute the SQL statement
                command.ExecuteNonQuery();

            }

            return p_lineItems;

        }

        public Orders GetOrders(Orders p_order)
        {
            return p_order;
        }

        public void UpdateInventory(int p_productID, int p_quantity)
        {
            int tempQuantity = 0;
            string sqlQuery = @"select sfp.quantity from storeFront_product sfp
                            where sfp.productID = @productID";
            
            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@productID", p_productID);
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tempQuantity = reader.GetInt32(0);
                }

                tempQuantity = tempQuantity + p_quantity;
            
            }
            
            sqlQuery = @"update storeFront_product
                        set quantity = @quantity
                        where productID = @productID";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command= new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@quantity", tempQuantity);
                command.Parameters.AddWithValue("@productID", p_productID);

                command.ExecuteNonQuery();
            }

        }

        public Customer SearchCustomer(Customer p_customer)
        {
            return p_customer;
        }

        public Products ViewOrder(Products p_product)
        {
            return p_product;
        }

        List<Products> IRepository.GetProductsByStoreAddress(string p_address)
        {
            List<Products> listOfProducts = new List<Products>();

            string sqlQuery = @"select p.productID, p.productName, p.productPrice, p.productDescription, p.productCategory, sp.quantity from Product p
                            inner join storeFront_product sp on sp.productID = p.productID 
                            inner join StoreFront s on s.storeFrontID = sp.storeId
                            where s.storeFrontAddress = @storeFrontAddress";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@storeFrontAddress", p_address);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                    listOfProducts.Add(new Products()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Description = reader.GetString(3),
                        Category = reader.GetString(4),
                        quantity = reader.GetInt32(5)
                    });

                }

            }

            return listOfProducts;
        }

        public List<Orders> GetAllOrders()
        {
            List<Orders> listOfOrders = new List<Orders>();

            string sqlQuery = @"select o.orderID, o.customerID, c.customerEmail, o.storeFrontID, s.storeFrontName, o.totalSpent from Orders o
                                inner join Customer c on o.customerID = c.customerID
                                inner join StoreFront s on s.storeFrontID = o.storeFrontID";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                //open the connection
                con.Open();

                //command object that has our query and con obj
                SqlCommand command = new SqlCommand(sqlQuery, con);

                //read outputs from sql statement using special class
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    listOfOrders.Add(new Orders()
                    {

                        OrderID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        customerEmail = reader.GetString(2),
                        StoreID = reader.GetInt32(3),
                        storeFrontName = reader.GetString(4),
                        orderTotalCost = reader.GetDecimal(5)
                        
                    });

                }

            }

            return listOfOrders;
        }

    }
}