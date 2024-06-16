using Domain.Entities;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class NewRoomDTO
{
    [Required(ErrorMessage = "Hotel name is required.")]
    [Range(1, 30, ErrorMessage = "Storey must be between 1 and 30.")]
    public int Storey { get; set; }

    [Required(ErrorMessage = "Room Type is required.")]
    [EnumDataType(typeof(RoomTypes), ErrorMessage = "Invalid room type value.")]
    public RoomTypes Type { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(1, 500, ErrorMessage = "Price must be between 1 and 500.")]
    public decimal Price { get; set; }

    public Room MapToDomainEntity() => new(Storey, Price, Type);
}
