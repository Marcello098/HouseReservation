using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using HouseReservationWebAPI.Data;
using HouseReservationWebAPI.Models;
using HouseReservationWebAPI.Models.DTO;
using HouseReservationWebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseReservationWebAPI.Controllers;

[Route("api/v1/HouseReservationAPI")]
[ApiController]
public class HouseReservationApiController : ControllerBase
{
    private readonly ILogger<HouseReservationApiController> _logger;
    private readonly IMapper _mapper;
    private readonly IHouseRepository _houseRepository;
    protected APIResponse apiResponse;

    public HouseReservationApiController(ILogger<HouseReservationApiController> logger,
        IHouseRepository houseRepository, IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _houseRepository = houseRepository;
        apiResponse = new APIResponse();
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task <ActionResult<APIResponse>> GetHousesAsync()
    {
        IEnumerable<House> houses = await _houseRepository.GetAllAsync();
        apiResponse.Result = _mapper.Map<List<HouseDto>>(houses);
        apiResponse.StatusCode = HttpStatusCode.OK;
        return Ok(apiResponse);
    }




    [HttpGet("{id:int}", Name = "GetHouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task <ActionResult<APIResponse>> GetHouseAsync(int id)
    {
        var house = await _houseRepository.GetAsync(u => u.Id == id);
        if (id == 0)
        {
            _logger.LogError("Getting house with id: {Id} failed", id);
            return BadRequest();
        }

        if (house == null)
        {
            _logger.LogError("Getting house with id: {Id} failed", id);
            return NotFound();
        }
        apiResponse.Result = _mapper.Map<HouseDto>(house);
        apiResponse.StatusCode = HttpStatusCode.OK;
        return Ok(apiResponse);
    }




    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreateHouseAsync([FromBody] HouseCreateDto houseCreateDto)
    {
        if (await _houseRepository.GetAsync(u => u.Name.ToLower() == houseCreateDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("AlreadyExistsError", "House with that name already exists!");
            return BadRequest(ModelState);
        }

        if (houseCreateDto == null)
        {
            return BadRequest(houseCreateDto);
        }
        House house = _mapper.Map<House>(houseCreateDto);
        await _houseRepository.CreateAsync(house);
        apiResponse.Result = _mapper.Map<HouseDto>(house);
        apiResponse.StatusCode = HttpStatusCode.Created;

        return CreatedAtRoute("GetHouse", new { id = house.Id }, apiResponse);
    }




    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteHouse")]
    public async Task <ActionResult<APIResponse>> DeleteHouse(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var house = await _houseRepository.GetAsync(u => u.Id == id);
        if (house == null)
        {
            return NotFound();
        }
        await _houseRepository.DeleteAsync(house);
        apiResponse.StatusCode = HttpStatusCode.NoContent;
        apiResponse.IsSuccess = true;
        return Ok(apiResponse);
    }




    [HttpPut("{id:int}", Name = "UpdateHouse")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateHouse(int id, [FromBody] HouseUpdateDto houseUpdateDto)
    {
        if (houseUpdateDto == null || id != houseUpdateDto.Id)
        {
            return BadRequest();
        }
        House house = _mapper.Map<House>(houseUpdateDto);

        await _houseRepository.UpdateAsync(house);
        apiResponse.StatusCode = HttpStatusCode.NoContent;
        apiResponse.IsSuccess = true;
        return Ok(apiResponse);
    }




    [HttpPatch("{id:int}", Name = "UpdatePartialHouse")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdatePartialHouse(int id, JsonPatchDocument<HouseUpdateDto> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }

        var house = await _houseRepository.GetAsync(u => u.Id == id, isTracked:false);
        var houseDto = _mapper.Map<HouseUpdateDto>(house);

        if (house == null)
        {
            return BadRequest();
        }
        patchDto.ApplyTo(houseDto, ModelState);

        var modelHouse = _mapper.Map<House>(houseDto);

        await _houseRepository.UpdateAsync(modelHouse);
        apiResponse.StatusCode = HttpStatusCode.NoContent;
        apiResponse.IsSuccess = true;
        return Ok(apiResponse);
    }
    
    
    

}

