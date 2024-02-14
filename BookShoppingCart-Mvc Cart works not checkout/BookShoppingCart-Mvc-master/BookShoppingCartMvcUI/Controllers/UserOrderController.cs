//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace BookShoppingCartMvcUI.Controllers
//{
//    [Authorize]
//    public class UserOrderController : Controller
//    {
//        private readonly IUserOrderRepository _userOrderRepo;

//        public UserOrderController(IUserOrderRepository userOrderRepo)
//        {
//            _userOrderRepo = userOrderRepo;
//        }
//        public async Task<IActionResult> UserOrders()
//        {
//            var orders = await _userOrderRepo.UserOrders();
//            return View(orders);
//        }
//    }
//}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookShoppingCartMvcUI.Models; 
using BookShoppingCartMvcUI.Data; 

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository _userOrderRepo;
        private readonly ApplicationDbContext _context;

        public UserOrderController(IUserOrderRepository userOrderRepo, ApplicationDbContext context)
        {
            _userOrderRepo = userOrderRepo;
            _context = context;
        }

        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userOrderRepo.UserOrders();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserOrders");
            }
            return View(order);
        }
    }
}
