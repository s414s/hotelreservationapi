using Domain.Entities;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class NewHotelDTO
{
    [Required(ErrorMessage = "Hotel name is required.")]
    [StringLength(20, ErrorMessage = "Hotel name cannot be longer than 20 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(20, ErrorMessage = "Address cannot be longer than 20 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required.")]
    [EnumDataType(typeof(Cities), ErrorMessage = "Invalid city value.")]
    public Cities City { get; set; }

    public Hotel MapToDomainEntity() => new(Name, Address, City);
}
