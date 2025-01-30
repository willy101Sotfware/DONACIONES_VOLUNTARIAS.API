using MediatR;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DONACIONES_VOLUNTARIAS.API.Services.Queries.DonorQuerys;

    public class GetAllDonorsQuery : IRequest<List<DonorDTO>>
    {
        public class GetAllDonorsQueryHandler : IRequestHandler<GetAllDonorsQuery, List<DonorDTO>>
        {
            private readonly IGestionVoluntariadoDonacionesContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<GetAllDonorsQueryHandler> _logger;

            public GetAllDonorsQueryHandler(IGestionVoluntariadoDonacionesContext context, IMapper mapper, ILogger<GetAllDonorsQueryHandler> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<List<DonorDTO>> Handle(GetAllDonorsQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Handling GetAllDonorsQuery");

                var donors = await _context.Donors
                    .Include(d => d.User)
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("Retrieved {Count} donors", donors.Count);

                return _mapper.Map<List<DonorDTO>>(donors);
            }
        }
    }

