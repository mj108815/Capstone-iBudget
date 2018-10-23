using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBudget.Models
{
    public class Budget
    {
        [Key]
        public int BudgetID { get; set; }
        [Display(Name = "Transaction")]
        public string Transaction { get; set; }
        [Display(Name = "Amount")]
        public double Amount { get; set; }
        [Display(Name = "Monthly Earnings")]
        public double MonthlyEarnings { get; set; }
        [Display(Name = "TotalAmount")]
        public double TotalAmount { get; set; }

    }
}
