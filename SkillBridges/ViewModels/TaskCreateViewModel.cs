using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;
using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class TaskCreateViewModel:IValidatableObject
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
        public TaskType Type { get; set; }
        public IEnumerable<SelectListItem>? Types {  get; set; }
        public City? City { get; set; }
        public IEnumerable<SelectListItem>? Cities {  get; set; }
        public string CategoryId {  get; set; }
        
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type == TaskType.Local && !City.HasValue)
            {
                yield return new ValidationResult(
                    "City is required for local tasks",
                    new[] { nameof(City) }
                );
            }
        }
    }
}
