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
            Field<AnimalTypeEnumType>(nameof(Animal.AnimalType)).Description = "The type of the Animal.";
        }
    }
}
