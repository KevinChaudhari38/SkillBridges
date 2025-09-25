using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class TaskCreateViewModel
    {
        [Required(ErrorMessage ="Title Is Required")]
        public string Title {  get; set; }

        [Required(ErrorMessage ="Budjet is required")]
        [Range(0,double.MaxValue,ErrorMessage ="Budjet Should be positive")]
        public decimal Budjet {  get; set; }
        [Required]
        public string Description { get; set; }
        
        public DateTime? Deadline { get; set; }
        public string ClientProfileId {  get; set; }
        public string CategoryId {  get; set; }
        
        public IEnumerable<SelectListItem>? Categories { get; set; }

    }
}
