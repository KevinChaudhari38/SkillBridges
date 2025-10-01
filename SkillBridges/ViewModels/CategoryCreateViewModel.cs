using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;
using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Type is required")]
        public TaskType type { get; set; }

        public IEnumerable<SelectListItem>? Types = new List<SelectListItem>();
    }
}
