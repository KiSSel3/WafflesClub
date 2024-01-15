using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waffles_Club.Shared.ViewModels
{
    public class OrderViewModel
    {
        public Guid WaffleId { get; set; }
        public int Count { get; set; }
    }
}
