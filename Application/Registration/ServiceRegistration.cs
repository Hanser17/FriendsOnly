using Application.Interfaces;
using Application.IRepository;
using Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application.Registration
{
    public static class ServiceRegistration
    {
        public static void AddServiceRegistration( this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<ISavingsAccountService, SavingsAccountService>();
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IBeneficiaryService, BeneficiaryService>();
            services.AddTransient<ICashAdvanceService, CashAdvanceService>();
            



        }

    }
}
