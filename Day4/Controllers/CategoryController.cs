
using Microsoft.AspNetCore.Mvc;
using Day4.Models;
using Day4.Data;

namespace Day4.Controllers
{
    public class CategoryController : Controller
    {
        // Hien thi danh sach
        public IActionResult Index()
        {
            return View(SeedData.categories);
        }

        // Hien thi form them
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Xu ly them danh muc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = SeedData.categories.Any()
                    ? SeedData.categories.Max(c => c.Id) + 1
                    : 1;

                SeedData.categories.Add(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // Hien thi form sua
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = SeedData.categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // Xu ly sua danh muc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var oldCategory = SeedData.categories
                    .FirstOrDefault(c => c.Id == id);

                if (oldCategory == null)
                    return NotFound();

                oldCategory.Name = category.Name;

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // Hien thi xac nhan xoa
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = SeedData.categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // Xu ly xoa danh muc
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = SeedData.categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            // Khong xoa danh muc dang co san pham
            if (SeedData.products.Any(p => p.CategoryId == id))
            {
                ModelState.AddModelError("",
                    "Khong the xoa danh muc dang co san pham.");

                return View("Delete", category);
            }

            SeedData.categories.Remove(category);

            return RedirectToAction("Index");
        }
    }
}
