using HouseReservationWebAPI.Models.DTO;

namespace HouseReservationWebAPI.Data;

public static class HouseStore
{
    public static List<HouseDto> HouseList = new List<HouseDto>
    {
        new HouseDto { Id = 1, Name = "Pool View", Area = 100, Occupancy = 4},
        new HouseDto { Id = 2, Name = "Beach View", Area = 300, Occupancy = 3}
    };
}