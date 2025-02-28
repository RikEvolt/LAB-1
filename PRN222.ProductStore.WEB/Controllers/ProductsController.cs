using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!Request.Cookies.ContainsKey("UserId") || !Request.Cookies.ContainsKey("UserName"))
			{
				// Nếu không có cookie, chuyển hướng về trang đăng nhập
				context.Result = RedirectToAction("Login", "Account");
			}
			base.OnActionExecuting(context);
		}

		public async Task<IActionResult> Index()
        {
            return View(await _productService.GetProductsAsync());
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
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitslnStock,UnitPrice")] ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "CategoryId", "CategoryName", product.CategoryId);
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
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "CategoryId", "CategoryName", product.CategoryId);
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
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategoriesAsync(), "CategoryId", "CategoryName", product.CategoryId);
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
            var product = await _productService.GetProductByIdAsync((int)id);
            if (product != null)
            {
                await _productService.DeleteProductAsync(product.ProductId);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _productService.GetProductByIdAsync(id) != null;
		}
    }
}
