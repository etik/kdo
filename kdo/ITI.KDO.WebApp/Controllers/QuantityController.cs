using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.QuantityViewModels;
using ITI.KDO.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class QuantityController : Controller
    {
        readonly QuantityServices _quantityService;

        public QuantityController(QuantityServices quantityService)
        {
            _quantityService = quantityService;
        }

        [HttpGet("{quantityId}", Name = "GetQuantity")]
        public IActionResult GetQuantityById(int quantityId)
        {
            Result<ItemQuantity> result = _quantityService.GetById(quantityId);
            return this.CreateResult<ItemQuantity, QuantityViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToQuantityViewModel();
            });
        }

        [HttpPost]
        public IActionResult CreatePresent([FromBody] QuantityViewModel model)
        {
            Result<ItemQuantity> result = _quantityService.CreateQuantity(model.QuantityId, model.Quantity, model.RecipientId, model.NominatorId, model.EventId, model.PresentId);
            return this.CreateResult<ItemQuantity, QuantityViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToQuantityViewModel();
                o.RouteName = "GetQuantity";
                o.RouteValues = s => new { id = s.QuantityId };
            });
        }

        [HttpPut("{quantityId}")]
        public IActionResult UpdatePresent(int quantityId, [FromBody] QuantityViewModel model)
        {
            Result<ItemQuantity> result = _quantityService.UpdateQuantity(model.QuantityId, model.Quantity, model.RecipientId, model.NominatorId, model.EventId, model.PresentId);
            return this.CreateResult<ItemQuantity, QuantityViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToQuantityViewModel();
            });
        }

        [HttpDelete("{quantityId}")]
        public IActionResult DeletePresent(int quantityId)
        {
            Result<int> result = _quantityService.Delete(quantityId);
            return this.CreateResult(result);
        }
    }
}
