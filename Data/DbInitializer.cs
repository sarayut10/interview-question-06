using System;
using System.Linq;
using ProductBarcodeManager.Models;

namespace ProductBarcodeManager.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var products = new Product[]
            {
                new Product { ProductCode = "A1B2-C3D4-E5F6-G7H8", CreatedAt = DateTime.Now.AddMinutes(-90) },
                new Product { ProductCode = "9876-5432-ABCD-EFGH", CreatedAt = DateTime.Now.AddMinutes(-80) },
                new Product { ProductCode = "IT06-CODE-39BA-RCOD", CreatedAt = DateTime.Now.AddMinutes(-70) },
                new Product { ProductCode = "K1L2-M3N4-O5P6-Q7R8", CreatedAt = DateTime.Now.AddMinutes(-60) },
                new Product { ProductCode = "S9T0-U1V2-W3X4-Y5Z6", CreatedAt = DateTime.Now.AddMinutes(-50) },
                new Product { ProductCode = "A7B8-C9D0-E1F2-G3H4", CreatedAt = DateTime.Now.AddMinutes(-40) },
                new Product { ProductCode = "I5J6-K7L8-M9N0-O1P2", CreatedAt = DateTime.Now.AddMinutes(-30) },
                new Product { ProductCode = "Q3R4-S5T6-U7V8-W9X0", CreatedAt = DateTime.Now.AddMinutes(-20) },
                new Product { ProductCode = "Y1Z2-A3B4-C5D6-E7F8", CreatedAt = DateTime.Now.AddMinutes(-10) }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
