using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiddleLayer;

namespace DataAccessLayer
{
    public  class CustomerDal
    {
        private List<CustomerBase> listOfCustomer = new List<CustomerBase>();
        public CustomerDal()
        {
            if (listOfCustomer.Count == 0)
            {
                listOfCustomer.Add(new LeadCustomer { CustomerName="aa",PhoneNumber="121212" ,CustomerType= "LeadCustomer" });
                listOfCustomer.Add(new LeadCustomer { CustomerName = "bb", PhoneNumber = "121212", CustomerType = "LeadCustomer" });
                listOfCustomer.Add(new LeadCustomer { CustomerName = "cc", PhoneNumber = "121212", CustomerType = "LeadCustomer" });
            }
        }
        public  string AddCustomer(CustomerBase customer)
        {
            string message = string.Empty;
            try {
                if (listOfCustomer.Any(x => x.CustomerName == customer.CustomerName))
                {
                    message = "Customer already exist in our record";
                }
                else
                {
                    listOfCustomer.Add(customer);
                    message = "SUCCESS";
                }                
            }
            catch (Exception ex)
            {
                message = "FAILED";
            }
            return message;
        }
        public  IEnumerable<CustomerBase> GetAllCustomers()
        {
            return listOfCustomer;
        }
        public  string DeleteCustomer(CustomerBase customer)
        {
            string message = string.Empty;
            try
            {
                if (!listOfCustomer.Any(x => x.CustomerName == customer.CustomerName))
                {
                    message = "Customer does not exist in our record";
                }
                else
                {
                    listOfCustomer.Remove(customer);
                    message = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                message = "FAILED";
            }
            return message;
        }
        public  CustomerBase GetCustomer(string name)
        {
            CustomerBase customerObj = null;
            try
            {
                if (listOfCustomer.Any(x => x.CustomerName == name))
                {
                    customerObj = listOfCustomer.First(x => x.CustomerName == name);
                }                
            }
            catch (Exception ex)
            {
               // log error;
            }
            return customerObj;
        }
        public  CustomerBase GetCustomerByIndex(int index)
        {
            CustomerBase customerObj = null;
            try
            {
                if (listOfCustomer.Count>index)
                {
                    customerObj = listOfCustomer.ElementAt(index);
                }
            }
            catch (Exception ex)
            {
                // log error;
            }
            return customerObj;
        }
        public string UpdateCustomer(CustomerBase customer)
        {
            string message = string.Empty;
            try
            {
                if (listOfCustomer.Any(x => x.CustomerName == customer.CustomerName))
                {
                    var existingCustomer = listOfCustomer.Where(x => x.CustomerName == customer.CustomerName).First();
                    listOfCustomer.Remove(existingCustomer);
                    listOfCustomer.Add(customer);
                    message = "Customer record updated successfully";
                }
                else
                {                    
                    message = "unable to update customer right now, please try again later";
                }
            }
            catch (Exception ex)
            {
                message = "FAILED";
            }
            return message;
        }
    }
}
