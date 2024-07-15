using IMM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MAUI.ViewModels
{
    public class TaxRateViewModel
    {
        private double taxRate;
        public double TaxRate
        {
            get
            {
                return Math.Truncate(taxRate * 100);
            }
            set
            {
                taxRate = value;
                taxRate /= 100;
            }
        }

        public void UpdateTaxRate()
        {
            InventoryServiceProxy.Current.UpdateTaxRate(taxRate);
        }

        public TaxRateViewModel()
        {
            taxRate = InventoryServiceProxy.Current.TaxRate;
        }
    }
}
