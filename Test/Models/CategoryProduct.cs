using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class CategoryProduct
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }


        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }


    
    }
}
