using LocadoraDVD.Application.Dtos;
using LocadoraDVD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDVD.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DvdController : ControllerBase
{
    private readonly IDvdService _service;
    public DvdController(IDvdService service)
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
    public async Task<IActionResult> Post(DvdDto model)
    {
        var result = await _service.Create(model);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> Put(DvdDto model)
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
