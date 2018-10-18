using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iBudget.Models
{
    public class Ad
    {
        [Key]
        public int AdID { get; set; }

        [Display(Name = "Pay to make your post a sponsored post")]
        public bool AdPost { get; set; }

        [Display(Name = "Pay to make your ad image appear on the home page")]
        public bool Carousel { get; set; }

        public bool PaymentCollected { get; set; }

        public string CarouselImage { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
