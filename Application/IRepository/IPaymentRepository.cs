using Domain.Entities;


namespace Application.IRepository
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<decimal> GetTotalPayments();
        Task<decimal> GetDailyPayments();
    }
}
