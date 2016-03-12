using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Greenlight.Models
{
    [Bind(Exclude="Id")]
    public partial class Order
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }
        [ScaffoldColumn(false)]
        public string Username { get; set; }
        [Required, Display(Name ="First Name"), StringLength(160)]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name"), StringLength(160)]
        public string LastName { get; set; }
        [Required, Display(Name = "Address"), StringLength(70)]
        public string Address { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        [Required, Display(Name = "City"), StringLength(40)]
        public string City { get; set; }
        [Required, Display(Name = "State"), StringLength(40)]
        public string State { get; set; }
        [Required, Display(Name = "Zip Code"), StringLength(10)]
        public string ZipCode { get; set; }
        [Required, DataType(DataType.EmailAddress), EmailAddress, Display(Name ="Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}