using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ApiKeysController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly Context _context;
    private readonly AesEncryptionService _encryptionService;
    
    public ApiKeysController(IUnitOfWork unitOfWork, IMapper mapper, Context context, AesEncryptionService encryptionService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
        _encryptionService = encryptionService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ApiKey>>> Get()
    {
        var keys = await _unitOfWork.ApiKeys.GetAllAsync();
        return _mapper.Map<List<ApiKey>>(keys);
    }

    [HttpPost("encrypt/passwords")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EncriptPasswords()
    {
        var keys = await _unitOfWork.ApiKeys.GetAllAsync();

        foreach (var apiKey in keys)
        {
            if (!string.IsNullOrWhiteSpace(apiKey.Key))
            {
                apiKey.Key = _encryptionService.Encrypt(apiKey.Key);
                _unitOfWork.ApiKeys.Update(apiKey);
            }
        }

        await _unitOfWork.SaveAsync();
        return Ok("API keys encrypted correctly.");
    }

    [HttpGet("provider/{provider}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> GetByProvider(string provider)
    {
        var apiKey = await _unitOfWork.ApiKeys.GetKeyByProviderAsync(provider);

        if (apiKey is null)
            return NotFound();

        return _mapper.Map<string>(apiKey);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiKey>> Get(int id)
    {
        var apiKey = await _unitOfWork.ApiKeys.GetByIdAsync(id);

        if (apiKey is null)
            return NotFound();

        return _mapper.Map<ApiKey>(apiKey);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ApiKey>>> Post([FromBody] ApiKey oApiKey)
    {
        try
        {
            var apiKey = _mapper.Map<ApiKey>(oApiKey);

            apiKey.Key = _encryptionService.Encrypt(apiKey.Key); // Encrypt the API key

            _unitOfWork.ApiKeys.Add(apiKey);
            await _unitOfWork.SaveAsync();

            if (apiKey is null)
                return BadRequest();

            oApiKey.Id = apiKey.Id;

            return CreatedAtAction(nameof(Post), new { id = oApiKey.Id }, oApiKey);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiKey>> Put([FromBody] ApiKey oApiKey)
    {
        try
        {
            if (oApiKey is null)
                return NotFound();

            var customer = _mapper.Map<ApiKey>(oApiKey);
            _unitOfWork.ApiKeys.Update(customer);
            await _unitOfWork.SaveAsync();

            return oApiKey;
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
            var apiKey = await _unitOfWork.ApiKeys.GetByIdAsync(id);

            if (apiKey is null)
                return NotFound();

            _unitOfWork.ApiKeys.Remove(apiKey);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
