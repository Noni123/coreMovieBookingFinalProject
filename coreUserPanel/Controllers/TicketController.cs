using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreUserPanel.Helpers;
using coreUserPanel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreUserPanel.Controllers
{

    public class TicketController : Controller
    {

        public string audiName;
        ProjectTestDataContext context = new ProjectTestDataContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = context.UserDetails.Where(x => x.UserName == username && x.Password == password).SingleOrDefault();
            if (user == null)
            {
                ViewBag.Error = "Invalid Credentials";
                return RedirectToAction("Login", "Ticket");
            }
            else
            {
                var userName = user.UserName;
                var Passwords = user.Password;
                string userdetailId = Convert.ToString(user.UserDetailId);
                var email = user.Email;
                var contact = user.ContactNo;
                if (username != null && password != null && username.Equals(userName) && password.Equals(Passwords))
                {
                    HttpContext.Session.SetString("uname", username);
                    HttpContext.Session.SetString("uid", userdetailId);
                    HttpContext.Session.SetString("uemail", email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Invalid Credentials";
                    return RedirectToAction("Login", "Ticket");
                }
            }
        }
        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("uname");
            return RedirectToAction("Index", "Home");
        }
        [Route("ChangePassword")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Route("ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(string oldpassword, string newpassword, string newpassword1)
        {
            int id = int.Parse(HttpContext.Session.GetString("uid"));
            UserDetails c = context.UserDetails.Where(x => x.UserDetailId == id).SingleOrDefault();
            if (oldpassword == c.Password && newpassword == newpassword1)
            {
                UserDetails cus = context.UserDetails.Where(x => x.UserName == c.UserName).SingleOrDefault();
                cus.Password = newpassword;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "bookmovie", cus);
                context.SaveChanges();
            }
            else
            {
                ViewBag.Error = "  Invalid Credentials";
                return View("Password");
            }
            return RedirectToAction("Login", "Ticket");
        }

        [Route("register")]

        [HttpGet]
        public ViewResult Register()
        {

            var bookmovie = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            ViewBag.bookmovie = bookmovie;
            ViewBag.total = bookmovie.Sum(item => item.Movies.MoviePrice * item.Quantity);
            TempData["total"] = ViewBag.total;

            return View();
        }
        [Route("register")]
        [HttpPost]
        public IActionResult Register(UserDetails c1)
        {
            var showTiming = Request.Form["showTiming"].ToString();
            var bookmovie = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            var total = bookmovie.Sum(item => item.Movies.MoviePrice * item.Quantity);
            context.UserDetails.Add(c1);
            context.SaveChanges();

            checkAudi(showTiming);

            Bookings booking = new Bookings()
            {
                BookingAmount = total,
                BookingDate = DateTime.Now,
                ShowTiming = showTiming,
                AudiName = audiName,
                UserDetailId = c1.UserDetailId
            };

            context.Bookings.Add(booking);
            context.SaveChanges();

            List<BookingDetails> BookingDetail = new List<BookingDetails>();
            for (int i = 0; i < bookmovie.Count; i++)
            {
                BookingDetails bookingDetail = new BookingDetails()
                {
                    BookingId = booking.BookingId,
                    MovieId = bookmovie[i].Movies.MovieId,
                    QtySeats = bookmovie[i].Quantity
                };
                context.BookingDetails.Add(bookingDetail);
            }
            BookingDetail.ForEach(n => context.BookingDetails.Add(n));
            context.SaveChanges();

            TempData["uid"] = c1.UserDetailId;

            return RedirectToAction("Checkout", "Ticket");
        }

        public void checkAudi(string showTiming)
        {
            if (showTiming == "12 Noon")
            {
                audiName = "Audi 1";
            }
            else if (showTiming == "3 PM")
            {
                audiName = "Audi 2";
            }
            else if (showTiming == "6 PM")
            {
                audiName = "Audi 3";
            }
        }

        public IActionResult Checkout()
        {
            var id = int.Parse(TempData["uid"].ToString());
            var userDetails = context.UserDetails.Where(x => x.UserDetailId == id).SingleOrDefault();
            //UserDetails userDetails = context.UserDetails.Where(x => x.UserDetailId == id).SingleOrDefault();
            //ViewBag.UserDetails = userDetails;

            var bookmovie = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            ViewBag.bookmovie = bookmovie;
            ViewBag.total = bookmovie.Sum(item => item.Movies.MoviePrice * item.Quantity);
            //ViewBag.totalitem = bookmovie.Count();
            TempData["total"] = ViewBag.total;
            TempData["uid"] = id;
            return View(userDetails);
        }
        [HttpPost]
        public IActionResult Checkout(UserDetails userDetails)
        {
            //context.UserDetails.Add(userDetails);
            //context.SaveChanges();
            var amount = (TempData["total"]);
            var uid = (TempData["uid"]).ToString();
            Bookings bookings = new Bookings()
            {
                BookingAmount = Convert.ToSingle(amount),
                BookingDate = DateTime.Now,
                UserDetailId = int.Parse(uid)
                //UserDetailId = userDetails.UserDetailId
            };
            ViewBag.book = bookings;
            context.Bookings.Add(bookings);
            context.SaveChanges();


            var bookmovie = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            List<BookingDetails> BookingDetail = new List<BookingDetails>();
            for (int i = 0; i < bookmovie.Count; i++)
            {
                BookingDetails booking = new BookingDetails()
                {
                    BookingId = bookings.BookingId,
                    MovieId = bookmovie[i].Movies.MovieId,
                    QtySeats = bookmovie[i].Quantity
                };
                context.BookingDetails.Add(booking);
            }
            BookingDetail.ForEach(n => context.BookingDetails.Add(n));
            context.SaveChanges();
            TempData["cust"] = /*userDetails.UserDetailId*/uid;
            ViewBag.bookings = null;

            return RedirectToAction("Invoice", "Ticket");
        }
        public IActionResult Invoice()
        {
            int userId = int.Parse(TempData["uid"].ToString());
            UserDetails userdetails = context.UserDetails.Where(x => x.UserDetailId == userId).SingleOrDefault();
            ViewBag.UserDetail = ViewBag;

            var bookmovie = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            ViewBag.bookmovie = bookmovie;

            ViewBag.total = bookmovie.Sum(item => item.Movies.MoviePrice * item.Quantity);
            return View();

        }
        public IActionResult ViewProfile(UserDetails userdetails)
        {
            int id = int.Parse(HttpContext.Session.GetString("uid"));
            UserDetails c = context.UserDetails.Where(x => x.UserDetailId == id).SingleOrDefault();
            return View(c);

        }
        public ViewResult Dashboard()
        {
            return View();
        }
      [HttpGet]
        public IActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditProfile(UserDetails m1)
        {
            int id = int.Parse(HttpContext.Session.GetString("uid"));
            UserDetails user = context.UserDetails.Where(x => x.UserDetailId == id).SingleOrDefault();
            user.UserName = m1.UserName;
            user.Email = m1.Email;
            user.ContactNo = m1.ContactNo;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult BookingHistory()
        {
            int id = int.Parse(HttpContext.Session.GetString("uid"));
            Bookings c = context.Bookings.Where(x => x.UserDetailId == id).SingleOrDefault();
            return View(c);


        }
        public IActionResult BookingDetails()
        {
            int id = int.Parse(HttpContext.Session.GetString("uid"));
            BookingDetails c = context.BookingDetails.Where(x => x.BookingId == id).SingleOrDefault();
            return View(c);


        }

    }
}




