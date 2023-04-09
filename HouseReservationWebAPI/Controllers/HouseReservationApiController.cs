using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseReservationWebAPI.Data;
using HouseReservationWebAPI.Models;
using HouseReservationWebAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace HouseReservationWebAPI.Controllers;

[Route("api/v1/HouseReservationAPI")]
[ApiController]
public class HouseReservationApiController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<HouseReservationApiController> _logger;

    public HouseReservationApiController(ILogger<HouseReservationApiController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<HouseDTO>> GetHouses()
    {
        return Ok(_dbContext.Houses.ToList());
    }




    [HttpGet("{id:int}", Name = "GetHouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<HouseDTO> GetHouse(int id)
    {
        var house = _dbContext.Houses.FirstOrDefault(u => u.Id == id);
        if (id == 0)
        {
            _logger.LogError("Getting house with id: {Id} failed", id);
            return BadRequest();
        }

        if (house == null)
        {
            _logger.LogError("Getting house with id: {Id} failed - Not Found", id);
            return NotFound();
        }

        ;
        return Ok(house);
    }




    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<HouseDTO> CreateHouse([FromBody] HouseDTO houseDto)
    {
        if (_dbContext.Houses.FirstOrDefault(u => u.Name.ToLower() == houseDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("AlreadyExistsError", "House with that name already exists!");
            return BadRequest(ModelState);
        }

        if (houseDto == null)
        {
            return BadRequest(houseDto);
        }

        if (houseDto.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        House modelHouse = new House()
        {
            Id = houseDto.Id,
            Amenity = houseDto.Amenity,
            DetailedInfo = houseDto.DetailedInfo,
            Name = houseDto.Name,
            Occupancy = houseDto.Occupancy,
            ChargeRate = houseDto.ChargeRate,
            Area = houseDto.Area,
            ImageUrl = houseDto.ImageUrl,
        };

        _dbContext.Houses.Add(modelHouse);
        _dbContext.SaveChanges();

        return CreatedAtRoute("GetHouse", new { id = houseDto.Id }, houseDto);
    }




    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteHouse")]
    public IActionResult DeleteHouse(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var house = _dbContext.Houses.FirstOrDefault(u => u.Id == id);
        if (house == null)
        {
            return NotFound();
        }

        _dbContext.Houses.Remove(house);
        _dbContext.SaveChanges();
        return NoContent();
    }




    [HttpPut("{id:int}", Name = "UpdateHouse")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateHouse(int id, [FromBody] HouseDTO houseDto)
    {
        if (houseDto == null || id != houseDto.Id)
        {
            return BadRequest();
        }

        House modelHouse = new House()
        {
            Id = houseDto.Id,
            Amenity = houseDto.Amenity,
            DetailedInfo = houseDto.DetailedInfo,
            Name = houseDto.Name,
            Occupancy = houseDto.Occupancy,
            ChargeRate = houseDto.ChargeRate,
            Area = houseDto.Area,
            ImageUrl = houseDto.ImageUrl,
        };
        _dbContext.Update(modelHouse);
        return NoContent();
    }




    [HttpPatch("{id:int}", Name = "UpdatePartialHouse")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePartialHouse(int id, JsonPatchDocument<HouseDTO> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }

        var house = _dbContext.Houses.FirstOrDefault(u => u.Id == id);

        var modelHouseDto = new HouseDTO()
        {
            Id = house.Id,
            Amenity = house.Amenity,
            DetailedInfo = house.DetailedInfo,
            Name = house.Name,
            Occupancy = house.Occupancy,
            ChargeRate = house.ChargeRate,
            Area = house.Area,
            ImageUrl = house.ImageUrl,
        };

        if (house == null)
        {
            return BadRequest();
        }
        patchDto.ApplyTo(modelHouseDto, ModelState);

        House modelHouse = new House()
        {
            Id = modelHouseDto.Id,
            Amenity = modelHouseDto.Amenity,
            DetailedInfo = modelHouseDto.DetailedInfo,
            Name = modelHouseDto.Name,
            Occupancy = modelHouseDto.Occupancy,
            ChargeRate = modelHouseDto.ChargeRate,
            Area = modelHouseDto.Area,
            ImageUrl = modelHouseDto.ImageUrl,
        };
        _dbContext.Houses.Update(modelHouse);
        _dbContext.SaveChanges();

        return NoContent();
    }
    
    
    

}

