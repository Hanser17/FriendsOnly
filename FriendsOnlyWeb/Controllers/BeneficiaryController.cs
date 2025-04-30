using Application.DTOs.Beneficiary;
using Application.DTOs.UserDtos;
using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels.Beneficiary;
using AutoMapper;
using FriendsOnlyWeb.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
   
    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _service;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse? _UserLogged;
        public BeneficiaryController(IBeneficiaryService beneficiaryService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISavingsAccountService savingsAccountService)
        {
            _service = beneficiaryService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _savingsAccountService = savingsAccountService;
            _UserLogged = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index()
        {
           var dtoList = await _service.GetAllDto(_UserLogged.Id);
           var vm = _mapper.Map<List<BeneficiaryVM>>(dtoList);
            return View("Index", vm);
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create()
        {
           
            var vm = new BeneficiarySaveVM 
            {

            };
            return View("Create", vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create(BeneficiarySaveVM vm)
        {
            var accountexisted = await _savingsAccountService.GetByIdSaveViewModelService(vm.AccountNumber);
            if (accountexisted == null)
            {
                ModelState.AddModelError("AccountNumber", "Account Does Not Exist");
                return View("Create", vm);
            }
            vm.UserId = accountexisted.UserId;
            vm.OwnerId = _UserLogged.Id;
            var save = _mapper.Map<BeneficiarySaveDTO>(vm); 
            var saved = await _service.AddAService(save);
            return RedirectToRoute(new { controller = "Beneficiary", action = "Index" });
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteService(id);
            return RedirectToRoute(new { controller = "Beneficiary", action = "Index" });
        }
    }
}
