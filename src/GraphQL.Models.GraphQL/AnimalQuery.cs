using GraphQL.Models.Data;
using GraphQL.Types;

namespace GraphQL.Models.GraphQL
{
    public class AnimalQuery : ObjectGraphType
    {
        public AnimalQuery()
        {
            Field<AnimalType>(
                "animal",
                resolve: context => new Animal { Id = 1, Name = "Pooky", AnimalType = AnimalTypeEnum.Dog }
            );
        }
    }
}
