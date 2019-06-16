using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using laba2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using laba2.Services;

namespace laba2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext _db;
        private IMemoryCache _cache;
        private string _cookieKey = "formCookies";
        private string _sessionKey = "formSession";
        private DbService _service;

        public HomeController(ApplicationContext context, IMemoryCache memoryCache, DbService service)
        {
            _db = context;
            _cache = memoryCache;
            _service = service;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PriceList()
        {
            ViewData["Message"] = "Price-list";
            //List<PriceClass> cacheList = _cache.Get<List<PriceClass>>("PriceFuels");
            List<PriceClass> cacheList = _service.GetPriceFuels();

            //var priceFuels = from type in db.TypeGSMs
            //                    join price in db.Prices
            //                    on type.Id equals price.TypeGSMId
            //                    select new { type = type.Marking, price = price.Cost, date = price.Date };
            //List<PriceFuels> list = new List<PriceFuels>();
            //foreach (var pf in priceFuels) list.Add(new PriceFuels { type = pf.type, price = pf.price, date = pf.date });

            return View(cacheList);
        }

        public async Task<IActionResult> Volume()
        {
            ViewData["Message"] = "Volumes";

            return View(await _db.Subjects.ToListAsync());
        }

        //public List<PriceFuel> GetPriceFuels()
        //{
        //    List<PriceFuel> priceFuels = new List<PriceFuel>();

        //    PriceFuel priceFuels1 = new PriceFuel();
        //    priceFuels1.Fuel = "АИ-89";
        //    priceFuels1.Price = 2.1;
        //    priceFuels1.Date = DateTime.Now;

        //    PriceFuel priceFuels2 = new PriceFuel();
        //    priceFuels2.Fuel = "АИ-91";
        //    priceFuels2.Price = 2.5;
        //    priceFuels2.Date = DateTime.Now;

        //    priceFuels.Add(priceFuels1);
        //    priceFuels.Add(priceFuels2);
        //    return priceFuels;
        //}

        public IActionResult AddFuel()
        {
            ViewData["Message"] = "Add fuel date";
            List<string> TypeFuels = new List<string> { "1", "2", "3", "4" };
            ViewBag.TypeFuels = new SelectList(TypeFuels);
            PriceClass priceFuels = new PriceClass("1","11" , 1 , DateTime.Now);

            if (Request.Cookies.ContainsKey(_cookieKey))
            {
                priceFuels = JsonConvert.DeserializeObject<PriceClass>(Request.Cookies[_cookieKey]);
            }

            //ViewBag.PriceFuels = priceFuels;

            return View(priceFuels);
        }

        public IActionResult Subject()
        {
            return View(_db.Subjects.ToList());
        }

        public IActionResult AddSubject()
        {
            ViewData["Message"] = "Add subject";
            Subject volume = new Subject();

            if (HttpContext.Session.Keys.Contains(_sessionKey))
            {
                volume = JsonConvert.DeserializeObject<Subject>(HttpContext.Session.GetString(_sessionKey));
            }

            //ViewBag.PriceFuels = priceFuels;

            return View(volume);
        }

        [HttpPost]
        public IActionResult AddFuel(PriceClass priceFuels)
        {
            //CookieOptions option = new CookieOptions();
            //option.Expires = DateTime.Now.AddSeconds(2 * 10 + 240);
            //Response.Cookies.Append(_cookieKey, JsonConvert.SerializeObject(priceFuels), option);
            _service.AddPriceFuels(priceFuels);
            //_cache.Remove("PriceFuels");

            return View("PriceList", _service.GetPriceFuels());
        }


        [HttpPost]
        public async Task<IActionResult> AddSubject(Subject subject)
        {
            _db.Subjects.Add(subject);
            _db.SaveChanges();
            //CookieOptions option = new CookieOptions();
            //option.Expires = DateTime.Now.AddSeconds(2 * 10 + 240);
            HttpContext.Session.SetString(_sessionKey, JsonConvert.SerializeObject(subject));
            //_service.AddPriceFuels(priceFuels);
            //_cache.Remove("PriceFuels");

            return View("Subject", await _db.Subjects.ToListAsync());
        }

       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
