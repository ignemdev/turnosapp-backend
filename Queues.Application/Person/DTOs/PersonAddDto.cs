using Microsoft.AspNetCore.Http;

namespace Queues.Application.Person.DTOs;
public class PersonAddDto
{
    public IFormFile DocumentFile { get; set; } = null!;
    public bool Pregnant { get; set; }
    public bool HealthIssues { get; set; }
    public DateTime Birthdate { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
}
