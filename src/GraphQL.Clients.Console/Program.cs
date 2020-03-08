using GraphQL.Models.GraphQL;
using GraphQL.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL.Clients.Console
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await RouteAsync();
        }

        private static Dictionary<string, (string description, string request, Func<string, Task> operation)>
            Operations => new Dictionary<string, (string description, string request, Func<string, Task> operation)>
            {
                { "a", ("single query (shorthand syntax) - only for single query", "{ animal { id name } }", RunAnimalQueryAsync) },
                { "b", ("single query (regular syntax)", "query { animal { id name } }", RunAnimalQueryAsync) },
                { "c", ("single query (add operation name) - optional if there is only a single operation", "query CustomOperation { animal { id name } }", RunAnimalQueryAsync) },
                { "d", ("single query (add operation name in execution option) - same as \"d\"", "query CustomOperationNameAsExecutionOption { animal { id name } }", RunAnimalQueryWithCustomOperationExecutionOptionAsync) },
                { "e", ("single query (enumeration) - an explict graph ql type has to be defined for enum", "{ animal { name animalType }}", RunAnimalQueryAsync) },
                { "f", ("arguments (filter by id) - one result", "{ animal(id: 1) { id name } }", RunAnimalQueryWithArgumentAsync) },
                { "g", ("arguments (filter by id) - no result", "{ animal(id: -1) { id name } }", RunAnimalQueryWithArgumentAsync) },
                { "h", ("query alias (alias result) - two queries", "{ dogAlias : animal(id : 1) { id name }, humanAlias : animal(id : 2) { id name } }", RunAnimalQueryWithArgumentAsync) },
                { "i", ("fragments (alias and fragments) - reuse query fields by defining fragments", "query { dogAlias : animal(id : 1) { ...fragmentFields }, humanAlias : animal(id : 2) { ...fragmentFields } } fragment fragmentFields on AnimalType { id name }", RunAnimalQueryWithArgumentAsync) },
                { "j", ("variable query",
                        @"{
                            ""query"": ""query AnimalQuery($animalId: Int) { animal(id: $animalId) { id name } }"",
                            ""variables"": { ""animalId"": 2 }
                        }"
                        , RunAnimalQueryWithVariableAsync) },
                { "k", ("directives - include",
                        @"{
                            ""query"": ""query AnimalQuery($animalId: Int, $includeName: Boolean!) { animal(id: $animalId) { id name @include(if: $includeName) } }"",
                            ""variables"": { ""animalId"": 2, ""includeName"": true }
                        }"
                        , RunAnimalQueryWithVariableAsync) },
                { "l", ("directives - skip",
                        @"{
                            ""query"": ""query AnimalQuery($animalId: Int, $skipName: Boolean!) { animal(id: $animalId) { id name @skip(if: $skipName) } }"",
                            ""variables"": { ""animalId"": 1, ""skipName"": true }
                        }"
                        , RunAnimalQueryWithVariableAsync) },
                { "x", ("exit", null, (request) => {
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
                await Operations[input].operation(Operations[input].request);
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Invalid input.");
            }
            await RouteAsync();
        }

        public static async Task RunAnimalQueryWithVariableAsync(string request)
        {
            var schema = new Schema { Query = new AnimalQueryWithArgument() };
            var requestObject = JsonConvert.DeserializeObject<JObject>(request);

            var json = await schema.ExecuteAsync(_ =>
            {
                _.Query = requestObject["query"].ToString();
                _.Inputs = JsonConvert.SerializeObject(requestObject["variables"]).ToInputs();
            });
            
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"request - {request}");
            System.Console.WriteLine(json);
        }

        public static async Task RunAnimalQueryAsync(string query)
        {
            var schema = new Schema { Query = new AnimalQuery() };
            var json = await schema.ExecuteAsync(_ =>
            {
                _.Query = query;
            });
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"query - {query}");
            System.Console.WriteLine(json);
        }

        private static async Task RunAnimalQueryWithCustomOperationExecutionOptionAsync(string query)
        {
            var schema = new Schema { Query = new AnimalQuery() };
            var json = await schema.ExecuteAsync(_ =>
            {
                _.OperationName = "CustomOperationNameAsExecutionOption";
                _.Query = query;
            });
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"query - {query}");
            System.Console.WriteLine(json);
        }

        private static async Task RunAnimalQueryWithArgumentAsync(string query)
        {
            var schema = new Schema { Query = new AnimalQueryWithArgument() };
            var json = await schema.ExecuteAsync(_ =>
            {
                _.Query = query;
            });
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"query - {query}");
            System.Console.WriteLine(json);
        }
    }
}
