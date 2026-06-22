using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductBarcodeManager.Data;
using ProductBarcodeManager.Models;

namespace ProductBarcodeManager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .OrderBy(p => p.Id)
                .ToListAsync();
            return View(products);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                TempData["ErrorMessage"] = "กรุณากรอกรหัสสินค้า";
                return RedirectToAction(nameof(Index));
            }

            // Normalise to uppercase
            productCode = productCode.Trim().ToUpper();

            // Validate against regex: ^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$
            var regex = new System.Text.RegularExpressions.Regex(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$");
            if (!regex.IsMatch(productCode))
            {
                TempData["ErrorMessage"] = "รหัสสินค้าไม่ถูกต้องตามฟอร์แมต (ต้องมีความยาว 16 หลัก และอยู่ในรูปแบบ XXXX-XXXX-XXXX-XXXX)";
                return RedirectToAction(nameof(Index));
            }

            // Check for duplicates
            var exists = await _context.Products.AnyAsync(p => p.ProductCode == productCode);
            if (exists)
            {
                TempData["ErrorMessage"] = "รหัสสินค้านี้มีอยู่ในระบบแล้ว";
                return RedirectToAction(nameof(Index));
            }

            var product = new Product
            {
                ProductCode = productCode,
                CreatedAt = DateTime.Now
            };

            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "เพิ่มรหัสสินค้าสำเร็จ";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "ไม่พบรหัสสินค้าที่ต้องการลบ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "ลบรหัสสินค้าสำเร็จ";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
