using AutoMapper;
using DONACIONES_VOLUNTARIAS.API.DTOs;
using DONACIONES_VOLUNTARIAS.API.Entities;

namespace DONACIONES_VOLUNTARIAS.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Donation, DonationDTO>().ReverseMap();
            CreateMap<Donor, DonorDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<EventParticipation, EventParticipationDTO>().ReverseMap();
            CreateMap<Impact, ImpactDTO>().ReverseMap();
            CreateMap<Organization, OrganizationDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Volunteer, VolunteerDTO>().ReverseMap();
        }
    }
}
