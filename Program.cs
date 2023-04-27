using System.Net;
using System.Net.NetworkInformation;

namespace CheckSiteStatus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a valid web address (or press Enter): ");
            string? url = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(url)) // if they enter nothing...
            {
                // ... set a default URL
                url = "https://www.google.com/search?q=uri";
            }
            Uri uri = new(url);
            Console.WriteLine($"URL: {url}");
            Console.WriteLine($"Scheme: {uri.Scheme}");
            Console.WriteLine($"Port: {uri.Port}");
            Console.WriteLine($"Host: {uri.Host}");
            Console.WriteLine($"Path: {uri.AbsolutePath}");
            Console.WriteLine($"Query: {uri.Query}");
            Console.WriteLine();

            IPHostEntry entry = Dns.GetHostEntry(uri.Host);
            Console.WriteLine($"{entry.HostName} has the following IP addresses:");
            foreach (IPAddress address in entry.AddressList)
            {
                Console.WriteLine($" {address} ({address.AddressFamily})");
            }

            Console.WriteLine();

            try
            {
                Ping ping = new();
                Console.WriteLine("Pinging server. Please wait...");
                PingReply reply = ping.Send(uri.Host);
                Console.WriteLine($"{uri.Host} was pinged and replied: {reply.Status}.");

                if (reply.Status == IPStatus.Success)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("Reply from {0} took {1:N0}ms",
                                            arg0: reply.Address,
                                            arg1: reply.RoundtripTime);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().ToString()} says {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}