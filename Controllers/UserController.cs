using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userServices)
    {
        userService = userServices;
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var users = await userService.Get();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener usuarios: {ex.Message}");
        }
    }

    // POST JSON: api/User
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        try
        {
            await userService.Save(user);
            return Ok(new { message = "Usuario creado correctamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al crear usuario: {ex.Message}");
        }
    }

    // POST MULTIPART: api/User/upload
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("upload")]
    public async Task<IActionResult> PostWithImage([FromForm] User user, [FromForm] IFormFile? imageData)
    {
        try
        {
            if (imageData != null && imageData.Length > 0)
            {
                using var ms = new MemoryStream();
                await imageData.CopyToAsync(ms);
                user.ImageData = ms.ToArray();
            }

            await userService.Save(user);
            return Ok(new { message = "Usuario creado con imagen correctamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al crear usuario con imagen: {ex.Message}");
        }
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] User user)
    {
        try
        {
            await userService.Update(id, user);
            return Ok(new { message = "Usuario actualizado correctamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al actualizar usuario: {ex.Message}");
        }
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await userService.Delete(id);
            return Ok(new { message = "Usuario eliminado correctamente" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al eliminar usuario: {ex.Message}");
        }
    }
}
