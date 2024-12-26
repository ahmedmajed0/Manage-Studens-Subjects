    using BL.Dtos;
using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AppEnums;
using Microsoft.AspNetCore.Authorization;

namespace Ui.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ShippingTypesController : Controller
    {
        private readonly IShippingType _IShippingType;
        public ShippingTypesController(IShippingType shippingType)
        {
            _IShippingType = shippingType;
        }
        public IActionResult Index()
        {
            return View(_IShippingType.GetAll());
        }

        public IActionResult Edit(Guid? id)
        {
            ShippingTypeDto model = new ShippingTypeDto();
            if (id != null)
            {
                model = _IShippingType.GetById((Guid)id);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ShippingTypeDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["MessageType"] = MeesagesType.ValidationError;
                    return View(nameof(Edit), model);                   
                }

                if (model.Id == Guid.Empty)
                {
                    _IShippingType.Add(model);
                }
                else
                {
                    _IShippingType.Update(model);
                }
                TempData["MessageType"] = MeesagesType.SaveSuccess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MeesagesType.SaveFailed;
            }
            return RedirectToAction(nameof(Index));

        }


        public IActionResult Delete(Guid id)
        {
            try
            {
                _IShippingType.ChangeStatus(id);
                TempData["MessageType"] = MeesagesType.DeleteSuccess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MeesagesType.DeleteFailed;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
