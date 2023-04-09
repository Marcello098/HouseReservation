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

    private readonly ILogger<HouseReservationApiController> _logger;
    public HouseReservationApiController(ILogger<HouseReservationApiController> logger)
    {
        _logger = logger;
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<HouseDTO>> GetHouses()
    {
        return Ok(HouseStore.HouseList);
    }


    [HttpGet("{id:int}", Name = "GetHouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<HouseDTO> GetHouse(int id)
    {
        var house = HouseStore.HouseList.FirstOrDefault(u => u.Id == id);
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
        if (HouseStore.HouseList.FirstOrDefault(u => u.Name.ToLower() == houseDto.Name.ToLower()) != null)
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

        houseDto.Id = HouseStore.HouseList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        HouseStore.HouseList.Add(houseDto);

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

        var house = HouseStore.HouseList.FirstOrDefault(u => u.Id == id);
        if (house == null)
        {
            return NotFound();
        }

        HouseStore.HouseList.Remove(house);
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

        var house = HouseStore.HouseList.FirstOrDefault(u => u.Id == id);
        house.Name = houseDto.Name;
        house.Area = houseDto.Area;
        house.Occupancy = houseDto.Occupancy;

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

        var house = HouseStore.HouseList.FirstOrDefault(u => u.Id == id);
        if (house == null)
        {
            return BadRequest();
        }
        patchDto.ApplyTo(house, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }

}

