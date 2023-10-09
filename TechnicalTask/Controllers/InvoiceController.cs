using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using TechnicalTask.Models;
using AutoMapper;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalTask.Controllers
{
    [Authorize]
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
            try
            {

                HttpResponseMessage response = await httpClient.GetAsync("Api/Invoice");

                if (response.IsSuccessStatusCode)
                {
                    var invoicesVm = await response.Content.ReadAsAsync<List<InvoiceViewModel>>();
                    return View(invoicesVm);
                }
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        // GET: InvoiceController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            try
            {

                HttpResponseMessage response = await httpClient.GetAsync($"Api/Invoice/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var invoiceVm = await response.Content.ReadAsAsync<InvoiceViewModel>();
                    return View(invoiceVm);
                }
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: InvoiceController/Create
        public async Task<ActionResult> Create()
        {
            HttpResponseMessage storeResponse = await httpClient.GetAsync($"Api/Store");
            HttpResponseMessage itemResponse = await httpClient.GetAsync($"Api/Item");
            HttpResponseMessage unitRepsones = await httpClient.GetAsync($"Api/Unit");

            if (storeResponse.IsSuccessStatusCode && itemResponse.IsSuccessStatusCode && unitRepsones.IsSuccessStatusCode)
            {
                var stores = await storeResponse.Content.ReadAsAsync<List<StoreViewModel>>();
                var items = await itemResponse.Content.ReadAsAsync<List<Item>>();
                var units  = await unitRepsones.Content.ReadAsAsync<List<Unit>>();
                var invoiceVm = new InvoiceViewModel
                {
                    InvoiceItemsViewModel = new List<InvoiceItemViewModel>()
                };
                invoiceVm.Date = DateTime.Now;
                ViewBag.Stores = stores;
                ViewBag.Items = items;
                ViewBag.Units = units;
                return View(invoiceVm);
            }
            var stores1 = await storeResponse.Content.ReadAsAsync<List<StoreViewModel>>();
            var items1 = await itemResponse.Content.ReadAsAsync<List<Item>>();
            var units1 = await unitRepsones.Content.ReadAsAsync<List<Unit>>();

            ViewBag.Stores = stores1;
            ViewBag.Items = items1;
            ViewBag.Units = units1;

            return View();

        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvoiceViewModel invoiceVm)
        {
            // Manually bind the InvoiceItemsViewModel from the form data
            var invoiceItemsViewModel = new List<InvoiceItemViewModel>();
            foreach (var key in Request.Form.Keys)
            {
                if (key.StartsWith("InvoiceItemsViewModel"))
                {
                    var val = Request.Form[key].FirstOrDefault();
                    invoiceItemsViewModel = JsonConvert.DeserializeObject<List<InvoiceItemViewModel>>(val);

                    invoiceVm.InvoiceItemsViewModel = invoiceItemsViewModel;
                    break;
                }
            }
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Api/Invoice", invoiceVm);
            if (response.IsSuccessStatusCode)
            {
                // invoice created successfully
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the error case

                HttpResponseMessage storeResponse = await httpClient.GetAsync($"Api/Store");
                HttpResponseMessage itemResponse = await httpClient.GetAsync($"Api/Item");
                HttpResponseMessage unitRepsones = await httpClient.GetAsync($"Api/Unit");

                if (storeResponse.IsSuccessStatusCode && itemResponse.IsSuccessStatusCode && unitRepsones.IsSuccessStatusCode)
                {
                    var stores = await storeResponse.Content.ReadAsAsync<List<StoreViewModel>>();
                    var items = await itemResponse.Content.ReadAsAsync<List<Item>>();
                    var units = await unitRepsones.Content.ReadAsAsync<List<Unit>>();

                    ViewBag.Stores = stores;
                    ViewBag.Items = items;
                    ViewBag.Units = units;
                    return View(invoiceVm);
                }
                return View("Error");
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
