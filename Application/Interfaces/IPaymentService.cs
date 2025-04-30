using Application.DTOs.Payment;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentService :IGenericService<PaymentSaveDTO, PaymentDTO, Payment>
    {
        Task<decimal> GetTotalPayments();
        Task<decimal> GetDailyPayments();
    }
}
