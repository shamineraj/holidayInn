using HolidayInn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolidayInn.Controllers
{
    public class BookingController : Controller
    {
        Hotel_InnEntities db = new Hotel_InnEntities();

        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewBooking()
        {
            Booking NBook = new Booking();
            NBook.Check_IN = DateTime.Now;
            NBook.Check_OUT = DateTime.Now.AddDays(2).Date;
            return View(NBook);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult NewBooking(Booking NBook)
        {
            NBook.Status = "RESERVED";
            NBook.Reseve_Date = DateTime.Now;
            NBook.ResevedBY = "Shamine";
            db.Bookings.Add(NBook);
            db.SaveChanges();
            return View();
        }

        // GET: reserved List
        public ActionResult CheckIn()
        {

            var BModel = (from b in this.db.Bookings
                          where (b.Status == "RESERVED")
                          select b).ToList();

            return View(BModel);
        }


        //Return to Check in details
        public ActionResult CheckedIN(int? id)
        {
            var BModel = (from b in this.db.Bookings
                          where (b.Reseve_ID == id)
                          select b).FirstOrDefault();

            return View(BModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CheckedIN(Booking NBook)
        {

            var C = (from b in this.db.Bookings
                     where (b.Reseve_ID == NBook.Reseve_ID)
                     select b).FirstOrDefault();


            C.Name = NBook.Name;
            C.PhoneNo = NBook.PhoneNo;
            C.Email = NBook.Email;
            C.Room_No = NBook.Room_No;
            C.Check_IN = NBook.Check_IN;
            C.Check_OUT = NBook.Check_OUT;
            C.Note = NBook.Note;
            C.Status = "CHECKEDIN";

            db.SaveChanges();

            //return View(CheckIn);
            return RedirectToAction("CheckIn", "Booking");

        }

        // GET: Check In  List
        public ActionResult CheckINList()
        {

            var BModel = (from b in this.db.Bookings
                          where (b.Status == "CHECKEDIN")
                          select b).ToList();

            return View(BModel);
        }

        //Return to Check in details
        public ActionResult CheckedOut(int? id)
        {
            var BModel = (from b in this.db.Bookings
                          where (b.Reseve_ID == id)
                          select b).FirstOrDefault();

            return View(BModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CheckedOut(Booking NBook)
        {

            var C = (from b in this.db.Bookings
                          where (b.Reseve_ID == NBook.Reseve_ID)
                          select b).FirstOrDefault();

            
            C.Name = NBook.Name;
            C.PhoneNo = NBook.PhoneNo;
            C.Email = NBook.Email;
            C.Room_No = NBook.Room_No;
            C.Check_IN = NBook.Check_IN;
            C.Check_OUT = NBook.Check_OUT;
            C.Note = NBook.Note;
            C.Status = "CHECKEDOUT";
          
            db.SaveChanges();
          
            return RedirectToAction("CheckIn", "Booking");

        }

    }
}