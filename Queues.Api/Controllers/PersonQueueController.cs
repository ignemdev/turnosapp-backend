using Microsoft.AspNetCore.Mvc;
using Queues.Application.Generic.Models;
using Queues.Application.PersonQueue.DTOs;
using Queues.Application.PersonQueue.Handlers;

namespace Queues.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonQueueController : ControllerBase
{
    private readonly IPersonQueueHandler _personQueueHandler;

    public PersonQueueController(IPersonQueueHandler personQueueHandler)
    {
        _personQueueHandler = personQueueHandler;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPersonQueueById([FromRoute] int id)
    {
        var response = new ResponseModel<PersonQueueDetailDto>();

        try
        {
            var personQueue = await _personQueueHandler.GetById(id);
            response.SetData(personQueue);

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

    [HttpGet]
    public async Task<IActionResult> GetAllPeopleQueues()
    {
        var response = new ResponseModel<List<PersonQueueDetailDto>>();

        try
        {
            var allPeopleQueues = await _personQueueHandler.GetAll();
            response.SetData(allPeopleQueues);

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
