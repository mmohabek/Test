using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";


        public virtual ICollection<CategoryProduct>? CategoryProducts { get; set; }

    }
}
