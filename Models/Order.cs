using System.ComponentModel.DataAnnotations;

namespace AuthProduct.Models;

public class Order
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } 
    [Required]
    public string Address { get; set; } 
    [Required]
    public string ContactPhone { get; set; }
    public DateTime? DateOfCreating { get; set; }
    
    public int PhoneId { get; set; } 
    public Phone? Phone { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}