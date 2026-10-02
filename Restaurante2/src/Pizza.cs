using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante
{
    public class Pizza : Comida
    {
        public Pizza() : base("Pizza", 18000)
        {
        }

        public override void Preparar()
        {
            Console.WriteLine("Preparando pizza...");
            Console.WriteLine("Preparando la masa...");
            Console.WriteLine("Agregando salsa de tomate...");
            Console.WriteLine("Agregando queso...");
            Console.WriteLine("Agregando jamón...");
            Console.WriteLine("Horneando la pizza...");
        }
    }
}