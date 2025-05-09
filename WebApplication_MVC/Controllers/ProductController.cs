using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebApplication_MVC.Models.Entities;
using WebApplication_MVC.Models.Services;

namespace WebApplication_MVC.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View(_productService.GetAll());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(long id)
        {
            if(id < 0)
            {
                return NotFound();
            }

            var result = _productService.GetById(id);

            return View(result);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product pr)
        {
            try
            {
                if (ModelState.IsValid is not true)
                {
                    return BadRequest(ModelState);
                }
                 
                var result = _productService.Create(pr);

                if (result is not true)
                    throw new Exception();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(long id)
        {
            return View(_productService.GetById(id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var result = _productService.Delete(id);

                if (result is not true)
                    throw new Exception();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
