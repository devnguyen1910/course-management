using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DigComp.Models;
using System.Linq;

namespace DigComp.Controllers
{
    public class BannerController : Controller
    {
        private List<Banner> GetBanners()
        {
            // **Trong ứng dụng thực tế, bạn sẽ lấy dữ liệu này từ Database, Configuration, hoặc một nguồn khác.**
            return new List<Banner>()
            {
                new Banner { Id = 1, ImageUrl = "~/assets/images/slider/slider_image_1.jpg", IsActive = true, LinkUrl = "/Course/Index" },
                new Banner { Id = 2, ImageUrl = "~/assets/images/slider/banner Mac.jpg", IsActive = true, LinkUrl = "/ServicePackage/Index" },
                new Banner { Id = 3, ImageUrl = "~/assets/images/slider/banner3.jpg", IsActive = true, LinkUrl = "#" },
                new Banner { Id = 4, ImageUrl = "~/assets/images/slider/inactive_banner.jpg", IsActive = false, LinkUrl = "#" }
            };
        }

        public IActionResult BannerSliderFixedTwo()
        {
            var banners = GetBanners();
            var activeBanners = banners.Where(b => b.IsActive).Take(2).ToList();
            return PartialView("_BannerSliderFixedTwo", activeBanners);
        }
    }
}