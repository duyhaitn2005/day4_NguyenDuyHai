
using Day4.Models;

namespace Day4.Data
{
    public class SeedData
    {
        public static List<Category> categories = new List<Category>

        {
            new Category { Id = 1, Name = "cho" },
            new Category { Id = 2, Name = "meo" },
            new Category { Id = 3, Name = "ga" }

        };

        public static List<Product> products = new List<Product>();
    }
}
