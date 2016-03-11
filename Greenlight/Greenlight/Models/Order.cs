using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Greenlight.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        [Required, Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        [Required, Display(Name = "City")]
        public string City { get; set; }
        [Required, Display(Name = "State")]
        public string State { get; set; }
        [Required, Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}