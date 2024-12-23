using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace CasinoApp
{
    internal class Program
    {
        static void PrintTableClients(Dictionary<string, Client> data)
        {
            Console.WriteLine("Name\t| Contact\t| Registration Date");
            foreach (var item in data.Values)
            {
                Console.WriteLine($"{item.Name}\t| {item.Contact}\t| {item.RegistrationDate:yyyy-MM-dd}");
            }
        }

        static void PrintTableGames(Dictionary<string, Game> data)
        {
            Console.WriteLine("Name\t| Type\t| Min Bet");
            foreach (var item in data.Values)
            {
                Console.WriteLine($"{item.Name}\t| {item.Type}\t| {item.MinBet:C}");
            }
        }

        static void PrintTableBets(Dictionary<string, Bet> data)
        {
            Console.WriteLine("Client Name\t| Game Name\t| Amount\t| Bet Date");
            foreach (var item in data.Values)
            {
                Console.WriteLine($"{item.ClientName}\t| {item.GameName}\t| {item.Amount:C}\t| {item.BetDate:yyyy-MM-dd}");
            }
        }

        static void Main(string[] args)
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "AIzaSyBYTnWJo5B67oZaqMX3NIwQgyTOjGK63z8",
                BasePath = "https://qwert585955858-default-rtdb.europe-west1.firebasedatabase.app/"
            };

            IFirebaseClient client;

            while (true)
            {
                Console.WriteLine("1. Display Data\n2. Enter Data");
                int task = int.Parse(Console.ReadLine());
                int subtask;

                switch (task)
                {
                    case 1:
                        Console.WriteLine("1. Clients, 2. Games, 3. Bets");
                        subtask = int.Parse(Console.ReadLine());
                        switch (subtask)
                        {
                            case 1:
                                client = new FireSharp.FirebaseClient(config);
                                FirebaseResponse resClients = client.Get(@"Clients/");
                                Dictionary<string, Client> dataClients = JsonConvert.DeserializeObject<Dictionary<string, Client>>(resClients.Body);
                                PrintTableClients(dataClients);
                                break;
                            case 2:
                                client = new FireSharp.FirebaseClient(config);
                                FirebaseResponse resGames = client.Get(@"Games/");
                                Dictionary<string, Game> dataGames = JsonConvert.DeserializeObject<Dictionary<string, Game>>(resGames.Body);
                                PrintTableGames(dataGames);
                                break;
                            case 3:
                                client = new FireSharp.FirebaseClient(config);
                                FirebaseResponse resBets = client.Get(@"Bets/");
                                Dictionary<string, Bet> dataBets = JsonConvert.DeserializeObject<Dictionary<string, Bet>>(resBets.Body);
                                PrintTableBets(dataBets);
                                break;
                        }
                        break;

                    case 2:
                        Console.WriteLine("1. Clients, 2. Games, 3. Bets");
                        subtask = int.Parse(Console.ReadLine());
                        switch (subtask)
                        {
                            case 1:
                                Console.WriteLine("Enter Client Name, Contact, Registration Date (YYYY-MM-DD):");
                                var clientData = new Client
                                {
                                    Name = Console.ReadLine(),
                                    Contact = Console.ReadLine(),
                                    RegistrationDate = DateTime.Parse(Console.ReadLine())
                                };
                                client = new FireSharp.FirebaseClient(config);
                                client.Set(@"Clients/" + clientData.Name, clientData);
                                break;
                            case 2:
                                Console.WriteLine("Enter Game Name, Type, Min Bet:");
                                var gameData = new Game
                                {
                                    Name = Console.ReadLine(),
                                    Type = Console.ReadLine(),
                                    MinBet = decimal.Parse(Console.ReadLine())
                                };
                                client = new FireSharp.FirebaseClient(config);
                                client.Set(@"Games/" + gameData.Name, gameData);
                                break;
                            case 3:
                                Console.WriteLine("Enter Client Name, Game Name, Amount, Bet Date (YYYY-MM-DD):");
                                var betData = new Bet
                                {
                                    ClientName = Console.ReadLine(),
                                    GameName = Console.ReadLine(),
                                    Amount = decimal.Parse(Console.ReadLine()),
                                    BetDate = DateTime.Parse(Console.ReadLine())
                                };
                                client = new FireSharp.FirebaseClient(config);
                                client.Set(@"Bets/" + Guid.NewGuid(), betData);
                                break;
                        }
                        break;
                }
            }
        }
    }
}

