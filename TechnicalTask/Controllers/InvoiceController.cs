﻿using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using TechnicalTask.Models;

namespace TechnicalTask.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly HttpClient httpClient;

        //inject the HttpClient service to consume our Apis
        public InvoiceController(IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(baseUrl);
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
            HttpResponseMessage response = await httpClient.GetAsync($"Api/Store/Details");
            HttpResponseMessage response1 = await httpClient.GetAsync($"Api/Item");

            if (response.IsSuccessStatusCode && response1.IsSuccessStatusCode)
            {
                var stores = await response.Content.ReadAsAsync<List<Store>>();
                var items = await response1.Content.ReadAsAsync<List<Item>>();

                ViewBag.Stores = stores;
                ViewBag.Items = items;
                return View();
            }
            return View();

        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvoiceItemViewModel invoiceVm)
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
