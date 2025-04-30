using Application.DTOs.Transfer;
using Application.DTOs.UserDtos;
using Application.Helpers;
using Application.Interfaces;
using Application.Service;
using Application.ViewModels.Transfer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NetBanking.Controllers
{
    public class TransferController : Controller
    {
        private readonly ITransferService _transferService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? _UserLogged;
        private readonly IMapper _Mapper;
        public TransferController
            (
            IHttpContextAccessor httpContextAccessor,
            ITransferService transferService,
            ISavingsAccountService savingsAccountService,
            IMapper mapper
            

            )
        { 
            _httpContextAccessor = httpContextAccessor;
            _transferService = transferService;
            _savingsAccountService = savingsAccountService;
            _Mapper = mapper;
            _UserLogged = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");

        }

        public async Task<IActionResult> Index()
        {
            var model = new TransferVM
            {
                from = await _savingsAccountService.GetAllViewModelService(_UserLogged.Id),
                To = await _savingsAccountService.GetAllViewModelService(_UserLogged.Id)
            };
            return View("Index", model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(TransferVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.from = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.To = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                return View("Index", vm);
            }
            if(vm.SourceAccountId == vm.DestinationAccountId)
            {
                ModelState.AddModelError("DestinationAccountId", "Source and Destination  account cannot be the same");
                vm.from = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.To = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                return View("Index", vm);
            }
            var account = await _savingsAccountService.GetByIdSaveViewModelService(vm.SourceAccountId);
            if (vm.Amount > account.Balance) 
            {
                ModelState.AddModelError("amount", "Insufficient balance");
                vm.from = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.To = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                return View("Index", vm);
            }
            var transferToAdd = _Mapper.Map<TransferSaveDTO>(vm);
            transferToAdd.UserId = _UserLogged.Id;
            transferToAdd.Date = DateTime.Now;
            var transferToAdded = await _transferService.AddAService(transferToAdd);
            if(transferToAdded != null)
            {
                await _savingsAccountService.UpdateBalance(transferToAdd.UserId, -vm.Amount, vm.SourceAccountId);
                await _savingsAccountService.UpdateBalance(transferToAdd.UserId, vm.Amount, vm.DestinationAccountId);
            }

            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });

        }
    }
}
