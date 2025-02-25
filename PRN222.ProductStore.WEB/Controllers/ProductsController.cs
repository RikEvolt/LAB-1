using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN222.ProductStore.Service.BusinessModels;
using PRN222.ProductStore.Service.Services;

namespace PRN222.ProductStore.WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var product = await _productService.GetProductsAsync();
			return View(product.ToList());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

		// GET: Products/Create
		public async Task<IActionResult> Create()
		{
			var categories = await _categoryService.GetCategories(); // Chờ lấy dữ liệu từ service
			ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
			return View();
		}


		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitslnStock,UnitPrice")] ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
			var categories = await _categoryService.GetCategories();
			ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
			return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync((int)id);
			if (product == null)
            {
                return NotFound();
            }
			var categories = await _categoryService.GetCategories();
			ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
			return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId,UnitslnStock,UnitPrice")] ProductDTO product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _productService.UpdateProductAsync(product);
				}
                catch (Exception)
                {
                    if (!await ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
			var categories = await _categoryService.GetCategories();
			ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
			return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync((int)id);
			if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
			if (product != null)
            {
                await _productService.DeleteProductAsync(product);
			}

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
			return await _productService.GetProductByIdAsync(id) != null;
		}
    }
}
