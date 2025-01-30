using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DONACIONES_VOLUNTARIAS.API.Interface;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.DonationQuerys
{
    public class GetDonationByIdQuery : IRequest<DonationDTO?>
    {
        public int DonationId { get; set; }

        public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationByIdQuery, DonationDTO?>
        {
            private readonly IGestionVoluntariadoDonacionesContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<GetDonationByIdQueryHandler> _logger;

            public GetDonationByIdQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetDonationByIdQueryHandler> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<DonationDTO?> Handle(GetDonationByIdQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handling GetDonationByIdQuery for DonationId: {DonationId}", request.DonationId);

                var donation = await _context.Donations
                    .Include(d => d.Donor)
                    .Include(d => d.Organization)
                    .FirstOrDefaultAsync(d => d.DonationId == request.DonationId, cancellationToken);

                if (donation == null)
                {
                    _logger.LogWarning("Donation not found for DonationId: {DonationId}", request.DonationId);
                    return null;
                }

                _logger.LogInformation("Donation found: {DonationId}", donation.DonationId);
                return _mapper.Map<DonationDTO>(donation);
            }
        }
    }
}
