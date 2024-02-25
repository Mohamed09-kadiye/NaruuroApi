using System.Collections.Generic;

namespace NaruuroApi.Model.Interface
{
    public interface ICustomer
    {
        List<Customer> GetCustomers();
        Customer GetCustomerById(int customerId);
        void UpdateCustomer(Customer customer);
        void AddCustomer(Customer customer);
        void DeleteCustomer(int customerId);
    }

}
