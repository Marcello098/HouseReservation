﻿using System.ComponentModel.DataAnnotations;

namespace HouseReservationWebAPI.Models.DTO;

public class HouseDto
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
    public int Occupancy { get; set; }
    public double Area { get; set; }
    public string DetailedInfo { get; set; }
    [Required]
    public double ChargeRate { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}