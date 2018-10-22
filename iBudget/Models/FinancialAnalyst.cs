using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iBudget.Models
{
    public class FinancialAnalyst
    {
        [Key]
        public int FinancialAnalystID { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "City, State, Zip")]
        public string CityStateZip { get; set; }
        [Display(Name = "Give a brief description of the services you offer.")]
        public string Bio { get; set; }
        [Display(Name = "List and Special promotions of coupons here")]
        public string Promotions { get; set; }
        [Display(Name = "Link to your website.")]
        public string Link { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}
