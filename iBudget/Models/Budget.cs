using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Display(Name = "Transactions")]
        public List<SelectListItem> Transactions { get; } = new List<SelectListItem>
        {
        new SelectListItem { Value = "House", Text = "Mortgage/Rent" },
        new SelectListItem { Value = "House", Text = "House Insurance" },
        new SelectListItem { Value = "House", Text = "Maintenance" },
        new SelectListItem { Value = "Auto", Text = "Car Payment" },
        new SelectListItem { Value = "Auto", Text = "Car Insurance" },
        new SelectListItem { Value = "Auto", Text = "Gas" },
        new SelectListItem { Value = "Auto", Text = "Maintenance" },
        new SelectListItem { Value = "Utilties", Text = "Gas" },
        new SelectListItem { Value = "Utilties", Text = "Water" },
        new SelectListItem { Value = "Utilties", Text = "Electricity" },
        new SelectListItem { Value = "Utilties", Text = "Phone" },
        new SelectListItem { Value = "Utilties", Text = "Internet" },
        new SelectListItem { Value = "Utilties", Text = "Cable" },
        new SelectListItem { Value = "Food", Text = "Groceries" },
        new SelectListItem { Value = "Food", Text = "Eating Out" },
        };
        [Display(Name = "Amount")]
        public double Amount { get; set; }
        [Display(Name = "Monthly Earnings")]
        public double MonthlyEarnings { get; set; }
        [Display(Name = "TotalAmount")]
        public double TotalAmount { get; set; }
    }
}
