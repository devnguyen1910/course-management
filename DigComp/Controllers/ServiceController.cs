using Microsoft.AspNetCore.Mvc;

namespace DigComp.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //============================SH_MN_005v - TRANG WISHLIST SẢN PHẨM DỊCH VỤ===================================================================================================
        public IActionResult WLService()
        {
            return View();
        }
    }
}
