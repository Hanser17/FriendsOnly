using Application.DTOs.CreditCard;
using Application.Interfaces;
using Application.ViewModels.CreditCard;
using AutoMapper;
using FriendsOnlyWeb.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _service;
        private readonly IMapper _mapper;
        public CreditCardController(ICreditCardService ICreditCardService, IMapper mapper)
        {
            _service = ICreditCardService;
            _mapper = mapper;
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string UserId)
        {
            var model = new CreditCardSaveVM { UserId = UserId };
            return View("CreateCC", model);
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreditCardSaveVM vm)
        {
            vm.Debt = 0;
            var creditcardToAdd = _mapper.Map<CreditCardSaveDTO>(vm);
            var creditcardToAdded = await _service.AddAService(creditcardToAdd);
            return RedirectToRoute(new { controller = "User", action = "AdministrationUsers" });
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteService(id);
            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });
        }
    }
}
