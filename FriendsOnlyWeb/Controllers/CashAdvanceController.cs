using Application.DTOs.CashAdvance;
using Application.DTOs.UserDtos;
using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels.CashAdvance;
using AutoMapper;
using FriendsOnlyWeb.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
    public class CashAdvanceController : Controller
    {
        private readonly ICashAdvanceService _cashAdvanceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? _UserLogged;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly IMapper _mapper;
        public CashAdvanceController
            (
            ICashAdvanceService cashAdvanceService,
            IHttpContextAccessor httpContextAccessor,
            ISavingsAccountService savingsAccountService,
            ICreditCardService creditCardService,
            IMapper mapper

            ) 
        {
            _cashAdvanceService = cashAdvanceService;
            _httpContextAccessor = httpContextAccessor;
            _UserLogged = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
            _savingsAccountService = savingsAccountService;
            _creditCardService = creditCardService;
            _mapper = mapper;
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async  Task<IActionResult> Create()
        { 
            var model = new CashAdvanceSaveVM
            {
                accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id),
                creditcards = await _creditCardService.GetAllViewModelService(_UserLogged?.Id)
            };
            return View("Create", model);
        }
        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create(CashAdvanceSaveVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.creditcards = await _creditCardService.GetAllViewModelService(_UserLogged?.Id);
                return View("Create", vm);
            }
            var card = await _creditCardService.GetByIdSaveViewModelService(vm.AccountNumber);
            if ( vm.amount > card.CreditLimit)
            {
                ModelState.AddModelError("AccountNumber", "el monte excede el limite de la tarjeta");
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.creditcards = await _creditCardService.GetAllViewModelService(_UserLogged?.Id);
                return View("Create", vm);
            }
            var paymentToAdd = _mapper.Map<CashAdvanceSaveDTO>(vm);
            paymentToAdd.UserId = _UserLogged?.Id;
            paymentToAdd.Interest = 6.25m;
            paymentToAdd.Amount = paymentToAdd.Amount + (paymentToAdd.Amount * paymentToAdd.Interest / 100);
            var paymentToAdded = await _cashAdvanceService.AddAService(paymentToAdd);
            if (paymentToAdded != null)
            {
                await _savingsAccountService.UpdateBalance(paymentToAdded.UserId, vm.amount, vm.SelectedAccountId);
                await _creditCardService.UpdateDebt(paymentToAdded.UserId, paymentToAdded.Amount, vm.AccountNumber);
            }
            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });

        }
    }
}
