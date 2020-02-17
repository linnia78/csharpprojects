using GraphQL.Models.GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL.Clients.Console
{
    class Program
    {
        private static Schema _schema;
        public static async Task Main(string[] args)
        {
            _schema = new Schema { Query = new AnimalQuery() };
            
            await RouteAsync();
        }

        private static Dictionary<string, (string description, string query, Func<string, Task> operation)>
            Operations => new Dictionary<string, (string description, string query, Func<string, Task> operation)>
            {
                { "a", ("single query (shorthand syntax) - only for single query", "{ animal { id name } }", RunAsync) },
                { "b", ("single query (regular syntax)", "query { animal { id name } }", RunAsync) },
                { "c", ("single query (add operation name) - optional if there is only a single operation", "query CustomOperation { animal { id name } }", RunAsync) },
                { "x", ("exit", null, (query) => {
                        System.Environment.Exit(0);
                        return Task.CompletedTask;
                    }) 
                }
            };

        public static async Task RouteAsync()
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("Input operation.");
            foreach(var entry in Operations)
            {
                System.Console.WriteLine($"{entry.Key} - {entry.Value.description}");
            }
            
            System.Console.ForegroundColor = ConsoleColor.White;
            var input = System.Console.ReadLine().ToLower();
            if (Operations.ContainsKey(input))
            {
                await Operations[input].operation(Operations[input].query);
            }
            await RouteAsync();
        }

        public static async Task RunAsync(string query)
        {
            var json = await _schema.ExecuteAsync(_ =>
            {
                _.Query = query;
            });
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"query - {query}");
            System.Console.WriteLine(json);
        }
    }
}
