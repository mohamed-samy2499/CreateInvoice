using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using TechnicalTask.Models;

namespace TechnicalTask.Controllers
{
    public class ItemController : Controller
    {
        private readonly HttpClient httpClient;

        //inject the HttpClient service to consume our Apis
        public ItemController(IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(baseUrl);
        }
        // GET: ItemController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync("Api/Item");
            if (response.IsSuccessStatusCode)
            {
                var Items = await response.Content.ReadAsAsync<List<Item>>();
                return View(Items);
            }
            return View();
        }
        

        // GET: ItemController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"Api/Item/{id}");
            if (response.IsSuccessStatusCode)
            {
                var Item = await response.Content.ReadAsAsync<Item>();
                return View(Item);
            }
            return View();
        }
        // GET: ItemController/Details/5
        public async Task<Item> GetOne(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"Api/Item/{id}");
            var Item = new Item();
            if (response.IsSuccessStatusCode)
            {
                Item = await response.Content.ReadAsAsync<Item>();
                return Item;
            }
            return Item;
        }
        // GET: ItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item Item)
        {

                HttpResponseMessage response = await httpClient.PostAsJsonAsync("Api/Item", Item);

                if (response.IsSuccessStatusCode)
                {
                // Item created successfully
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the error case
                    return View("Error");
                }
            }

        // GET: ItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Item item)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Item item)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
