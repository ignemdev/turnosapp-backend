using Microsoft.Extensions.Options;
using Queues.Application.Interfaces;
using Queues.Domain.Configurations;

namespace Queues.Infrastructure.Services;
public class DocumentRecognizerService : IDocumentRecognizerService
{
    private readonly IOptions<FormRecognizerConfiguration> _formRecognizerConfiguration;
    public DocumentRecognizerService(IOptions<FormRecognizerConfiguration> formRecognizerConfiguration)
    {
        _formRecognizerConfiguration = formRecognizerConfiguration;
    }
}
