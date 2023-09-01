using Echipa_de_fotbal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echipa_de_fotbal.Operations
{
    public class TeamOperations
    {
        public void AddTeam(Echipa teamToAdd)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
            string? connectionString = configuration.GetConnectionString("MyConnectionString");
            var serviceProvider = new ServiceCollection()
                            .AddDbContext<EchipaDeFotbalContext>(options =>
                            options.UseNpgsql(connectionString))
                            .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EchipaDeFotbalContext>();

            dbContext.Teams.Add(teamToAdd);
            dbContext.SaveChanges();

        }
        public void UpdateTeam(int idToEdit, Echipa teamUpdate)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
            string? connectionString = configuration.GetConnectionString("MyConnectionString");
            var serviceProvider = new ServiceCollection()
                            .AddDbContext<EchipaDeFotbalContext>(options =>
                            options.UseNpgsql(connectionString))
                            .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EchipaDeFotbalContext>();
            var teamToUpdate = dbContext.Teams.FirstOrDefault(t => t.IdEchipa == idToEdit);
            if (teamToUpdate != null)
            {
                teamToUpdate.NumeEchipa = teamUpdate.NumeEchipa;
                teamToUpdate.NumePatron = teamUpdate.NumePatron;
                teamToUpdate.IdCampionat = teamUpdate.IdCampionat;
                teamToUpdate.IdStadion = teamUpdate.IdStadion;
                dbContext.SaveChanges();
            }
        }
        public void DeleteTeam(int idToDelete)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
            string? connectionString = configuration.GetConnectionString("MyConnectionString");
            var serviceProvider = new ServiceCollection()
                            .AddDbContext<EchipaDeFotbalContext>(options =>
                            options.UseNpgsql(connectionString))
                            .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EchipaDeFotbalContext>();

            var teamToDelete = dbContext.Teams.FirstOrDefault(t => t.IdEchipa == idToDelete);
            if (teamToDelete != null)
            {
                dbContext.Teams.Remove(teamToDelete);
                dbContext.SaveChanges();
            }
        }
        public void ReadTeams()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
            string? connectionString = configuration.GetConnectionString("MyConnectionString");
            var serviceProvider = new ServiceCollection()
                            .AddDbContext<EchipaDeFotbalContext>(options =>
                            options.UseNpgsql(connectionString))
                            .BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EchipaDeFotbalContext>();

            var teams = dbContext.Teams.ToList();
            Console.WriteLine("Teams:");
            foreach (var team in teams)
            {
                Console.WriteLine($"{team.IdEchipa}: {team.NumeEchipa}, Patron: {team.NumePatron}, Campionat: {dbContext.Championships.Where(c => c.IdCampionat == team.IdCampionat).Select(c => c.NumeCampionat).FirstOrDefault()}, Stadion: {dbContext.Stadiums.Where(s => s.IdStadion == team.IdStadion).Select(s => s.NumeStadion).FirstOrDefault()}");
            }
        }

    }
}
