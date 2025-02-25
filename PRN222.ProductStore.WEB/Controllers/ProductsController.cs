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

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
                
            var product = await _productService.GetProductsAsync();
            return View(product.ToList());
        }

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

		public async Task<IActionResult> Create()
		{
			var categories = await _categoryService.GetCategoriesAsync(); // Chờ lấy dữ liệu từ service
			ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
			return View();
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitslnStock,UnitPrice")] ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
			var categories = await _categoryService.GetCategoriesAsync();
			ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
			return View(product);
        }

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
			var categories = await _categoryService.GetCategoriesAsync();
			ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
			return View(product);
        }

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
			var categories = await _categoryService.GetCategoriesAsync();
			ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
			return View(product);
        }

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
