using Data.Entityes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Presentation.Models
{

    public class ProductViewModel : PageEditModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле наименование не должно быть пустым")]
        //[RegularExpression(@"^[^\s][a-zA-Zа-яА-Я\s]*$", ErrorMessage = "Запрещено вводить пробел перед текстом в поле 'Наименование'")]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int TimeWasted { get; set; }
        public DateTime DateEditor { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }

    public class ProductViewOrder
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateEditor { get; set; }
        public int? CategoryId { get; set; }
    }

}
