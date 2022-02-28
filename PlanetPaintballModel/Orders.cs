namespace PPModel
{

    public class Orders
    {
        public int OrderID {get; set;}

        public int CustomerID {get; set;}
    
        public int StoreID {get; set;}

        public string storeName {get; set;}

        public decimal orderTotalCost {get; set;}

        public DateTime createdOrder {get; set;}

        private List<LineItems> _shoppingCart;
        public List<LineItems> ShoppingCart
        {

            get { return _shoppingCart; }
            set
            {
                _shoppingCart = value;
            }

        }

        //string version of the object
        public override string ToString()
        {
            return $"=====================\nOrder ID: {OrderID}\nCustomer ID: {CustomerID}\nStore ID: {StoreID}\n=====================\n";
        }

    }

}