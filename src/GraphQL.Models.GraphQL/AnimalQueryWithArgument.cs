using GraphQL.Models.Data;
using GraphQL.Types;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.Models.GraphQL
{
    public class AnimalQueryWithArgument : ObjectGraphType
    {
        private List<Animal> _animals = new List<Animal>
        {
            new Animal { Id = 1, Name = "Pooky", AnimalType = AnimalTypeEnum.Dog },
            new Animal { Id = 2, Name = "Romina", AnimalType = AnimalTypeEnum.Human }
        };

        public AnimalQueryWithArgument()
        {
            Field<AnimalType>(
                "animal",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return _animals.FirstOrDefault(x => x.Id == id);
                });
        }
    }
}
