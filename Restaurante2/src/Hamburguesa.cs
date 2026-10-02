using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante
{
    public class Hamburguesa : Comida
    {
        public Hamburguesa() : base("Hamburguesa", 15000)
        {
        }

        public override void Preparar()
        {
            Console.WriteLine("Preparando hamburguesa...");
            Console.WriteLine("Cocinando la carne...");
            Console.WriteLine("Preparando el pan...");
            Console.WriteLine("Agregando lechuga...");
            Console.WriteLine("Agregando tomate...");
            Console.WriteLine("Agregando cebolla...");
            Console.WriteLine("Agregando salsa...");
        }
    }
}