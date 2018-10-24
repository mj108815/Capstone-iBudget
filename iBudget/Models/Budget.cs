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
        [ForeignKey("Transaction")]
        [Display(Name = "Transaction Name")]
        public int TransactionID { get; set; }
        public Transaction Transaction { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        [Display(Name = "Amount")]
        public double Amount { get; set; }
        [Display(Name = "Monthly Earnings")]
        public double MonthlyEarnings { get; set; }
        [Display(Name = "TotalAmount")]
        public double TotalAmount { get; set; }
    }
}
