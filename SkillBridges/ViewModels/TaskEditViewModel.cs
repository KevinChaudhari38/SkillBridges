using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class TaskEditViewModel
    {
        [Required]
        public string TaskId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Budjet { get; set; }
        public DateTime? Deadline { get; set; }
        [Required]
        public string CategoryId { get; set; }
        
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public string ClientProfileId { get; set; }

        
    }
}
