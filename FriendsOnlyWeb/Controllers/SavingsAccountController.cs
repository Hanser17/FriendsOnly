using Application.DTOs.SavingsAccount;
using Application.Interfaces;
using Application.ViewModels.SavingAccount;
using AutoMapper;
using FriendsOnlyWeb.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
    public class SavingsAccountController : Controller
    {
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IMapper _mapper;
        public SavingsAccountController(ISavingsAccountService savingsAccountService, IMapper mapper ) 
        {
            _savingsAccountService = savingsAccountService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string UserId)
        {
            var model = new SavingsAccountSaveVM { UserId = UserId };
            return View("CreateSA", model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(SavingsAccountSaveVM vm)
        {
            var accountToAdd = _mapper.Map<SavingsAccountSaveDTO>(vm);
            var accountToAdded = await _savingsAccountService.AddAService(accountToAdd);
            return RedirectToRoute(new { controller = "User", action = "AdministrationUsers" });
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        public async Task<IActionResult> Delete(string userId, decimal balance, int id)
        {
            await _savingsAccountService.UpdateBalance(userId, balance, null);
            await _savingsAccountService.DeleteService(id);
            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
