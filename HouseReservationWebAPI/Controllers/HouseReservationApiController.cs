using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HouseReservationWebAPI.Data;
using HouseReservationWebAPI.Models;
using HouseReservationWebAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseReservationWebAPI.Controllers;

[Route("api/v1/HouseReservationAPI")]
[ApiController]
public class HouseReservationApiController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<HouseReservationApiController> _logger;
    private readonly IMapper _mapper;

    public HouseReservationApiController(ILogger<HouseReservationApiController> logger, 
        ApplicationDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _dbContext = dbContext;
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task <ActionResult<IEnumerable<HouseDto>>> GetHousesAsync()
    {
        IEnumerable<House> houses = await _dbContext.Houses.ToListAsync();
        return Ok(_mapper.Map<List<HouseDto>>(houses));
    }




    [HttpGet("{id:int}", Name = "GetHouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task <ActionResult<HouseDto>> GetHouseAsync(int id)
    {
        var house = await _dbContext.Houses.FirstOrDefaultAsync(u => u.Id == id);
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
        return Ok(_mapper.Map<HouseDto>(house));
    }




    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<HouseDto>> CreateHouseAsync([FromBody] HouseCreateDto houseCreateDto)
    {
        if (await _dbContext.Houses.FirstOrDefaultAsync(u => u.Name.ToLower() == houseCreateDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("AlreadyExistsError", "House with that name already exists!");
            return BadRequest(ModelState);
        }

        if (houseCreateDto == null)
        {
            return BadRequest(houseCreateDto);
        }
        House modelHouse = _mapper.Map<House>(houseCreateDto);
        await _dbContext.Houses.AddAsync(modelHouse);
        await _dbContext.SaveChangesAsync();

        return CreatedAtRoute("GetHouse", new { id = modelHouse.Id }, modelHouse);
    }




    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteHouse")]
    public async Task <IActionResult> DeleteHouse(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var house = await _dbContext.Houses.FirstOrDefaultAsync(u => u.Id == id);
        if (house == null)
        {
            return NotFound();
        }

        _dbContext.Houses.Remove(house);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }




    [HttpPut("{id:int}", Name = "UpdateHouse")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateHouse(int id, [FromBody] HouseUpdateDto houseUpdateDto)
    {
        if (houseUpdateDto == null || id != houseUpdateDto.Id)
        {
            return BadRequest();
        }

        House modelHouse = _mapper.Map<House>(houseUpdateDto);

        _dbContext.Update(modelHouse);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }




    [HttpPatch("{id:int}", Name = "UpdatePartialHouse")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePartialHouse(int id, JsonPatchDocument<HouseUpdateDto> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }

        var house = await _dbContext.Houses.AsNoTracking().FirstOrDefaultAsync( u => u.Id == id);
        var modelHouseDto = _mapper.Map<HouseUpdateDto>(house);

        if (house == null)
        {
            return BadRequest();
        }
        patchDto.ApplyTo(modelHouseDto, ModelState);

        var modelHouse = _mapper.Map<House>(modelHouseDto);

        _dbContext.Houses.Update(modelHouse);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
    
    
    

}

