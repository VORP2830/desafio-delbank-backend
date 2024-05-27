using LocadoraDVD.Application.Dtos;
using LocadoraDVD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDVD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirectorController : ControllerBase
{
    private readonly IDirectorService _service;
    public DirectorController(IDirectorService service)
    {
        _service = service;
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAll();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetById(id);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Post(DirectorDto model)
    {
        var result = await _service.Create(model);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> Put(DirectorDto model)
    {
        var result = await _service.Update(model);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);
        return Ok(result);
    }
}
