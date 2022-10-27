using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Queues.Application.Interfaces;
using Queues.Application.Person.Models;
using Queues.Domain.Configurations;

namespace Queues.Infrastructure.Services;
public class DocumentRecognizerService : IDocumentRecognizerService
{
    private readonly FormRecognizerConfiguration _formRecognizerConfiguration;
    private readonly AzureKeyCredential _azureKeyCredential;
    public DocumentRecognizerService(
        IOptions<FormRecognizerConfiguration> formRecognizerConfiguration)
    {
        _formRecognizerConfiguration = formRecognizerConfiguration.Value;
        _azureKeyCredential = new AzureKeyCredential(_formRecognizerConfiguration.ApiKey);
    }

    public async Task<PersonDocumentModel> GetRecognizedDocument(IFormFile documentFile)
    {
        using var stream = documentFile.OpenReadStream();

        var client = new DocumentAnalysisClient(new Uri(_formRecognizerConfiguration.Endpoint), _azureKeyCredential);

        var operation = await client.AnalyzeDocumentAsync(
            WaitUntil.Completed,
            _formRecognizerConfiguration.ModelId,
            stream);

        var operationResult = operation.Value;
        var fields = operationResult?.Documents?.FirstOrDefault()?.Fields;

        var personDocument = new PersonDocumentModel();

        if (fields == default)
            return personDocument;

        personDocument.Name = fields.GetValueOrDefault("Name")?.Value?.AsString();
        personDocument.LastName = fields.GetValueOrDefault("LastName")?.Value?.AsString();
        personDocument.IdentificationNumber = fields.GetValueOrDefault("IDNumber")?.Value?.AsString().Replace("-", string.Empty);
        personDocument.Gender = char.Parse(fields.GetValueOrDefault("Gender")?.Value.AsString());

        return personDocument;
    }
}
