using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iBudget.Models
{
    public class Budget
    {
        [Key]
        public int BudgetID { get; set; }
        [Display(Name = "Transaction")]
        public string Transactions { get; set; }
         [Display(Name = "Amount")]
        public double Amount { get; set; }
        [Display(Name = "Monthly Earnings")]
        public double MonthlyEarnings { get; set; }
        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }
        [Display(Name = "Account Number")]
        public double AccountNumber { get; set; }

    }
}
