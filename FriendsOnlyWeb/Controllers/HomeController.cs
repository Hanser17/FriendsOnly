using Application.DTOs.UserDtos;
using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels.User.Admin;
using Application.ViewModels.User.Client;
using FriendsOnlyWeb.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FriendsOnlyWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly ITransferService _transferService;
        private readonly ICreditCardService _creditCardService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? _UserLogged;
        private readonly ILoanService _loanService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public HomeController
            (
            ITransferService transferService,
            IPaymentService paymentService,
            IUserService userService,
            ICreditCardService creditCardService,
            ISavingsAccountService savingsAccountService,
            ILoanService loanService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _transferService = transferService;
            _paymentService = paymentService;
            _userService = userService;
            _creditCardService = creditCardService;
            _httpContextAccessor = httpContextAccessor;
            _savingsAccountService = savingsAccountService;
            _loanService = loanService;
            _UserLogged = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
        }

        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var totalTransactions = await _transferService.GetTotalTransactions(); // Total de transacciones
            var dailyTransactions = await _transferService.GetDailyTransactions(); // Transacciones de hoy
            var totalPayments = await _paymentService.GetTotalPayments(); // Total de pagos
            var dailyPayments = await _paymentService.GetDailyPayments(); // Pagos de hoy
            var activeCustomers = await _userService.GetActiveCustomers(); // Clientes activos
            var inactiveCustomers = await _userService.GetInactiveCustomers(); // Clientes inactivos
                                                                             

            var viewModel = new HomeViewModel
            {
                TotalTransactions = totalTransactions,
                DailyTransactions = dailyTransactions,
                TotalPayments = totalPayments,
                DailyPayments = dailyPayments,
                ActiveCustomers = activeCustomers,
                InactiveCustomers = inactiveCustomers,
               
            };

            return View(viewModel);
        }

        [ServiceFilter(typeof(LoginAuthorized))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ClientHome()
        {
            var SavingsAccount = await _savingsAccountService.GetAllViewModelService(_UserLogged.Id);
            var CreditCard = await _creditCardService.GetAllViewModelService(_UserLogged.Id);
            var Loan = await _loanService.GetAllViewModelService(_UserLogged.Id);

            var viewModel = new ClientHomeVm
            {
                SavingAccounts = SavingsAccount,
                CreditCards = CreditCard,
                Loans = Loan
            };

            return View("ClientHomeView",viewModel);
        }
        




    }
}
