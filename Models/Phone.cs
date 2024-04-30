using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthProduct.Models;

public class Phone
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Поле 'Наименование' обязательно для заполнения")]
    [Remote(action: "CheckPhoneName", controller: "Validation", ErrorMessage = "Телефон с этим названием уже существует")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Поле 'Компания' обязательно для заполнения")]
    public string Company { get; set; }
    [Range(100, 2000)]
    [Required]
    public int Price { get; set; }
    public string? Description { get; set; }
    public DateTime? DateOfEditing { get; set; }
    [BindNever]
    public ICollection<PhoneReview>? Reviews { get; set; }
    [Required(ErrorMessage = "Пожалуйста, выберите бренд")]
    public int? BrandId { get; set; }
    public Brand? Brand { get; set; }
}