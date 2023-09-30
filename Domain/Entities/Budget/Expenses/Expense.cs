using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Budget.Expenses
{
    public abstract class Expense
    {
        public Guid ExpenseId { get; set; }
        public Guid UserVehicleId { get; set; }
        public decimal Value { get; set; }
        public UserVehicle UserVehicle { get; set; }

    }
}
