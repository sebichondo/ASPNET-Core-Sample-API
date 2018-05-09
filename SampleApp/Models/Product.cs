namespace SampleApp.Models
{
    public class Product : BaseEntity
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        //other properties here like address and such
    }
}
