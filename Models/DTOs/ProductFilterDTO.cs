using Models.Entities;

namespace Models.DTOs;

public class ProductFilterDTO
{
    public int Total { get; set; }
    public IEnumerable<Product> Products { get; set; }
}