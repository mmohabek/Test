namespace Test.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";


        public virtual ICollection<CategoryProduct>? CategoryProducts { get; set; }
    }
}
