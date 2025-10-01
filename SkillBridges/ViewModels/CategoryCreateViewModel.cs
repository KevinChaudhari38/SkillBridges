using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;
using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public TaskType type { get; set; }

        public IEnumerable<SelectListItem>? Types = new List<SelectListItem>();
    }
}
