using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class BaseClass
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
