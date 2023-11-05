using Application.ViewModels.Expense;
using AutoMapper;
using Domain.Entities.Budget.Expenses;

namespace Application.Mappers
{
    public class ExpenseMapperProfile : Profile
    {
        public ExpenseMapperProfile() {
            CreateMap<ExpenseDTO, Expense>().ReverseMap();
            CreateMap<FuelExpenseDTO, FuelExpense>().ReverseMap();
            CreateMap<MaintenanceExpenseDTO, MaintenanceExpense>().ReverseMap();
            CreateMap<OtherExpenseDTO, OtherExpense>().ReverseMap();
        }
    }
}
