using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testdbfirst.Models;


namespace testdbfirst.Repository.ImplRepository
{
    public class CustomerImpl : ICustomer
    {
        ProductOderDemoContext db = new ProductOderDemoContext();


        public bool CreateCustomer(Customer RPC)
        {
            try
            {
                var addCustome = db.Customer.Add(RPC);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deleteCustomer(string id)
        {
            try
            {
                db.Customer.Remove(db.Customer.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditCustomer(string id, Customer RPC)
        {

            var findIdCustomer = db.Customer.Find(id);
            if (findIdCustomer != null)
            {
                findIdCustomer.CustomerAddress = RPC.CustomerAddress;
                findIdCustomer.CustomerEmail = RPC.CustomerEmail;
                findIdCustomer.CustomerLogin = RPC.CustomerLogin;
                findIdCustomer.CustomerName = RPC.CustomerName;
                findIdCustomer.CustomerOrders = RPC.CustomerOrders;
                findIdCustomer.CustomerPassword = RPC.CustomerPassword;
                findIdCustomer.CustomerPhone = RPC.CustomerPhone;
                findIdCustomer.CustomerName = RPC.CustomerName;
                db.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }



        }

        public IEnumerable<Customer> getAllCustomer()
        {
            return db.Customer.ToList();
        }

        public Customer getFindIDCustomer(string id)
        {
            return db.Customer.Find(id);
        }


    }
}
