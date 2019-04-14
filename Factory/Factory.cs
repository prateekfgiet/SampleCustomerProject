using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiddleLayer;

namespace FactoryCustomer
{
    public static class Factory
    {
        private static Dictionary<string, CustomerBase> customerDic = new Dictionary<string, CustomerBase>();
        
        public static CustomerBase Create(string customerType)
        {
            if (!customerDic.Any())
            {
                customerDic.Add("SaleCustomer", new SaleCustomer());
                customerDic.Add("LeadCustomer", new LeadCustomer());
            }
            return customerDic[customerType];
        }
    }
}
