// See https://aka.ms/new-console-template for more information
using Echipa_de_fotbal.Models;
using Echipa_de_fotbal.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Transactions;


PlayerOperations playerOperations = new PlayerOperations();
TeamOperations teamOperations = new TeamOperations();
PlayerContractOperations playerContractOperations = new PlayerContractOperations();

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

while (true)
{
    Console.WriteLine("Choose the table where you want to make an operation: \n Player \n Team \n PlayerContract \n Exit");
    string table = Console.ReadLine();
    switch (table)
    {
        case "Player":
            {
                bool val = true;
                while (val)
                {
                    Console.WriteLine("Choose an operation: \n Add \n Update \n Delete \n Read \n Exit");
                    string operation = Console.ReadLine();
                    switch (operation)
                    {
                        case "Add":
                            {
                                var id = dbContext.Players
                                      .Select(p => (int?)p.IdJucator)
                                      .Max();
                                if (id == null)
                                {
                                    id = 1;
                                }
                                else
                                {
                                    id++;
                                }
                                Console.WriteLine("Enter the last name of the player");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter the first name of the player");
                                string firstName = Console.ReadLine();
                                Console.WriteLine("Enter the birthdate");
                                DateOnly birthday;
                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly date))
                                {
                                    birthday = date;

                                }
                                else
                                {
                                    Console.WriteLine("The date was not in the correct format");
                                    break;
                                }
                                Console.WriteLine("Enter the position");
                                string position = Console.ReadLine();
                                Console.WriteLine("Enter the height");
                                int height;
                                if (int.TryParse(Console.ReadLine(), out int h))
                                {
                                    height = h;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }
                                Console.WriteLine("Enter the weight");
                                int weight;
                                if (int.TryParse(Console.ReadLine(), out int w))
                                {
                                    weight = w;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }

                                Console.WriteLine("Enter the name of the player's team");
                                string teamName = Console.ReadLine();
                                var idTeam = dbContext.Teams
                                  .Where(t => t.NumeEchipa == teamName)
                                  .Select(t => t.IdEchipa)
                                  .FirstOrDefault();
                                if (idTeam == 0)
                                {
                                    break;
                                }
                                Jucator newPlayer = new Jucator { IdJucator = (int)id, Nume = name, Prenume = firstName, DataN = birthday, Pozitie = position, Inaltime = height, Greutate = weight, IdEchipa = idTeam };
                                playerOperations.AddPlayer(newPlayer);
                                break;
                            }
                        case "Delete":
                            {
                                Console.WriteLine("Enter the id of the player you want to delete");
                                int idPlayer;
                                if (int.TryParse(Console.ReadLine(), out int i))
                                {
                                    idPlayer = i;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }
                                playerOperations.DeletePlayer(idPlayer);
                                break;
                            }
                        case "Update":
                            {
                                Console.WriteLine("Enter the id of the player you want to update:");
                                int id;
                                if (int.TryParse(Console.ReadLine(), out int i))
                                {
                                    id = i;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }
                                Console.WriteLine("Now enter the new details");
                                Console.WriteLine("Enter the last name of the player");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter the first name of the player");
                                string firstName = Console.ReadLine();
                                Console.WriteLine("Enter the birthdate");
                                DateOnly birthday;
                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly date))
                                {
                                    birthday = date;

                                }
                                else
                                {
                                    Console.WriteLine("The date was not in the correct format");
                                    break;
                                }
                                Console.WriteLine("Enter the position");
                                string position = Console.ReadLine();
                                Console.WriteLine("Enter the height");
                                int height;
                                if (int.TryParse(Console.ReadLine(), out int h))
                                {
                                    height = h;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }
                                Console.WriteLine("Enter the weight");
                                int weight;
                                if (int.TryParse(Console.ReadLine(), out int w))
                                {
                                    weight = w;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }

                                Console.WriteLine("Enter the name of the player's team");
                                string teamName = Console.ReadLine();
                                var idTeam = dbContext.Teams
                                  .Where(t => t.NumeEchipa == teamName)
                                  .Select(t => t.IdEchipa)
                                  .FirstOrDefault();
                                if (idTeam == 0)
                                {
                                    break;
                                }
                                Jucator updatedPLayer = new Jucator { IdJucator = id, Nume = name, Prenume = firstName, DataN = birthday, Pozitie = position, Inaltime = height, Greutate = weight, IdEchipa = idTeam };
                                playerOperations.UpdatePlayer(id, updatedPLayer);
                                break;
                            }
                        case "Read":
                            {
                                playerOperations.ReadPlayer();
                                break;
                            }
                        case "Exit":
                            {
                                val = false;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                break;
            }
        case "Team":
            {
                bool val = true;
                while (val)
                {
                    Console.WriteLine("Choose an operation: \n Add \n Update \n Delete \n Read \n Exit ");
                    string operation = Console.ReadLine();
                    switch (operation)
                    {
                        case "Add":
                            {
                                var id = dbContext.Teams
                                      .Select(t => (int?)t.IdEchipa)
                                      .Max();
                                if (id == null)
                                {
                                    id = 1;
                                }
                                else
                                {
                                    id++;
                                }
                                Console.WriteLine("Enter the name of the team");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter the name of the chairman");
                                string chairmanName = Console.ReadLine();
                                
                                Console.WriteLine("Enter the name of the championship");
                                string championship = Console.ReadLine();
                                var idChampionship = dbContext.Championships
                                  .Where(c => c.NumeCampionat == championship)
                                  .Select(c => c.IdCampionat)
                                  .FirstOrDefault();
                                if (idChampionship == 0)
                                {
                                    break;
                                }

                                Console.WriteLine("Enter the name of the stadium");
                                string stadium = Console.ReadLine();
                                var idStadium = dbContext.Stadiums
                                  .Where(s => s.NumeStadion == stadium)
                                  .Select(s => s.IdStadion)
                                  .FirstOrDefault();
                                if (idStadium == 0)
                                {
                                    break;
                                }
                                Echipa newTeam = new Echipa {IdEchipa = (int)id, NumeEchipa=name, NumePatron=chairmanName, IdCampionat = idChampionship, IdStadion = idStadium };
                                teamOperations.AddTeam(newTeam);
                                break;
                            }
                        case "Delete":
                            {
                                Console.WriteLine("Enter the id of the team you want to delete");
                                int idTeam;
                                if (int.TryParse(Console.ReadLine(), out int i))
                                {
                                    idTeam = i;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }
                                teamOperations.DeleteTeam(idTeam);
                                break;
                            }
                        case "Update":
                            {
                                Console.WriteLine("Enter the id of the team you want to update");
                                int id;
                                if (int.TryParse(Console.ReadLine(), out int i))
                                {
                                    id = i;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }
                                Console.WriteLine("Now enter the new details");
                                Console.WriteLine("Enter the name of the team");
                                string name = Console.ReadLine();

                                Console.WriteLine("Enter the name of the chairman");
                                string chairmanName = Console.ReadLine();

                                Console.WriteLine("Enter the name of the championship");
                                string championship = Console.ReadLine();
                                var idChampionship = dbContext.Championships
                                  .Where(c => c.NumeCampionat == championship)
                                  .Select(c => c.IdCampionat)
                                  .FirstOrDefault();
                                if (idChampionship == 0)
                                {
                                    break;
                                }

                                Console.WriteLine("Enter the name of the stadium");
                                string stadium = Console.ReadLine();
                                var idStadium = dbContext.Stadiums
                                  .Where(s => s.NumeStadion == stadium)
                                  .Select(s => s.IdStadion)
                                  .FirstOrDefault();
                                if (idStadium == 0)
                                {
                                    break;
                                }
                                Echipa newTeam = new Echipa { IdEchipa = (int)id, NumeEchipa = name, NumePatron = chairmanName, IdCampionat = idChampionship, IdStadion = idStadium };
                                teamOperations.UpdateTeam(id, newTeam);
                                break;
                            }
                        case "Read":
                            {
                                teamOperations.ReadTeams();
                                break;
                            }
                        case "Exit":
                            {
                                val = false;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                break;
            }
        case "PlayerContract":
            {
                bool val = true;
                while (val)
                {
                    Console.WriteLine("Choose an operation: \n Add \n Update \n Delete \n Read \n Exit ");
                    string operation = Console.ReadLine();
                    switch (operation)
                    {
                        case "Add":
                            {
                                var id = dbContext.PlayersContracts
                                      .Select(pc => (int?)pc.IdContractJucator)
                                      .Max();
                                if (id == null)
                                {
                                    id = 1;
                                }
                                else
                                {
                                    id++;
                                }
                                Console.WriteLine("Enter the name of the team");
                                string team = Console.ReadLine();
                                var idTeam = dbContext.Teams
                                  .Where(t => t.NumeEchipa == team)
                                  .Select(t => t.IdEchipa)
                                  .FirstOrDefault();
                                if (idTeam == 0)
                                {
                                    break;
                                }
                                Console.WriteLine("Enter the last name of the player");
                                string lastName = Console.ReadLine();
                                Console.WriteLine("Enter the first name of the player");
                                string firstName = Console.ReadLine();

                                var idPlayer = dbContext.Players
                                  .Where(p => p.Nume == lastName && p.Prenume==firstName)
                                  .Select(p => p.IdJucator)
                                  .FirstOrDefault();
                                if (idPlayer == 0)
                                {
                                    break;
                                }

                                Console.WriteLine("Enter the salary");
                                int salary;
                                if (int.TryParse(Console.ReadLine(), out int s))
                                {
                                    salary = s;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }

                                Console.WriteLine("Enter the date of signing");
                                DateOnly signatureDate;
                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly sd))
                                {
                                    signatureDate = sd;

                                }
                                else
                                {
                                    Console.WriteLine("The date was not in the correct format");
                                    break;
                                }

                                Console.WriteLine("Enter the expiration date");
                                DateOnly expirationDate;
                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly ed))
                                {
                                    expirationDate = ed;

                                }
                                else
                                {
                                    Console.WriteLine("The date was not in the correct format");
                                    break;
                                }

                                Console.WriteLine("Enter the release clause");
                                int releaseClause;
                                if (int.TryParse(Console.ReadLine(), out int rc))
                                {
                                    releaseClause = rc;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }

                                ContractJucator newContractJucator = new ContractJucator { IdContractJucator = (int)id, Salariu = salary, DataSemnare=signatureDate, DataExpirare=expirationDate, ClauzaReziliere=releaseClause, IdEchipa=idTeam, IdJucator=idPlayer };
                                playerContractOperations.AddPlayerContract(newContractJucator);
                                break;
                            }
                        case "Delete":
                            {
                                Console.WriteLine("Enter the id of the contract you want to delete");
                                int idPlayerContract;
                                if (int.TryParse(Console.ReadLine(), out int i))
                                {
                                    idPlayerContract = i;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }
                                playerContractOperations.DeletePlayerContract(idPlayerContract);
                                break;
                            }
                        case "Update":
                            {
                                Console.WriteLine("Enter the id of the contract you want to update");
                                int id;
                                if (int.TryParse(Console.ReadLine(), out int i))
                                {
                                    id = i;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }

                                Console.WriteLine("Enter the name of the team");
                                string team = Console.ReadLine();
                                var idTeam = dbContext.Teams
                                  .Where(t => t.NumeEchipa == team)
                                  .Select(t => t.IdEchipa)
                                  .FirstOrDefault();
                                if (idTeam == 0)
                                {
                                    break;
                                }
                                Console.WriteLine("Enter the last name of the player");
                                string lastName = Console.ReadLine();
                                Console.WriteLine("Enter the first name of the player");
                                string firstName = Console.ReadLine();

                                var idPlayer = dbContext.Players
                                  .Where(p => p.Nume == lastName && p.Prenume == firstName)
                                  .Select(p => p.IdJucator)
                                  .FirstOrDefault();
                                if (idPlayer == 0)
                                {
                                    break;
                                }

                                Console.WriteLine("Enter the salary");
                                int salary;
                                if (int.TryParse(Console.ReadLine(), out int s))
                                {
                                    salary = s;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }

                                Console.WriteLine("Enter the date of signing");
                                DateOnly signatureDate;
                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly sd))
                                {
                                    signatureDate = sd;

                                }
                                else
                                {
                                    Console.WriteLine("The date was not in the correct format");
                                    break;
                                }

                                Console.WriteLine("Enter the expiration date");
                                DateOnly expirationDate;
                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly ed))
                                {
                                    expirationDate = ed;

                                }
                                else
                                {
                                    Console.WriteLine("The date was not in the correct format");
                                    break;
                                }

                                Console.WriteLine("Enter the release clause");
                                int releaseClause;
                                if (int.TryParse(Console.ReadLine(), out int rc))
                                {
                                    releaseClause = rc;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer");
                                    break;
                                }

                                ContractJucator newContractJucator = new ContractJucator { IdContractJucator = (int)id, Salariu = salary, DataSemnare = signatureDate, DataExpirare = expirationDate, ClauzaReziliere = releaseClause, IdEchipa = idTeam, IdJucator = idPlayer };
                                playerContractOperations.UpdatePlayerContract(id,newContractJucator);
                                break;
                            }
                        case "Read":
                            {
                                playerContractOperations.ReadPlayerContract();
                                break;
                            }
                        case "Exit":
                            {
                                val = false;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                break;
            }
        case "Exit":
            {
                return;
            }
        default:
            {
                break;
            }
    }
}
