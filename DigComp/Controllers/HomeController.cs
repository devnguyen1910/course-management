using System.Diagnostics;
using DigComp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigComp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DCE_DbContext _context;

        public HomeController(ILogger<HomeController> logger, DCE_DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username"); // Kiểm tra xem user có đăng nhập không
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string; // Lấy thông báo từ TempData
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Hiển thị trang đăng nhập
        public IActionResult Login()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            return View();
        }

        // Hiển thị trang đăng ký
        public IActionResult Register()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var user = _context.DceUsers
                .FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("Username", user.Username);

                TempData["SuccessMessage"] = "Đăng nhập thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu");
                return View();
            }
        }

        // Xử lý đăng ký
        [HttpPost]
        public IActionResult Register(string Fullname, string Username, string Password, string Email)
        {
            var existingUser = _context.DceUsers.FirstOrDefault(u => u.Username == Username);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                return View();
            }

            var newUser = new DceUser
            {
                Fullname = Fullname,
                Username = Username,
                Password = Password, // Nên mã hóa mật khẩu
                Email = Email,
                Birthday = null
            };

            _context.DceUsers.Add(newUser);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction("Login");
        }

        // Xử lý logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username"); // Xóa session đăng nhập
            TempData["SuccessMessage"] = "Bạn đã đăng xuất thành công!";
            return RedirectToAction("Index");
        }

        public IActionResult Account()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login"); // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
            }

            var username = HttpContext.Session.GetString("Username");
            var user = _context.DceUsers.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Username = user.Username;
            ViewBag.Fullname = user.Fullname;
            ViewBag.Email = user.Email;

            return View();
        }

        public IActionResult AccountInfo()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login");
            }

            var username = HttpContext.Session.GetString("Username");
            var user = _context.DceUsers.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Username = user.Username;
            ViewBag.Fullname = user.Fullname;
            ViewBag.Email = user.Email;
            ViewBag.Birthday = user.Birthday;

            return View();
        }

        [HttpPost]
        public IActionResult AccountInfo(string Fullname, DateOnly? Birthday)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login");
            }

            var username = HttpContext.Session.GetString("Username");
            var user = _context.DceUsers.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Cập nhật thông tin tài khoản
            user.Fullname = Fullname;
            user.Birthday = Birthday;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("AccountInfo");
        }


        public IActionResult ChangePassword()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login"); // Nếu người dùng chưa đăng nhập, chuyển đến trang đăng nhập
            }

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string OldPassword, string NewPassword)
        {
            var username = HttpContext.Session.GetString("Username");
            var user = _context.DceUsers.FirstOrDefault(u => u.Username == username);

            if (user == null || user.Password != OldPassword)
            {
                TempData["SuccessMessage"] = "Mật khẩu hiện tại không đúng!";
                return RedirectToAction("ChangePassword");
            }

            user.Password = NewPassword;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("ChangePassword");
        }

        public IActionResult RegisterGuide()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult AllCourses()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
          return View();
        }
        public IActionResult Partner()
        {
            return View();
        }
    }
}
