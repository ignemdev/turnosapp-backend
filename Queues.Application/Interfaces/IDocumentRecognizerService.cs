using Microsoft.AspNetCore.Http;
using Queues.Application.Person.Models;

namespace Queues.Application.Interfaces
{
    public interface IDocumentRecognizerService
    {
        Task<PersonDocumentModel> GetRecognizedDocument(IFormFile documentFile);
    }
}
