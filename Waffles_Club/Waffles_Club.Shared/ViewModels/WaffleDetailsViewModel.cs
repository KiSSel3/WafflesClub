using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Data.Entity;

namespace Waffles_Club.Shared.ViewModels
{
    public class WaffleDetailsViewModel
    {
        public Waffle Waffle { get; set; }
        public WaffleType WaffleType { get; set; }
        public FillingType FillingType { get; set; }
    }
}
