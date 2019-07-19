//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Saugatshrestha.Data;
//using Saugatshrestha.Models;


//namespace Saugatshrestha.Controllers
//{
//    public class OrderController : Controller
//    {
//        private readonly ApplicationDbContext context;

//        public OrderController(ApplicationDbContext context)
//        {
//            this.context = context;
//        }

//        //public IActionResult AddOrder(int productId, decimal quantity)
//        //{
//        //    var product = context.Products.FirstOrDefault(x => x.Id == productId);

//        //    var order = new Order();

//        //    order.Item = product.ProductName;
//        //    order.Quantity = quantity;
//        //    order.Unit = product.unit;
//        //    order.Rate = product.Rate;
           


//        //    if (product.Quantity < quantity)
//        //    {
//        //        return RedirectToAction(nameof(Index));
//        //    }

//        //    context.Orders.Add(order);

//        //    product.Quantity = product.Quantity - quantity;
//        //    context.Products.Update(product);

//        //    context.SaveChanges();
//        //    return RedirectToAction("Index");
//        //}
//        public IActionResult Addorder(int productId , decimal quantity)
//        {
//            var product = context.products.FirstOrDefault(x => x.Id == productId);
//            var order = new Order();
//            order.Item = product.Item;
//            order.Quantity = quantity;
//            order.Unit = product.Unit;
//            order.Rate = product.Rate;
//            order.Image = product.ImageUrl;

//            context.orders.Add(order);
//            context.SaveChanges();
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}