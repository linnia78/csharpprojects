using GraphQL.Models.Data;
using GraphQL.Types;

namespace GraphQL.Models.GraphQL
{
    public class AnimalType : ObjectGraphType<Animal>
    {
        public AnimalType()
        {
            Field(x => x.Id).Description("The id of the Animal.");
            Field(x => x.Name).Description("The name of the Animal.");
        }
    }

    public class AnimalQuery : ObjectGraphType
    {
        public AnimalQuery()
        {
            Field<AnimalType>(
                "animal",
                resolve: context => new Animal { Id = 1, Name = "Pooky" }
            );
        }
    }
}
