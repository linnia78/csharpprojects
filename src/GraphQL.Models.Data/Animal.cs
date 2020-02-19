namespace GraphQL.Models.Data
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AnimalTypeEnum AnimalType { get; set; }
    }

    public enum AnimalTypeEnum
    {
        Dog = 1,
        Cat = 2,
        Lion = 3,
        Human = 4
    }


}
