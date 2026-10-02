using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante
{
    public class PerroCaliente : Comida
    {
        public PerroCaliente() : base("Perro Caliente", 12000)
        {
        }

        public override void Preparar()
        {
            Console.WriteLine("Preparando perro caliente...");
            Console.WriteLine("Cocinando la salchicha...");
            Console.WriteLine("Calentando el pan...");
            Console.WriteLine("Agregando cebolla...");
            Console.WriteLine("Agregando papas...");
            Console.WriteLine("Agregando salsa de tomate...");
            Console.WriteLine("Agregando salsa rosada...");
            Console.WriteLine("Agregando mostaza...");
        }
    }
}