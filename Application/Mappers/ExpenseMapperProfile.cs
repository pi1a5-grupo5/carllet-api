using Application.ViewModels.Expense;
using AutoMapper;
using Domain.Entities.Budget.Expenses;

namespace Application.Mappers
{
    public class ExpenseMapperProfile : Profile
    {
        public ExpenseMapperProfile() {
            CreateMap<ExpenseRequest, Expense>();
            CreateMap<FuelExpenseRequest, FuelExpense>();
            CreateMap<MaintenanceExpenseRequest, MaintenanceExpense>();
            CreateMap<OtherExpenseRequest, OtherExpense>();
            CreateMap<Expense, ExpenseResponse>();
            CreateMap<FuelExpense, FuelExpenseResponse>();
            CreateMap<MaintenanceExpense, MaintenanceExpenseResponse>();
            CreateMap<OtherExpense, OtherExpenseResponse>();

            CreateMap<FuelExpenseType, ExpenseTypeResponse>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.FuelExpenseName))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.FuelExpenseTypeId));
            CreateMap<MaintenanceExpenseType, ExpenseTypeResponse>()
                                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.MaintenanceName))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.MaintenanceExpenseTypeId));
            CreateMap<OtherExpenseType, ExpenseTypeResponse>()
                                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.OtherExpenseName))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.OtherExpenseTypeId));


        }
    }
}
