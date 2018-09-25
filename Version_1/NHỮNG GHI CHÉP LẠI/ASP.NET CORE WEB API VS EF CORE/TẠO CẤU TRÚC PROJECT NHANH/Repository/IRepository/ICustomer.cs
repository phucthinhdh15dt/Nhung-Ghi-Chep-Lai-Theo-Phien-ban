using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testdbfirst.Models;

namespace testdbfirst.Repository.ImplRepository
{
    public interface ICustomer
    {
        IEnumerable<Customer> getAllCustomer();
        Customer getFindIDCustomer(string id);
        bool CreateCustomer(Customer RPC);
        bool EditCustomer(string id, Customer RPC);
        bool deleteCustomer(string id);

    }
}
