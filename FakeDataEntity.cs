using Bogus;

public static class FakeDataEntity
{
    public static List<Entity> GetListFakeEntities()
    {
        var entityFaker = new Faker<Entity>("pt_BR")
        .RuleFor(c => c.Id, f => (ushort)f.IndexFaker)
        .RuleFor(c => c.Name, f => f.Name.FullName())
        .RuleFor(c => c.Email, f => f.Internet.Email())
        ;

        var entities = entityFaker.Generate(50000);

        return entities;
    }
}