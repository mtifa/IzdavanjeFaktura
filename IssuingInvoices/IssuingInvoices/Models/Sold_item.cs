namespace IzdavanjeFaktura.Models
{
    public class Sold_item
    {
        public int SoldItemId { get; set; }
        public int ProductId { get; set; }
        public int InoviceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}