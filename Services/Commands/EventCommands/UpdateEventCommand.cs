using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;

namespace DONACIONES_VOLUNTARIAS.API.Services.Commands.EventCommands;

    public class UpdateEventCommand : IRequest<EventDTO>
    {
        public int EventId { get; set; }
        public int? OrganizationId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? EventDate { get; set; }
        public string? Location { get; set; }

        public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventDTO>
        {
            private readonly IGestionVoluntariadoDonacionesContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<UpdateEventCommandHandler> _logger;

            public UpdateEventCommandHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<UpdateEventCommandHandler> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<EventDTO> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handling UpdateEventCommand for EventId: {EventId}", request.EventId);

                var eventEntity = await _context.Events.FindAsync(request.EventId, cancellationToken);

                if (eventEntity == null)
                {
                    _logger.LogWarning("Event not found for EventId: {EventId}", request.EventId);
                    throw new Exception("Event not found");
                }

                eventEntity.OrganizationId = request.OrganizationId;
                eventEntity.Title = request.Title;
                eventEntity.Description = request.Description;
                eventEntity.EventDate = request.EventDate;
                eventEntity.Location = request.Location;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Event updated with ID: {EventId}", eventEntity.EventId);

                return _mapper.Map<EventDTO>(eventEntity);
            }
        }
    }

