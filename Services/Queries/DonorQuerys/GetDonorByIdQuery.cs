using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.DonorQuerys;

public class GetDonorByIdQuery : IRequest<DonorDTO?>
    {
        public int DonorId { get; set; }

        public class GetDonorByIdQueryHandler : IRequestHandler<GetDonorByIdQuery, DonorDTO?>
        {
            private readonly IGestionVoluntariadoDonacionesContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<GetDonorByIdQueryHandler> _logger;

            public GetDonorByIdQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetDonorByIdQueryHandler> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<DonorDTO?> Handle(GetDonorByIdQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handling GetDonorByIdQuery for DonorId: {DonorId}", request.DonorId);

                var donor = await _context.Donors
                    .Include(d => d.User)
                    .FirstOrDefaultAsync(d => d.DonorId == request.DonorId, cancellationToken);

                if (donor == null)
                {
                    _logger.LogWarning("Donor not found for DonorId: {DonorId}", request.DonorId);
                    return null;
                }

                _logger.LogInformation("Donor found: {DonorId}", donor.DonorId);
                return _mapper.Map<DonorDTO>(donor);
            }
        }
    }

