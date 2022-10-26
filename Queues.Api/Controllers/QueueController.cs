using Microsoft.AspNetCore.Mvc;
using Queues.Application.Generic.Models;
using Queues.Application.Queue.DTOs;
using Queues.Application.Queue.Handlers;
using System.Net;

namespace Queues.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueueController : ControllerBase
{
    private readonly IQueueHandler _queueHandler;

    public QueueController(IQueueHandler queueHandler)
    {
        _queueHandler = queueHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllQueues()
    {
        var response = new ResponseModel<List<QueueDetailDto>>();

        try
        {
            var queue = await _queueHandler.GetAll();
            response.SetData(queue);

            if (!response.HasData())
                return NotFound();

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.SetErrorMessage(ex);
            return BadRequest(response);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetQueueById([FromRoute] int id)
    {
        var response = new ResponseModel<QueueDetailDto>();

        try
        {
            var queue = await _queueHandler.GetById(id);
            response.SetData(queue);

            if (!response.HasData())
                return NotFound();

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.SetErrorMessage(ex);
            return BadRequest(response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddQueue([FromBody] QueueAddDto queueDto)
    {
        var response = new ResponseModel<QueueDetailDto>();

        try
        {
            var queue = await _queueHandler.Create(queueDto);
            response.SetData(queue);

            if (!response.HasData())
                return NotFound();

            return StatusCode((int)HttpStatusCode.Created, response);
        }
        catch (Exception ex)
        {
            response.SetErrorMessage(ex);
            return BadRequest(response);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQueue([FromBody] QueueUpdateDto queueDto)
    {
        var response = new ResponseModel<QueueDetailDto>();

        try
        {
            var queue = await _queueHandler.Update(queueDto);
            response.SetData(queue);

            if (!response.HasData())
                return NotFound();

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.SetErrorMessage(ex);
            return BadRequest(response);
        }
    }
}
