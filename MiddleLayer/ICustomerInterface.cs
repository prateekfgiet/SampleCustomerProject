using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer
{       
    public class CustomerBase
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }
        public string CustomerType { get; set; }
        public virtual void Validate()
        {
            throw new NotImplementedException();
        }
    }
    public class SaleCustomer : CustomerBase
    {
        public override void Validate()
        {
            if (CustomerName == null || CustomerName.Length == 0)
            {
                throw new Exception("cuatomer name required");
            }
            if (PhoneNumber == null || PhoneNumber.Length == 0)
            {
                throw new Exception("phone number required");
            }
            if (BillAmount == 0)
            {
                throw new Exception("BillAmount required");
            }
            if (BillDate > DateTime.Now)
            {
                throw new Exception("BillDate cannot be future date");
            }
            if (Address == null || Address.Length == 0)
            {
                throw new Exception("Address required");
            }
        }
    }
    public class LeadCustomer:CustomerBase
    {
        public override void Validate()
        {
            if (CustomerName == null || CustomerName.Length == 0)
            {
                throw new Exception("cuatomer name required");
            }
            if (PhoneNumber == null || PhoneNumber.Length == 0)
            {
                throw new Exception("phone number required");
            }
          
        }
    }
}
