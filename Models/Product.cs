using System;
using System.ComponentModel.DataAnnotations;

namespace ProductBarcodeManager.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(19)]
        [RegularExpression(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$", ErrorMessage = "รหัสสินค้าต้องอยู่ในรูปแบบ XXXX-XXXX-XXXX-XXXX และประกอบด้วยตัวอักษรภาษาอังกฤษพิมพ์ใหญ่หรือตัวเลขเท่านั้น")]
        public string ProductCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
