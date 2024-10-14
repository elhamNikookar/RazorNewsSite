using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using ZarinpalSandbox;

namespace CoffeeShop.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor
        private readonly ILogger<HomeController> _logger;
        private readonly DbContext _context;


        public HomeController(ILogger<HomeController> logger, DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        #endregion
        public IActionResult Index()
        {
            //var products = _context.Products
            //    .ToList();

            return View();
        }


        public IActionResult Detail(int id)
        {
            //var product = _context.Products
            //    .Include(p => p.Item)
            //    .SingleOrDefault(p => p.ID == id);

            //if (product == null) return NotFound();

            //var categories = _context.Products
            //    .Where(p => p.ID == id)
            //    .SelectMany(c => c.CategoryToProducts)
            //    .Select(ca => ca.category)
            //    .ToList();

            //var model = new DetailsViewModel()
            //{
            //    Product = product,
            //    Categories = categories
            //};

            return View();
        }


        #region Cart

        [Authorize]
        public IActionResult AddToCart(int itemId)
        {
            //var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.ItemId == itemId);

            //if (product != null)
            //{
            //    int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            //    var order = _context.Orders.FirstOrDefault(o => o.UserId == userID);
            //    if (order != null)
            //    {
            //        var orderDetail = _context.OrderDetails.FirstOrDefault(
            //            d => d.OrderId == order.ID && d.ProductId == product.ID);

            //        if (orderDetail != null)
            //        {
            //            orderDetail.Count += 1;
            //        }
            //        else
            //        {
            //            _context.OrderDetails.Add(new OrderDetail()
            //            {
            //                OrderId = order.ID,
            //                ProductId = product.ID,
            //                Price = product.Item.Price,
            //                Count = 1
            //            });
            //        }
            //    }
            //    else
            //    {
            //        order = new Order()
            //        {
            //            IsFinaly = false,
            //            CreateDate = DateTime.Now,
            //            UserId = userID
            //        };
            //        _context.Orders.Add(order);
            //        _context.SaveChanges();


            //        _context.OrderDetails.Add(new OrderDetail()
            //        {
            //            OrderId = order.ID,
            //            ProductId = product.ID,
            //            Price = product.Item.Price,
            //            Count = 1
            //        });
            //    }
            //    _context.SaveChanges();
            //}

            return RedirectToAction("ShowCart");
        }


        public IActionResult RemoveCart(int detailId)
        {
            //var orderDetail = _context.OrderDetails.Find(detailId);
            //_context.OrderDetails.Remove(orderDetail);
            //_context.SaveChanges();
            return RedirectToAction("ShowCart");
        }


        [Authorize]
        public IActionResult ShowCart()
        {
            //int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());

            //var order = _context.Orders.Where(o => o.UserId == userID && !o.IsFinaly)
            //    .Include(o => o.OrderDetails)
            //    .ThenInclude(c => c.Product).FirstOrDefault();

            return View();
        }

        #endregion

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}



        [Authorize]
        public IActionResult Payment()
        {
            //int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var order = _context.Orders
            //    .Include(o => o.OrderDetails)
            //    .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
            //if (order == null)
            //    return NotFound();

            //var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
            //var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.ID}",
            //    "http://localhost:5083/Home/OnlinePayment/" + order.ID, "Iman@Madaeny.com", "09197070750");
            //if (res.Result.Status == 100)
            //{
            //    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            //}
            //else
            //{
            //    return BadRequest();
            //}
            return View();

        }

        public IActionResult OnlinePayment(int id)
        {
            //if (HttpContext.Request.Query["Status"] != "" &&
            //    HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
            //    HttpContext.Request.Query["Authority"] != "")
            //{
            //    string authority = HttpContext.Request.Query["Authority"].ToString();
            //    var order = _context.Orders.Include(o => o.OrderDetails)
            //        .FirstOrDefault(o => o.ID == id);
            //    var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
            //    var res = payment.Verification(authority).Result;
            //    if (res.Status == 100)
            //    {
            //        order.IsFinaly = true;
            //        _context.Orders.Update(order);
            //        _context.SaveChanges();
            //        ViewBag.code = res.RefId;
            //        return View();
            //    }
            //}

            return NotFound();
        }


    }
}
