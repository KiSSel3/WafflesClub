using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffles_Club.Shared.ViewModels;

namespace Waffles_Club.Shared.Mappers
{
    public class WaffleMapper
    {
        public WaffleViewModel MapToWaffleViewModel(WaffleDetailsViewModel waffleDetails)
        {
            if (waffleDetails == null)
                throw new ArgumentNullException(nameof(waffleDetails));

            return new WaffleViewModel
            {
                TypeId = waffleDetails.WaffleType?.Id ?? Guid.Empty,
                FillingTypeId = waffleDetails.FillingType?.Id ?? Guid.Empty,
                Name = waffleDetails.Waffle?.Name,
                Description = waffleDetails.Waffle?.Description,
                ImageUrl = waffleDetails.Waffle?.ImageUrl,
                CountInPackage = waffleDetails.Waffle?.CountInPackage ?? 0,
                Price = waffleDetails.Waffle?.Price ?? 0,
                Weight = waffleDetails.Waffle?.Weight ?? 0
            };
        }
    }

}
