using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthProduct.Models;

public class PhoneReview
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Пожалуйста, укажите ваше имя")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Пожалуйста, оцените телефон от 0 до 5")]
    [Range(0, 5, ErrorMessage = "Оценка должна быть от 0 до 5")]
    public int Rating { get; set; }
    [Required]
    public string ReviewText { get; set; }

    [ForeignKey("Phone")]
    public int PhoneId { get; set; }
    public Phone? Phone { get; set; }
}