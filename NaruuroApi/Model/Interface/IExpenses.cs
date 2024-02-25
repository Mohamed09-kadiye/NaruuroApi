using System;
namespace NaruuroApi.Model.Interface
{
    public interface IExpenseRepository
    {
        List<Expense> GetAllExpenses();
        Expense GetExpenseById(int expenseId);
        void InsertExpense(Expense expense);
        void UpdateExpense(Expense expense);
        void DeleteExpense(int expenseId);
    }


}

