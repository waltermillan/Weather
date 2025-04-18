using API.DTOs;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HistoricalQueriesController : BaseApiController
{
    IUnitOfWork _unitOfWork;
    IMapper _mapper;
    HistoricalQueryDTOService _historicalQueryDTOService;

    public HistoricalQueriesController(IUnitOfWork unitOfWork, IMapper mapper, HistoricalQueryDTOService historicalQueryDTOService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _historicalQueryDTOService = historicalQueryDTOService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<HistoricalQuery>>> GetAll()
    {
        var historicalQueries = await _unitOfWork.HistoricalQueries.GetAllAsync();
        return _mapper.Map<List<HistoricalQuery>>(historicalQueries);
    }

    [HttpGet("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<HistoricalQueryDTO>>> GetAllWithUserName()
    {
        var historicalQueries = await _historicalQueryDTOService.GetHistoricalQueryDTOAsync();

        if (historicalQueries is null)
            return NotFound();

        return Ok(historicalQueries);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HistoricalQuery>> Get(int id)
    {
        var historicalQuery = await _unitOfWork.HistoricalQueries.GetByIdAsync(id);

        if (historicalQuery is null)
            return NotFound();

        return _mapper.Map<HistoricalQuery>(historicalQuery);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<HistoricalQuery>>> Post([FromBody] HistoricalQuery oHistoricalQuery)
    {
        try
        {
            var historicalQuery = _mapper.Map<HistoricalQuery>(oHistoricalQuery);
            _unitOfWork.HistoricalQueries.Add(historicalQuery);
            await _unitOfWork.SaveAsync();

            if (historicalQuery is null)
                return BadRequest();

            oHistoricalQuery.Id = historicalQuery.Id;

            return CreatedAtAction(nameof(Post), new { id = oHistoricalQuery.Id }, oHistoricalQuery);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HistoricalQuery>> Put(int id, [FromBody] HistoricalQuery oHistoricalQuery)
    {
        try
        {
            if (oHistoricalQuery is null)
                return NotFound();

            var historicalQuery = _mapper.Map<HistoricalQuery>(oHistoricalQuery);
            _unitOfWork.HistoricalQueries.Update(historicalQuery);
            await _unitOfWork.SaveAsync();

            return oHistoricalQuery;
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
            var historicalQuery = await _unitOfWork.HistoricalQueries.GetByIdAsync(id);

            if (historicalQuery is null)
                return NotFound();

            _unitOfWork.HistoricalQueries.Remove(historicalQuery);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
