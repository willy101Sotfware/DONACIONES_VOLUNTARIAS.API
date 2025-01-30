using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.VolunteerQuerys;

public class GetVolunteerByIdQuery : IRequest<VolunteerDTO?>
{
    public int VolunteerId { get; set; }

    public class GetVolunteerByIdQueryHandler : IRequestHandler<GetVolunteerByIdQuery, VolunteerDTO?>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetVolunteerByIdQueryHandler> _logger;

        public GetVolunteerByIdQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetVolunteerByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<VolunteerDTO?> Handle(GetVolunteerByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetVolunteerByIdQuery for VolunteerId: {VolunteerId}", request.VolunteerId);

            var volunteer = await _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.VolunteerId == request.VolunteerId, cancellationToken);

            if (volunteer == null)
            {
                _logger.LogWarning("Volunteer not found for VolunteerId: {VolunteerId}", request.VolunteerId);
                return null;
            }

            _logger.LogInformation("Volunteer found: {VolunteerId}", volunteer.VolunteerId);
            return _mapper.Map<VolunteerDTO>(volunteer);
        }
    }
}
