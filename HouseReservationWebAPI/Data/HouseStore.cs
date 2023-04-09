using HouseReservationWebAPI.Models.DTO;

namespace HouseReservationWebAPI.Data;

public static class HouseStore
{
    public static List<HouseDTO> HouseList = new List<HouseDTO>
    {
        new HouseDTO { Id = 1, Name = "Pool View", Area = 100, Occupancy = 4},
        new HouseDTO { Id = 2, Name = "Beach View", Area = 300, Occupancy = 3}
    };
}