using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Entities;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;

namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.EventCommands;

public class CreateEventCommand : IRequest<EventDTO>
{
    public int? OrganizationId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? EventDate { get; set; }
    public string? Location { get; set; }

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDTO>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEventCommandHandler> _logger;

        public CreateEventCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<CreateEventCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EventDTO> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateEventCommand");

            var eventEntity = new Event
            {
                OrganizationId = request.OrganizationId,
                Title = request.Title,
                Description = request.Description,
                EventDate = request.EventDate,
                Location = request.Location,
                CreatedAt = DateTime.UtcNow
            };

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Event created with ID: {EventId}", eventEntity.EventId);

            return _mapper.Map<EventDTO>(eventEntity);
        }
    }
}
