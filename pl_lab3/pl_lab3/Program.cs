using pl_lab3;
using System.Threading.Channels;
internal class Program
{
    static async Task Main()
    {
        Console.Write("Введите количество узлов: ");
        int countNodes = Convert.ToInt32(Console.ReadLine());

        var nodes = RingNodesFactory.CreateNodes(countNodes);
        var nodeForSendingToken = nodes.First();

        while (true)
        {
            Console.WriteLine("\nЦепь готова\n");

            Console.Write("Введите tll: ");
            int ttl = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите номер узла, который будет получать сообщение: ");
            int receiverId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите сообщение: ");
            string data = Console.ReadLine() ?? "";

            var token = new Token(data, receiverId, ttl);
            await nodeForSendingToken.Send(token);

            Thread.Sleep(5000);
        }
    }
}