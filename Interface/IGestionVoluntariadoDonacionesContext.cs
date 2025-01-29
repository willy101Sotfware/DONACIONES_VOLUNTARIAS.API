using DONACIONES_VOLUNTARIAS.API.Entities;
using Microsoft.EntityFrameworkCore;



namespace DONACIONES_VOLUNTARIAS.API.Interface;

public interface IGestionVoluntariadoDonacionesContext
{
    DbSet<Donation> Donations { get; set; }
    DbSet<Donor> Donors { get; set; }
    DbSet<Event> Events { get; set; }
    DbSet<EventParticipation> EventParticipations { get; set; }
    DbSet<Impact> Impacts { get; set; }
    DbSet<Organization> Organizations { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Volunteer> Volunteers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}