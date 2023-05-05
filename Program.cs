public class Program
{
    public static void Main()
    {
        var m = new AppDomainMonitor();

        var entities = FakeDataEntity.GetListFakeEntities();


        Console.WriteLine(m.TakeSnapshot());
    }

    public static void SendMessage(Entity entity)
    {
        Console.WriteLine($"Id: {entity.Id}, Nome: {entity.Name}, Email: {entity.Email}");
    }
}
