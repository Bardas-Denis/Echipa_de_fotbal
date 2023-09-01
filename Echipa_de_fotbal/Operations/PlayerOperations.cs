using Echipa_de_fotbal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Echipa_de_fotbal.Operations
{
    public class PlayerOperations
    {
        public void AddPlayer(Jucator playerToAdd)
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
            dbContext.Players.Add(playerToAdd);
            dbContext.SaveChanges();

        }
        public void UpdatePlayer(int idToEdit, Jucator playerUpdate)
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

            var playerToUpdate = dbContext.Players.FirstOrDefault(p => p.IdJucator == idToEdit);
            if (playerToUpdate != null)
            {
                playerToUpdate.Nume = playerUpdate.Nume;
                playerToUpdate.Prenume = playerUpdate.Prenume;
                playerToUpdate.DataN = playerUpdate.DataN;
                playerToUpdate.Pozitie = playerUpdate.Pozitie;
                playerToUpdate.Inaltime = playerUpdate.Inaltime;
                playerToUpdate.Greutate = playerUpdate.Greutate;
                playerToUpdate.IdEchipa = playerUpdate.IdEchipa;
                dbContext.SaveChanges();
            }
        }
        public void DeletePlayer(int idToDelete)
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

            var playerToDelete = dbContext.Players.FirstOrDefault(p => p.IdJucator == idToDelete);
            if (playerToDelete != null)
            {
                dbContext.Players.Remove(playerToDelete);
                dbContext.SaveChanges();
            }
        }
        public void ReadPlayer()
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
            var players = dbContext.Players.ToList();
            Console.WriteLine("Players:");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.IdJucator}: {player.Nume} {player.Prenume}, Data nasterii: {player.DataN}, {player.Pozitie}, Inaltime: {player.Inaltime}, Greutate: {player.Greutate}, Echpa: {dbContext.Teams.Where(t => t.IdEchipa == player.IdEchipa).Select(t => t.NumeEchipa).FirstOrDefault()}");
            }
        }

    }
}
