using Data.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using Presentation.Models;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Введите Ваше имя")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        public string CustomerPhone { get; set; } //+7(803) 363-47-69
        [Required(ErrorMessage = "Введите Ваш Email")]
        public string CustomerEmail { get; set; }
        [StringLength(150, MinimumLength = 0, ErrorMessage = "Длина строки должна быть от 0 до 150 символов")]
        public string CustomerComment { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        public decimal Price { get; set; }
        public bool Status { get; set; }  //status accept order
        public DateTime RegistrationTime { get; set; } // time registration order

        public int[] SelectProductsId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public List<Order_product> OrderProducts { get; set; }

        public List<OrderWorkTIme> OrderingTime { get; set; }

        [Range(typeof(bool), "true", "true",
            ErrorMessage = "Вы должны принять условия")]
        public bool TermsAccepted { get; set; }
    }

    public class OrderList
    {
        public Order Order { get; set; }
        public List<Product> Products { get; set; }
    }

    public class OrderWorkTIme
    {
        public DateTime OrderingDate { get; set; }
        public int TimeWasted { get; set; }
    }

    public class OrderAmoutAndTime
    {
        public int TimeWasted { get; set; }
        public decimal Price { get; set; }
    }




}
