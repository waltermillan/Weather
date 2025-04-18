using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    IUnitOfWork _unitOfWork;
    IMapper _mapper;
    IPasswordHasher _passwordHasher;
    Context _context;
    public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, Context context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _context = context;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] LoginRequest request)
    {
        var usr = request.Usr.ToUpper();
        var psw = request.Psw;

        try
        {
            var user = await _unitOfWork.Users.GetByUserNameAsync(usr);

            if (user is null || !_passwordHasher.VerifyPassword(psw, user.Password))
            {
                Log.Logger.Information($"Login attempt failed for user: {usr}");
                return Unauthorized(new { Code = 401, Message = "Invalid username or password" });
            }

            Log.Logger.Information($"User '{usr}' authenticated successfully.");

            return Ok(new
            {
                Code = 0,
                Message = "User authenticated successfully",
                Id = user.Id,
                UserName = user.UserName
            });
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Authentication error", ex);
            return StatusCode(500, new { StatusCode = 500, Message = "There was an issue authenticating the user.", Details = ex.Message });
        }
    }

    [HttpPost("encrypt/passwords")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RebuildPasswords()
    {
        var users = await _context.Users.ToListAsync();

        foreach (var user in users)
        {
            if (!user.Password.StartsWith("$2"))
            {
                Console.WriteLine($"User: {user.UserName} | Old: {user.Password}");
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
        }

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Passwords actualizadas correctamente." });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return StatusCode(500, new { sMessage = "Error guardando cambios", ex.Message });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return _mapper.Map<List<User>>(users);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Get(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return _mapper.Map<User>(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<User>>> Post([FromBody] User oUser)
    {
        try
        {
            var user = _mapper.Map<User>(oUser);
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveAsync();

            if (user is null)
                return BadRequest();

            oUser.Id = user.Id;

            return CreatedAtAction(nameof(Post), new { id = oUser.Id }, oUser);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<User>> Put(int id, [FromBody] User oUser)
    {
        try
        {
            if (oUser is null)
                return NotFound();

            var customer = _mapper.Map<User>(oUser);
            _unitOfWork.Users.Update(customer);
            await _unitOfWork.SaveAsync();

            return oUser;
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
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
