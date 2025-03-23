namespace DigComp.Models
{
    public class Banner
    {
        public int Id { get; set; } // Thêm Id nếu bạn cần
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string LinkUrl { get; set; }
    }
}
