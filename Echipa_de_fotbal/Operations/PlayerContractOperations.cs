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
    public class PlayerContractOperations
    {
        public void AddPlayerContract(ContractJucator playerContractToAdd)
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

            dbContext.PlayersContracts.Add(playerContractToAdd);
            dbContext.SaveChanges();

        }
        public void UpdatePlayerContract(int idToEdit, ContractJucator playerContractUpdate)
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
            var playerContractToUpdate = dbContext.PlayersContracts.FirstOrDefault(pc => pc.IdContractJucator == idToEdit);
            if (playerContractToUpdate != null)
            {
                playerContractToUpdate.Salariu=playerContractUpdate.Salariu;
                playerContractToUpdate.DataSemnare = playerContractUpdate.DataSemnare;
                playerContractToUpdate.DataExpirare = playerContractUpdate.DataExpirare;
                playerContractToUpdate.ClauzaReziliere = playerContractUpdate.ClauzaReziliere;
                playerContractToUpdate.IdEchipa = playerContractUpdate.IdEchipa;
                playerContractToUpdate.IdJucator = playerContractUpdate.IdJucator;
                dbContext.SaveChanges();
            }
        }
        public void DeletePlayerContract(int idToDelete)
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

            var playerContractToDelete = dbContext.PlayersContracts.FirstOrDefault(pc => pc.IdContractJucator == idToDelete);
            if (playerContractToDelete != null)
            {
                dbContext.PlayersContracts.Remove(playerContractToDelete);
                dbContext.SaveChanges();
            }
        }
        public void ReadPlayerContract()
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

            var playerContracts = dbContext.PlayersContracts.ToList();
            Console.WriteLine("PLayers and contracts:");
            foreach (var playerContract in playerContracts)
            {
                Console.WriteLine($"{playerContract.IdContractJucator}: {dbContext.Teams.Where(t => t.IdEchipa == playerContract.IdEchipa).Select(t => t.NumeEchipa).FirstOrDefault()}, {dbContext.Players.Where(p => p.IdJucator == playerContract.IdJucator).Select(p => p.Nume + " " + p.Prenume).FirstOrDefault()}, {playerContract.Salariu}, {playerContract.DataSemnare}, {playerContract.DataExpirare}, {playerContract.ClauzaReziliere} ");
            }
        }

    }
}

