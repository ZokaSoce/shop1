using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop1.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
        public string DeliveryAddress { get; set; }
        public List<CartProduct> CartProduct { get; set; }

        //public User User { get; set; }
    }
}
