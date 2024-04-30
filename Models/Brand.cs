using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuthProduct.Models;

public class Brand
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле 'Наименование' обязательно для заполнения")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Поле 'Дата основания' обязательно для заполнения")]
    [Remote(action: "CheckDate", controller: "Validation", ErrorMessage = "Дату из будущего или ранее чем 100 лет. ")]
    public DateTime FoundationDate { get; set; }
}