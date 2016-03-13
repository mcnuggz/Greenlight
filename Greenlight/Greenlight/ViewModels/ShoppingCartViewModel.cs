using Greenlight.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Greenlight.ViewModels
{
    public class ShoppingCartViewModel
    {
        [Key]
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}