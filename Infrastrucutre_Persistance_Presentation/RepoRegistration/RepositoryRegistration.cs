
using Application.IRepository;
using Infrastrucutre_Persistance_Presentation.DBContext;
using Infrastrucutre_Persistance_Presentation.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastrucutre_Persistance_Presentation.RepoRegistration
{
    public static class RepositoryRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<NetBankingContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               m => m.MigrationsAssembly(typeof(NetBankingContext).Assembly.FullName)));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ISavingsAccountRepository, SavingsAccountRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();
            services.AddTransient<IBeneficiaryRepository, BeneficiaryRepository>();
            services.AddTransient<ICashAdvanceRepository, CashAdvanceRepository>();
            



        }

    }
}
