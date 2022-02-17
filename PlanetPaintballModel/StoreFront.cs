namespace PPModel
{

    public class StoreFront
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        private List<Products> _products;
        public List<Products> Products
        {
            get { return _products; }
            set
            {
                _products = value;
            }
        }

        private List<Orders> _orders;
        public List<Orders> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
            }
        }

        public StoreFront()
        {

            Name = "Planet Paintball";
            Address = "21 Paint St";
            _products = new List<Products>()
            {
                new Products()
            };

            _orders = new List<Orders>()
            {
                new Orders()
            };

        }

        //string version of the object
        public override string ToString()
        {   

            Console.WriteLine(Name + Address);
            string items = "=================\n";
            foreach(Products item in _products)
            {  
                items +=  item + "\n=================\n";
            }

            return items;
                
        }

        public string printStoreInfo()
        {
            return "====================\nStore Name: " + Name + "\nStore Address: " + Address;
        }

        

    }

}