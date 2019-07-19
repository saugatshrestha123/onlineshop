using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClientNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saugatshrestha.Data;
using Saugatshrestha.Models;
using static ClientNotifications.Helpers.NotificationHelper;

namespace Saugatshrestha.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IClientNotification clientNotification;

        public ProductController(ApplicationDbContext context,
                                IClientNotification clientNotification)
        {
            this.context = context;
            this.clientNotification = clientNotification;
        }
        public IActionResult Index()
        {
            var products = context.products.ToList();

            return View(products);
        }
        [Authorize(Roles ="Admin,Manager")]
        public IActionResult New()
        {
            return View(new Product());
        }
        [Authorize]
        public IActionResult Save(Product product, IFormFile file)
        {

            if (!ModelState.IsValid)
                return View(nameof(New));


            if (file != null)
            {
                string filepath = UploadImage(file);
                product.ImageUrl = filepath;
            }

                context.products.Add(product);
                context.SaveChanges();

            clientNotification.AddSweetNotification("Product Save Status", "Product has been saved", NotificationType.success);
           
            return RedirectToAction(nameof(Index));
        
        }

        [Authorize(Roles="Manager")]
        public IActionResult Delete(int id)
        {
            var products = context.products.FirstOrDefault(x => x.Id == id);
            context.products.Remove(products);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize]
        public IActionResult Update(int id)
        {
            var products = context.products.FirstOrDefault(x => x.Id == id);

           
            return View(products);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Update(Product products, IFormFile file)
        {
            products.ImageUrl = UploadImage(file);
            context.products.Update(products);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Search(string search=null)
        {
            var products = context.products.Where(x => x.Item.Contains(search) || search == null).ToList();
            return View(nameof(Index), products);
        }
        public string UploadImage(IFormFile file)
        {
            var fileName = file.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
        public IActionResult Addorder(int productId, decimal quantity)
        {
            var product = context.products.FirstOrDefault(x => x.Id == productId);
            var order = new Order();
            order.Item = product.Item;
            order.Quantity = quantity;
            order.Unit = product.Unit;
            order.Rate = product.Rate;
            order.Image = product.ImageUrl;

            context.orders.Add(order);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ViewOrderDetails()
        {
            var order = context.orders.ToList();
            return View(order);
        }
    }
}