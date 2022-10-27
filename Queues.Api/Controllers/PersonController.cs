using Microsoft.AspNetCore.Mvc;
using Queues.Application.Generic.Models;
using Queues.Application.Interfaces;
using Queues.Application.Person.DTOs;
using Queues.Application.Person.Handlers;

namespace Queues.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonHandler _personHandler;
    private readonly IDocumentRecognizerService _documentRecognizerService;

    public PersonController(
        IPersonHandler personHandler,
        IDocumentRecognizerService documentRecognizerService)
    {
        _personHandler = personHandler;
        _documentRecognizerService = documentRecognizerService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPersonById([FromRoute] int id)
    {
        var response = new ResponseModel<PersonDetailDto>();

        try
        {
            var person = await _personHandler.GetById(id);
            response.SetData(person);

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
    public async Task<IActionResult> AddPerson([FromForm] PersonAddDto personAddDto)
    {
        var response = new ResponseModel<PersonDetailDto>();

        try
        {
            var personDocument = await _documentRecognizerService.GetRecognizedDocument(personAddDto.DocumentFile);
            var person = await _personHandler.Create(personDocument, personAddDto);
            response.SetData(person);

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
