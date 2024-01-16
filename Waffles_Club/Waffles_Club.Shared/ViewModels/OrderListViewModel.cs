using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;
using Waffles_Club.Data.Enum;

namespace Waffles_Club.Shared.ViewModels
{
    public class OrderListViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<CartsListViewModel> Carts { get; set; }
        public OrderStatus Status { get; set; }
    }
}
