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
            return View();
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Phương thức xử lý đăng nhập
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            // Tìm người dùng dựa trên Username và Password
            var user = _context.DceUsers
                .FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                // Nếu người dùng tồn tại, đăng nhập thành công
                // (Bạn có thể sử dụng Session, Cookies, hoặc Redirect)
                return RedirectToAction("Index"); // Chuyển hướng đến trang Dashboard sau khi đăng nhập
            }
            else
            {
                // Nếu không tìm thấy người dùng, đăng nhập thất bại
                ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu");
                return View("Index");
            }
        }

        // Phương thức xử lý đăng ký
        [HttpPost]
        public IActionResult Register(string Username, string Password, string Email)
        {
            // Kiểm tra xem tên đăng nhập đã tồn tại chưa
            var existingUser = _context.DceUsers.FirstOrDefault(u => u.Username == Username);

            if (existingUser != null)
            {
                // Nếu tên đăng nhập đã tồn tại, thông báo lỗi
                ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                return View("Index");
            }

            // Tạo người dùng mới
            var newUser = new DceUser
            {
                Username = Username,
                Password = Password, // Mã hóa mật khẩu trước khi lưu vào DB trong thực tế
                Email = Email,
                Fullname = "Chưa có tên", // Bạn có thể cập nhật sau
                Gender = true, // Giới tính mặc định
                Birthday = DateOnly.FromDateTime(DateTime.Now) // Ngày sinh mặc định
            };

            // Thêm người dùng mới vào cơ sở dữ liệu
            _context.DceUsers.Add(newUser);
            _context.SaveChanges();

            // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}
