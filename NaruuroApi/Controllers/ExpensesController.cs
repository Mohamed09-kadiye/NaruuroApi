using Microsoft.AspNetCore.Mvc;
using NaruuroApi.Model;
using NaruuroApi.Model.Interface;

namespace NaruuroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpensesController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        [HttpGet]
        public IActionResult GetAllExpenses()
        {
            var expenses = _expenseRepository.GetAllExpenses();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public IActionResult GetExpenseById(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);
            if (expense == null)
                return NotFound();
            return Ok(expense);
        }

        [HttpPost]
        public IActionResult InsertExpense([FromBody] Expense expense)
        {
            _expenseRepository.InsertExpense(expense);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, [FromBody] Expense expense)
        {
            var existingExpense = _expenseRepository.GetExpenseById(id);
            if (existingExpense == null)
                return NotFound();

            expense.Id = id; // Make sure the ID is set correctly
            _expenseRepository.UpdateExpense(expense);
            return Ok();
        }

        // Implement the DeleteExpense method in a similar fashion.
    }
}
