using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using TechnicalTask.Models;

namespace TechnicalTask.Controllers
{
    public class StoreController : Controller
    {
        private readonly HttpClient httpClient;

        //inject the HttpClient service to consume our Apis
        public StoreController(IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(baseUrl);
        }
        // GET: StoreController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync("Api/Store");
            if (response.IsSuccessStatusCode)
            {
                var stores = await response.Content.ReadAsAsync<List<StoreViewModel>>();
                return View(stores);
            }
            return View();
        }
        

        // GET: StoreController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"Api/Store/{id}");
            if (response.IsSuccessStatusCode)
            {
                var stores = await response.Content.ReadAsAsync<List<Store>>();
                return View(stores);
            }
            return View();
        }

        // GET: StoreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Store store)
        {

                HttpResponseMessage response = await httpClient.PostAsJsonAsync("Api/Store", store);

                if (response.IsSuccessStatusCode)
                {
                // Store created successfully
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the error case
                    return View("Error");
                }
            }

        // GET: StoreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: StoreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
