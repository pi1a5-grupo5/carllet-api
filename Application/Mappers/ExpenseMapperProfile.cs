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

            CreateMap<FuelExpenseType, ExpenseResponse>();
            CreateMap<MaintenanceExpenseType, ExpenseResponse>();
            CreateMap<OtherExpenseType, ExpenseResponse>();


        }
    }
}
