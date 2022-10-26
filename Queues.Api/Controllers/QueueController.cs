using Microsoft.AspNetCore.Mvc;
using Queues.Application.Generic.Models;
using Queues.Application.PersonQueue.DTOs;
using Queues.Application.PersonQueue.Handlers;
using Queues.Application.Queue.DTOs;
using Queues.Application.Queue.Handlers;
using System.Net;

namespace Queues.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueueController : ControllerBase
{
    private readonly IQueueHandler _queueHandler;
    private readonly IPersonQueueHandler _personQueueHandler;

    public QueueController(IQueueHandler queueHandler, IPersonQueueHandler personQueueHandler)
    {
        _queueHandler = queueHandler;
        _personQueueHandler = personQueueHandler;
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

    [HttpPost("{id:int}/add")]
    public async Task<IActionResult> AddPersonToQueue([FromRoute] int id, [FromBody] PersonQueueAddDto personQueueAddDto)
    {
        var response = new ResponseModel<PersonQueueDetailDto>();

        try
        {
            var createdPersonQueue = await _personQueueHandler.Create(id, personQueueAddDto);
            response.SetData(createdPersonQueue);

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

    [HttpGet("{id:int}/people")]
    public async Task<IActionResult> GetAllPeopleQueuesInQueue([FromRoute] int id)
    {
        var response = new ResponseModel<List<PersonQueueDetailDto>>();

        try
        {
            var allPeopleQueuesInQueue = await _personQueueHandler.GetAllPeopleQueueByQueueId(id);
            response.SetData(allPeopleQueuesInQueue);

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

    [HttpGet("{id:int}/next")]
    public async Task<IActionResult> SetActivePersonQueueInQueue([FromRoute] int id)
    {
        var response = new ResponseModel<PersonQueueDetailDto>();

        try
        {
            await _personQueueHandler.SetPersonQueueAsAttendedByQueueId(id);
            var activePersonQueue = await _personQueueHandler.SetActivePersonQueueByQueueId(id);
            response.SetData(activePersonQueue);

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

    [HttpGet("{id:int}/active")]
    public async Task<IActionResult> GetActivePersonQueueInQueue([FromRoute] int id)
    {
        var response = new ResponseModel<PersonQueueDetailDto>();

        try
        {
            var activePersonQueue = await _personQueueHandler.GetActivePersonQueueByQueueId(id);
            response.SetData(activePersonQueue);

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

    [HttpGet("{id:int}/count")]
    public async Task<IActionResult> GetPeopleQueuesCountInQueue([FromRoute] int id)
    {
        var response = new ResponseModel<int>();

        try
        {
            var peopleQueuesCount = await _personQueueHandler.GetPeopleQueuesCountByQueueId(id);
            response.SetData(peopleQueuesCount);

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
