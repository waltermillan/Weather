using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CitiesController : BaseApiController
{
    IUnitOfWork _unitOfWork;
    IMapper _mapper;
    public CitiesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<City>>> Get()
    {
        var cities = await _unitOfWork.Cities.GetAllAsync();
        return _mapper.Map<List<City>>(cities);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<City>> Get(int id)
    {
        var city = await _unitOfWork.Cities.GetByIdAsync(id);

        if (city is null)
            return NotFound();

        return _mapper.Map<City>(city);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<City>>> Post([FromBody] City oCity)
    {
        try
        {
            var city = _mapper.Map<City>(oCity);
            _unitOfWork.Cities.Add(city);
            await _unitOfWork.SaveAsync();

            if (city is null)
                return BadRequest();

            oCity.Id = city.Id;

            return CreatedAtAction(nameof(Post), new { id = oCity.Id }, oCity);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<City>> Put(int id, [FromBody] City oCity)
    {
        try
        {
            if (oCity is null)
                return NotFound();

            var city = _mapper.Map<City>(oCity);
            _unitOfWork.Cities.Update(city);
            await _unitOfWork.SaveAsync();

            return oCity;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(id);

            if (city is null)
                return NotFound();

            _unitOfWork.Cities.Remove(city);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
