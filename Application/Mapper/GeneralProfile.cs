
using Application.DTOs.Beneficiary;
using Application.DTOs.CashAdvance;
using Application.DTOs.CreditCard;
using Application.DTOs.Loan;
using Application.DTOs.Payment;
using Application.DTOs.SavingsAccount;
using Application.DTOs.Transfer;
using Application.DTOs.UserDtos;
using Application.ViewModels.Beneficiary;
using Application.ViewModels.CashAdvance;
using Application.ViewModels.CreditCard;
using Application.ViewModels.Loan;
using Application.ViewModels.Payments.BeneficiaryPayment;
using Application.ViewModels.Payments.CreditCardPayment;
using Application.ViewModels.Payments.ExpressPayment;
using Application.ViewModels.Payments.LoanPayment;
using Application.ViewModels.SavingAccount;
using Application.ViewModels.Transfer;
using Application.ViewModels.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class GeneralProfile : Profile 
    {
        public GeneralProfile()

        {

            CreateMap<Authenticationrequest, LoginUserViewModel>()
                 .ForMember(x => x.HasError, opt => opt.Ignore())
                 .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                 .ForMember(x => x.HasError, opt => opt.Ignore())
                 .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<AuthenticationResponse, SaveUserViewModel>()
                 .ForMember(x => x.HasError, opt => opt.Ignore())
                 .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => string.Join(", ", src.Roles)))
                 .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<string, Authenticationrequest>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src));

            CreateMap<AuthenticationResponse, RegisterRequest>()
                 .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => string.Join(", ", src.Roles)))
            .ReverseMap();

           

            #region Saving Account to DTO

            CreateMap<SavingsAccount, SavingsAccountSaveDTO>()
           .ReverseMap();

            CreateMap<SavingsAccount, SavingsAccountDTO>()
          .ReverseMap();

            CreateMap<SavingsAccountSaveVM, SavingsAccountSaveDTO>()
          .ReverseMap();
            #endregion


            #region Loan  to DTO

            CreateMap<Loan, LoanSaveDTO>()
           .ReverseMap();

            CreateMap<Loan, LoanDTO>()
           .ReverseMap();

            CreateMap<LoanSaveDTO, LoanSaveVM>()
           .ReverseMap();

            CreateMap<LoanDTO, LoanVM>()
          .ReverseMap();
            #endregion

            #region CreditCard  to DTO

            CreateMap<CreditCard, CreditCardDTO>()
           .ReverseMap();

            CreateMap<CreditCard, CreditCardSaveDTO>()
           .ReverseMap();

            CreateMap<CreditCardSaveDTO, CreditCardSaveVM>()
           .ReverseMap();

            CreateMap<CreditCardDTO, CreditCardVM>()
          .ReverseMap();

            #endregion
            #region Beneficiary  to DTO

            CreateMap<Beneficiary, BeneficiarySaveDTO>()
           .ReverseMap();

            CreateMap<Beneficiary, BeneficiaryDTO>()
           .ReverseMap();

            CreateMap<BeneficiaryDTO, BeneficiaryVM>()
           .ReverseMap();

            CreateMap<BeneficiarySaveDTO, BeneficiarySaveVM>()
          .ReverseMap();

            #endregion

            #region Payment  to DTO

            CreateMap<ExpressPaymentVM, PaymentSaveDTO>()
            .ForMember(dest => dest.SourceAccountId, opt => opt.MapFrom(src => src.SelectedAccountId)) 
            .ForMember(dest => dest.DestinationAccountId, opt => opt.MapFrom(src => src.AccountNumber)) 
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now)) 
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<CreditCardPaymentVM, PaymentSaveDTO>()
            .ForMember(dest => dest.SourceAccountId, opt => opt.MapFrom(src => src.SelectedAccountId))
            .ForMember(dest => dest.DestinationAccountId, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<LoanPaymentVM, PaymentSaveDTO>()
            .ForMember(dest => dest.SourceAccountId, opt => opt.MapFrom(src => src.SelectedAccountId))
            .ForMember(dest => dest.DestinationAccountId, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<BeneficiaryPayemtnVM, PaymentSaveDTO>()
            .ForMember(dest => dest.SourceAccountId, opt => opt.MapFrom(src => src.SelectedAccountId))
            .ForMember(dest => dest.DestinationAccountId, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<Payment, PaymentSaveDTO>()
                .ReverseMap();
            #endregion
            #region CashAdvance  to DTO 
            
                 CreateMap<CashAdvanceSaveVM, CashAdvanceSaveDTO>()
            .ForMember(dest => dest.CreditCardId, opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(dest => dest.DestinationAccountId, opt => opt.MapFrom(src => src.SelectedAccountId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<CashAdvance, CashAdvanceSaveDTO>()
                .ReverseMap();
            #endregion

            #region Tranfer  to DTO 

            CreateMap<TransferVM, TransferSaveDTO>()
                .ReverseMap();

            CreateMap<Transfer, TransferSaveDTO>()
                .ReverseMap();
            #endregion

        }

    }
}
