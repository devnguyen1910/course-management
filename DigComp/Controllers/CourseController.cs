using Microsoft.AspNetCore.Mvc;

namespace DigComp.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //============================SH_MN_005i - TRANG WISHLIST KHÓA HỌC===================================================================================================
        public IActionResult WLCourse()
        {
            return View();
        }
    }
}
