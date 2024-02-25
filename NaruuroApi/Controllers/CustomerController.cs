using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NaruuroApi.Model.Interface;
using NaruuroApi.Model;
using NaruuroApi.Model.Repository;

namespace  NaruuroApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
       private readonly ICustomer _customer;

        public CustomerController(ICustomer customerrepo)
        {
            _customer= customerrepo;
        }
        [HttpGet]
        public IActionResult GetCustomers()
        {
            List<Customer> customers = _customer.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(int customerId)
        {
            Customer customer = _customer.GetCustomerById(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        [HttpPost]
        
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            try
            {
                _customer.AddCustomer(customer);
                
                return Ok(); // Return a 200 OK response if the customer was added successfully
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the customer: {ex.Message}");
            }
        }


        [HttpPut("{customerId}")]
        public IActionResult UpdateCustomer(int customerId, Customer updatedCustomer)
        {
            Customer existingCustomer = _customer.GetCustomerById(customerId);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Names = updatedCustomer.Names;
            existingCustomer.Gender = updatedCustomer.Gender;
            existingCustomer.Tell = updatedCustomer.Tell;
            existingCustomer.Address = updatedCustomer.Address;
            existingCustomer.ID_Documents = updatedCustomer.ID_Documents;

            _customer.UpdateCustomer(existingCustomer);

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomer(int customerId)
        {
            Customer existingCustomer = _customer.GetCustomerById(customerId);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            _customer.DeleteCustomer(customerId);

            return NoContent();
        }
    }
}
