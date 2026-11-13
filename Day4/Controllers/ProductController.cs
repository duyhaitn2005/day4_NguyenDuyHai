
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Day4.Models;
using Day4.Data;

namespace Day4.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            foreach (var product in SeedData.products)
            {
                product.Category = SeedData.categories
                    .FirstOrDefault(c => c.Id == product.CategoryId);
            }

            return View(SeedData.products);
        }

        private void LoadCategories()
        {
            ViewBag.Categories = new SelectList(
                SeedData.categories, "Id", "Name");
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!SeedData.categories.Any(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Select a valid category");
            }

            if (ModelState.IsValid)
            {
                product.Id = SeedData.products.Any()
                    ? SeedData.products.Max(p => p.Id) + 1
                    : 1;

                SeedData.products.Add(product);

                return RedirectToAction("Index");
            }

            LoadCategories();
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = SeedData.products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            LoadCategories();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            if (!SeedData.categories.Any(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Select a valid category");
            }

            if (ModelState.IsValid)
            {
                var oldProduct = SeedData.products
                    .FirstOrDefault(p => p.Id == id);

                if (oldProduct == null)
                    return NotFound();

                oldProduct.Name = product.Name;
                oldProduct.Price = product.Price;
                oldProduct.Description = product.Description;
                oldProduct.CategoryId = product.CategoryId;

                return RedirectToAction("Index");
            }

            LoadCategories();
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = SeedData.products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            product.Category = SeedData.categories
                .FirstOrDefault(c => c.Id == product.CategoryId);

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = SeedData.products
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            SeedData.products.Remove(product);

            return RedirectToAction("Index");
        }
    }
}
