using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.DonationQuerys;

public class GetAllDonationsQuery : IRequest<List<DonationDTO>>
{
    public class GetAllDonationsQueryHandler : IRequestHandler<GetAllDonationsQuery, List<DonationDTO>>
    {
        private readonly IGestionVoluntariadoDonacionesContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllDonationsQueryHandler> _logger;

        public GetAllDonationsQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetAllDonationsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<DonationDTO>> Handle(GetAllDonationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllDonationsQuery");

            var donations = await _context.Donations
                .Include(d => d.Donor)
                .Include(d => d.Organization)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} donations", donations.Count);

            return _mapper.Map<List<DonationDTO>>(donations);
        }
    }
}
