using Application.DTOs.Payment;
using Application.DTOs.SavingsAccount;
using Application.DTOs.UserDtos;
using Application.Helpers;
using Application.Interfaces;
using Application.Service;
using Application.ViewModels.Payments.BeneficiaryPayment;
using Application.ViewModels.Payments.CreditCardPayment;
using Application.ViewModels.Payments.ExpressPayment;
using Application.ViewModels.Payments.LoanPayment;
using AutoMapper;
using FriendsOnlyWeb.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace NetBanking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly ICreditCardService _creditCardService;
        private readonly ILoanService _loanService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? _UserLogged;
        public PaymentController(IPaymentService IPaymentService,
            IMapper mapper,
            IBeneficiaryService beneficiaryService,
            ISavingsAccountService savingsAccountService,
            ICreditCardService creditCardService,
            IHttpContextAccessor httpContextAccessor,
            ILoanService loanService
            )
        {
            _paymentService = IPaymentService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _beneficiaryService = beneficiaryService;
            _savingsAccountService = savingsAccountService;
            _loanService = loanService;
            _creditCardService = creditCardService;
            _UserLogged = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
            _loanService = loanService;
        }
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]

        public async Task<IActionResult> PaymentExpress()
        {
            var model = new ExpressPaymentVM
            {
                accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id) 
            };

            return View("PaymentExpress", model);
        }

        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> PaymentExpress(ExpressPaymentVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                return View("PaymentExpress", vm);
            }

            var accountexist = await _savingsAccountService.GetByIdSaveViewModelService(vm.AccountNumber);
            if (accountexist == null)
            {
                ModelState.AddModelError("AccountNumber", "Account number does not exist");
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                return View("PaymentExpress", vm);
            }
            var accountbalance = await _savingsAccountService.GetByIdSaveViewModelService(vm.SelectedAccountId);
            if(accountbalance.Balance < vm.amount)
            {
                ModelState.AddModelError("amount", "Insufficient balance");
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                return View("PaymentExpress", vm);
            }

            
            var paymentToAdd = _mapper.Map<PaymentSaveDTO>(vm);
            paymentToAdd.UserId = _UserLogged?.Id;
            paymentToAdd.Date = DateTime.Now;
            var paymentToAdded = await _paymentService.AddAService(paymentToAdd);

            if (paymentToAdded != null)
            {
                await _savingsAccountService.UpdateBalance(paymentToAdded.UserId, vm.amount, vm.AccountNumber);
                await _savingsAccountService.UpdateBalance(paymentToAdded.UserId, -vm.amount, vm.SelectedAccountId);
            }

            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });
        }

        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreditCardPayment()
        {
            var model = new CreditCardPaymentVM
            {
                accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id),
                creditcards = await _creditCardService.GetAllViewModelService(_UserLogged?.Id)
            };

            return View("CreditCardPayment", model);
        }

        

        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreditCardPayment(CreditCardPaymentVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.creditcards = await _creditCardService.GetAllViewModelService(_UserLogged?.Id);
                return View("CreditCardPayment", vm);
            }
            var accountbalance = await _savingsAccountService.GetByIdSaveViewModelService(vm.SelectedAccountId);
            if (accountbalance.Balance < vm.amount)
            {
                ModelState.AddModelError("amount", "Insufficient balance");
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.creditcards = await _creditCardService.GetAllViewModelService(_UserLogged?.Id);
                return View("CreditCardPayment", vm);
            }
            var card = await _creditCardService.GetByIdSaveViewModelService(vm.AccountNumber);
            if(card.Debt == 0)
            {
                ModelState.AddModelError("AccountNumber", "No debe dinero de este tarjeta");
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.creditcards = await _creditCardService.GetAllViewModelService(_UserLogged?.Id);
                return View("CreditCardPayment", vm);
            }
            if (vm.amount >= card.Debt )
            {
                vm.amount = card.Debt;
            }
            var paymentToAdd = _mapper.Map<PaymentSaveDTO>(vm);
            paymentToAdd.UserId = _UserLogged?.Id;
            paymentToAdd.Date = DateTime.Now;
            var paymentToAdded = await _paymentService.AddAService(paymentToAdd);
            if(paymentToAdded != null)
            {
                await _savingsAccountService.UpdateBalance(paymentToAdded.UserId, -vm.amount, vm.SelectedAccountId);
                await _creditCardService.UpdateDebt(paymentToAdded.UserId, -vm.amount, vm.AccountNumber);
            }
            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });

        }

        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> LoanPayment()
        {
            var model = new LoanPaymentVM
            {
                accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id),
                loans = await _loanService.GetAllViewModelService(_UserLogged?.Id)
            };

            return View("LoanPayment", model);
        }
        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> LoanPayment(LoanPaymentVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.loans = await _loanService.GetAllViewModelService(_UserLogged?.Id);
                return View("LoanPayment", vm);
            }
            var accountbalance = await _savingsAccountService.GetByIdSaveViewModelService(vm.SelectedAccountId);
            if (accountbalance.Balance < vm.amount)
            {
                ModelState.AddModelError("amount", "Insufficient balance");
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.loans = await _loanService.GetAllViewModelService(_UserLogged?.Id);
                return View("LoanPayment", vm);
            }
            var loan = await _loanService.GetByIdSaveViewModelService(vm.AccountNumber);
            if (loan.Debt == 0)
            {
                ModelState.AddModelError("AccountNumber", "No debe dinero de este prestamo");
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.loans = await _loanService.GetAllViewModelService(_UserLogged?.Id);
                return View("LoanPayment", vm);
            }
            if (vm.amount >= loan.Debt)
            {
                vm.amount = loan.Debt;
            }
            var paymentToAdd = _mapper.Map<PaymentSaveDTO>(vm);
            paymentToAdd.UserId = _UserLogged?.Id;
            paymentToAdd.Date = DateTime.Now;
            var paymentToAdded = await _paymentService.AddAService(paymentToAdd);
            if (paymentToAdded != null)
            {
                await _savingsAccountService.UpdateBalance(paymentToAdded.UserId, -vm.amount, vm.SelectedAccountId);
                await _loanService.UpdateDebt(paymentToAdded.UserId, -vm.amount, vm.AccountNumber);
            }
            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });
        }

        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> BeneficiaryPayment()
        {
            var model = new BeneficiaryPayemtnVM
            {
                accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id),
                beneficiaries = await _beneficiaryService.GetAllDto(_UserLogged?.Id)
            };

            return View("BeneficiaryPayment", model);
        }

        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> BeneficiaryPayment(BeneficiaryPayemtnVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                vm.beneficiaries = await _beneficiaryService.GetAllDto(_UserLogged?.Id);
                return View("BeneficiaryPayment", vm);
            }
            var accountbalance = await _savingsAccountService.GetByIdSaveViewModelService(vm.SelectedAccountId);
            if (accountbalance.Balance < vm.amount)
            {
                ModelState.AddModelError("amount", "Insufficient balance");
                vm.accounts = await _savingsAccountService.GetAllViewModelService(_UserLogged?.Id);
                return View("BeneficiaryPayment", vm);
            }
            var paymentToAdd = _mapper.Map<PaymentSaveDTO>(vm);
            paymentToAdd.UserId = _UserLogged?.Id;
            paymentToAdd.Date = DateTime.Now;
            var paymentToAdded = await _paymentService.AddAService(paymentToAdd);

            if (paymentToAdded != null)
            {
                await _savingsAccountService.UpdateBalance(paymentToAdded.UserId, vm.amount, vm.AccountNumber);
                await _savingsAccountService.UpdateBalance(paymentToAdded.UserId, -vm.amount, vm.SelectedAccountId);
            }

            return RedirectToRoute(new { controller = "Home", action = "ClientHome" });

        }


    }
}
