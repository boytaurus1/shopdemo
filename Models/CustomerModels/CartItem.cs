namespace ShopHoaMVC.Models.CustomerModels
{
    public class CartItem
    {
        public int CartId { get; set; }
        public string CartImage { get; set; }
        public string CartTitle { get; set;}
        public double CartPrice { get; set;}
        public int CartQuantity { get; set;}
        public double TotalPrice => CartQuantity * CartPrice;
    }
}
