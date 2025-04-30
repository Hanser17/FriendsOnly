using Application.DTOs.Loan;
using Application.Interfaces;
using Application.ViewModels.Loan;
using AutoMapper;
using FriendsOnlyWeb.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _service;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IMapper _mapper;
        public LoanController(ILoanService ILoanService, IMapper mapper, ISavingsAccountService savingsAccountService)
        {
            _service = ILoanService;
            _mapper = mapper;
            _savingsAccountService = savingsAccountService;
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string UserId)
        {
            var model = new LoanSaveVM { UserId = UserId };
            return View("CreateL", model);
        }
        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(LoanSaveVM vm)
        {
            vm.Debt = vm.Amount;
            var loanToAdd = _mapper.Map<LoanSaveDTO>(vm);
            var loanToAdded = await _service.AddAService(loanToAdd);
            if(loanToAdded != null)
            {
                await _savingsAccountService.UpdateBalance(vm.UserId, vm.Amount, null);
            }
               
            return RedirectToRoute(new { controller = "User", action = "AdministrationUsers" });
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Delete( int id)

        {
           
            await _service.DeleteService(id);
            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });
        }
    }
}
