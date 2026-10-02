using System;
using Restaurante;

class Program
{
    static bool LeerSiNo(string pregunta)
    {
        Console.Write(pregunta + " (s/n): ");
        string respuesta = Console.ReadLine();
        return respuesta != null && respuesta.Trim().ToLower() == "s";
    }

    static void Main(string[] args)
    {
        Console.WriteLine("=================================");
        Console.WriteLine("          RESTAURANTE");
        Console.WriteLine("=================================");
        Console.WriteLine();
        Console.WriteLine("MENÚ");
        Console.WriteLine("1. Hamburguesa - $15000");
        Console.WriteLine("2. Pizza - $18000");
        Console.WriteLine("3. Perro Caliente - $12000");
        Console.WriteLine();
        Console.WriteLine("Seleccione una comida:");

        string entrada = Console.ReadLine();
        int opcion;
        if (!int.TryParse(entrada, out opcion))
        {
            opcion = 0;
        }

        bool queso = false;
        bool tocineta = false;
        bool papas = false;

        bool pepperoni = false;
        bool champinones = false;
        bool aceitunas = false;

        bool maiz = false;

        if (opcion == 1)
        {
            Console.WriteLine();
            Console.WriteLine("ADICIONALES PARA HAMBURGUESA");
            queso = LeerSiNo("¿Desea agregar queso?");
            tocineta = LeerSiNo("¿Desea agregar tocineta?");
            papas = LeerSiNo("¿Desea agregar papas?");
        }
        else if (opcion == 2)
        {
            Console.WriteLine();
            Console.WriteLine("ADICIONALES PARA PIZZA");
            pepperoni = LeerSiNo("¿Desea agregar pepperoni?");
            champinones = LeerSiNo("¿Desea agregar champiñones?");
            aceitunas = LeerSiNo("¿Desea agregar aceitunas?");
        }
        else if (opcion == 3)
        {
            Console.WriteLine();
            Console.WriteLine("ADICIONALES PARA PERRO CALIENTE");
            queso = LeerSiNo("¿Desea agregar queso?");
            tocineta = LeerSiNo("¿Desea agregar tocineta?");
            maiz = LeerSiNo("¿Desea agregar maíz?");
        }

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("       PROCESANDO PEDIDO");
        Console.WriteLine("=================================");

        RestauranteFacade restaurante = new RestauranteFacade();

        restaurante.RealizarPedido(
            opcion,
            queso,
            tocineta,
            papas,
            pepperoni,
            champinones,
            aceitunas,
            maiz
        );

        Console.WriteLine();
        Console.WriteLine("Presione una tecla para salir...");
        Console.ReadKey();
    }
}
