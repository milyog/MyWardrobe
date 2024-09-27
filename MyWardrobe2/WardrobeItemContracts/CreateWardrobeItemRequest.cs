using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWardrobe.Models;

namespace MyWardrobe.Contracts.WardrobeItem
{
    public record CreateWardrobeItemRequest(
        string Category,
        string Subcategory,
        string Brand, 
        string Model, 
        decimal Price,  
        string? Material, 
        string? Color, 
        string? Size, 
        string? Description,
        List<WardrobeItemUsage>? WardrobeItemUsages
        );
        
}
