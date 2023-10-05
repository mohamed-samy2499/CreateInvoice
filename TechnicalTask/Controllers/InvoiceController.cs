using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using TechnicalTask.Models;
using AutoMapper;

namespace TechnicalTask.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;

        //inject the HttpClient service to consume our Apis
        public InvoiceController(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(baseUrl);
            this.mapper = mapper;
        }
        // GET: InvoiceController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync("Api/Invoice");
            if (response.IsSuccessStatusCode)
            {
                var invoices = await response.Content.ReadAsAsync<List<InvoiceViewModel>>();
                return View(invoices);
            }
            return View();
        }
        

        // GET: InvoiceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InvoiceController/Create
        public async Task<ActionResult> Create()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"Api/Store");
            HttpResponseMessage response1 = await httpClient.GetAsync($"Api/Item");

            if (response.IsSuccessStatusCode && response1.IsSuccessStatusCode)
            {
                var stores = await response.Content.ReadAsAsync<List<Store>>();
                var items = await response1.Content.ReadAsAsync<List<Item>>();
                var invoiceVm = new InvoiceViewModel
                {
                    InvoiceItemsViewModel = new List<InvoiceItemViewModel>()
                };
                invoiceVm.Date = DateTime.Now;
                ViewBag.Stores = stores;
                ViewBag.Items = items;
                return View(invoiceVm);
            }
            return View();

        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvoiceViewModel invoiceVm)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Api/Invoice", invoiceVm);
            if (response.IsSuccessStatusCode)
            {
                // invoice created successfully
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error case
                return View(invoiceVm);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddInvoiceItemRow(int index)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"Api/Invoice/AddInvoiceItemRow/{index}");

            if (response.IsSuccessStatusCode)
            {
                var viewModel = await response.Content.ReadAsAsync<InvoiceItemViewModel>();

                return PartialView("_InvoiceItem", viewModel);
            }
                return View();
        }
        // GET: InvoiceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvoiceController/Edit/5
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

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InvoiceController/Delete/5
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
