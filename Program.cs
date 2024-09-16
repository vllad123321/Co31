using System;
using System.Collections.Generic;

public class Servers
{
    // Статическое поле для хранения единственного экземпляра
    private static readonly Lazy<Servers> _instance = new Lazy<Servers>(() => new Servers());

    // Множество для хранения уникальных серверов
    private readonly HashSet<string> _servers;

    // Конструктор приватный, чтобы предотвратить создание экземпляра извне
    private Servers()
    {
        _servers = new HashSet<string>();
    }

    // Свойство для получения единственного экземпляра класса
    public static Servers Instance => _instance.Value;

    // Метод для добавления сервера
    public bool AddServer(string address)
    {
        if (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            address.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return _servers.Add(address); // Метод Add возвращает true, если элемент был добавлен
        }
        return false; // Если адрес не начинается с http:// или https://
    }
}

class Program
{
    static void Main()
    {
        var servers = Servers.Instance;

        bool added1 = servers.AddServer("http://example.com");
        Console.WriteLine($"Server added: {added1}"); // Output: Server added: True

        bool added2 = servers.AddServer("http://example.com");
        Console.WriteLine($"Server added: {added2}"); // Output: Server added: False

        bool added3 = servers.AddServer("ftp://example.com");
        Console.WriteLine($"Server added: {added3}"); // Output: Server added: False

        bool added4 = servers.AddServer("https://anotherexample.com");
        Console.WriteLine($"Server added: {added4}"); // Output: Server added: True
    }
}