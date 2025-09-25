using System.ComponentModel.DataAnnotations;

namespace SkillBridges.ViewModels
{
    public class CategoryViewModel
    {
        public string CategoryId {  get; set; }
        public string Name { get; set; }
        public List<Task>? Tasks { get; set; }

    }
}
