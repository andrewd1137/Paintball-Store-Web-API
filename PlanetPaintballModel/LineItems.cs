namespace PPModel
{

    public class LineItems
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantity { get; set; }


        public override string ToString()
        {
            return $"======================\nID: {ProductID}\nQuantity: {ProductQuantity}\n";
        }

    }

    

}