using Application.DTOs.CashAdvance;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICashAdvanceService: IGenericService<CashAdvanceSaveDTO, CashAdvanceDTO, CashAdvance>
    {

    }
}
