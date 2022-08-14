using System.ComponentModel.DataAnnotations;
using Core.Model;
using Core;
namespace SuperDairy.Models.Admin
{
    public class CollectMilkModelView
    {
        [Editable(false)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name = "User")]
        public int UserId { get; set; }
        [Display(Name = "Date")]
        public TimeBatch Batch { get; set; } = TimeBatch.MORNING;
        public DateTime Date { get; set; }
        [Display(Name = "Milk Type")]
        public MilkType MilkType { get; set; }
        
       
        public float Fat { get; set; }
        [Required]
        public float Quantity { get; set; }
        public float Amount { get; set; }
        public float Price { get; set; }
        public string Comment { get; set; } = "None";
        public MilkCollectStatus Status { get; set; } = MilkCollectStatus.COLLECTED;



    }
}
