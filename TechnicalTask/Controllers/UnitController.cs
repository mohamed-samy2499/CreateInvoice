using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using TechnicalTask.Models;

namespace TechnicalTask.Controllers
{
    public class UnitController : Controller
    {
        private readonly HttpClient httpClient;

        //inject the HttpClient service to consume our Apis
        public UnitController(IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(baseUrl);
        }
        // GET: ItemController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync("Api/unit");
            if (response.IsSuccessStatusCode)
            {
                var Units = await response.Content.ReadAsAsync<List<Unit>>();
                return View(Units);
            }
            return View();
        }
        

        // GET: ItemController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"Api/Unit/{id}");
            if (response.IsSuccessStatusCode)
            {
                var Units = await response.Content.ReadAsAsync<List<Unit>>();
                return View(Units);
            }
            return View();
        }

        // GET: ItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Unit unit)
        {

                HttpResponseMessage response = await httpClient.PostAsJsonAsync("Api/Unit", unit);

                if (response.IsSuccessStatusCode)
                {
                // Item created successfully
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the error case
                    return View(unit);
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
        public ActionResult Edit(int id, Unit unit)
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
        public ActionResult Delete(int id, Unit unit)
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
