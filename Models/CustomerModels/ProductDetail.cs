namespace ShopHoaMVC.Models.CustomerModels
{
    public class ProductDetail
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public double? Price { get; set; }
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

    }
}
