using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreUserPanel.Helpers;
using coreUserPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreUserPanel.Controllers
{
    public class BookingController : Controller
    {
        ProjectTestDataContext context = new ProjectTestDataContext();
        public IActionResult Index()
        {
            var bookmovie = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            ViewBag.bookmovie = bookmovie;
            ViewBag.total = bookmovie.Sum(item => item.Movies.MoviePrice * item.Quantity);
            return View();
        }

        public IActionResult Book(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Movies = context.Movies.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "bookmovie", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Movies = context.Movies.Find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "bookmovie", cart);
            }
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Movies.MovieId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "bookmovie", cart);
            return RedirectToAction("Index");
        }
        [Route("goback")]
        public IActionResult GoBack()
        {
            return View();
        }
        [Route("Plus/{id}")]
        public IActionResult Plus(int id)
        {
            List<Item> bookmovie = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            int index = isExist(id);
            if (index != -1)
            {
                bookmovie[index].Quantity++;
            }
            else
            {
                bookmovie.Add(new Item
                {
                    Movies = context.Movies.Find(id),
                    Quantity = 1
                });

            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "bookmovie", bookmovie);
            return RedirectToAction("Index");
        }


        [Route("Minus/{id}")]
        [HttpGet]
        public IActionResult Minus(int id)
        {
            List<Item> bookmovie = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "bookmovie");
            int index = isExist(id);
            if (index != -1)
            {
                if (bookmovie[index].Quantity != 1)
                {
                    bookmovie[index].Quantity--;
                }

                else
                    return RedirectToAction("Remove", "bookmovie", new { @id = id });
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "bookmovie", bookmovie);
            return RedirectToAction("Index");
        }

    }

}