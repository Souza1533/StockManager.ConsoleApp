using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

class Product
{
    public string Name { get; }
    public double Value { get; }

    public Product(string name, double value)
    {
        Name = name;
        Value = value;
    }
}

class Storage
{
    private readonly List<Product> _products = new();
    public IReadOnlyList<Product> Products => _products;

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public bool IsEmpty()
    {
        return !_products.Any();
    }
}

class ConsoleUI
{
    private readonly Storage _storage = new();

    public void Run()
        {
            int option;
            do
            {
                Console.WriteLine("\n---<  INTERFACE  >---\n");
                Console.WriteLine("1) Ver Estoque");
                Console.WriteLine("2) Criar Produto");
                Console.WriteLine("3) Sair\n");

                option = ReadMenuOption("Escolha uma opção -> ");
                Clear();

                
                switch (option)
                {
                    case 1:
                        ShowProducts();
                        Pause();
                        break;

                    case 2:
                        Console.WriteLine("\n---<  Criando Produto...  >---\n");
                        AddProduct();
                        Pause();
                        break;

                    case 3:
                        Console.WriteLine("\n👋 Saindo do sistema...");
                        Clear();
                        break;

                    default:
                        Console.WriteLine("\n Opção inválida.\n");
                        Pause();
                        Clear();
                        break;
                }
            } while(option != 3);
        }

    public int ReadMenuOption(string message)
    {
        int value;
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write($"❌ Opção inválida. Digite um número valido: ");
        }
        return value;
    }

    public void ShowProducts()
    {
        if (_storage.IsEmpty())
        {
            Console.WriteLine("\n❌ Não há produtos cadastrados ainda.\n");
            return;
        }

            Console.WriteLine("\n📦 Produtos Cadastrados:\n");
            foreach (var product in _storage.Products)
            {
                Console.WriteLine($"{product.Name} - R$ {product.Value:F2}");
            }
        
    }

    public void AddProduct()
    {
        var name = ReadString("Nome: ");
        var value = ReadDouble("Value: ");
        _storage.Add(new Product(name, value));
        Console.WriteLine("\n✅ Produto criado com sucesso!\n");
    }

    public string ReadString(string message)
    {
        Console.Write(message);
        while (true)
        {
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter))
            {
                return input;
            }

            Console.Write("❌ Digite apenas letras (sem números ou símbolos): ");
        }
    }

    public double ReadDouble(string message)
    {
        Console.Write(message);
        double value;
        while (!double.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Valor invalido, digite novamente:\n");
        }
        return value;
    }

    public void Pause()
    {
        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.ReadLine();
    }

    public void Clear()
    {
        Console.Clear();
    }

}


class Program
{
    static void Main()
    {
        new ConsoleUI().Run();
    }
}
