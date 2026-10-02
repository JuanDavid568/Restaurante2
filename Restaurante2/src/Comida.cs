using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante
{
    public abstract class Comida
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }

        public Comida(string nombre, double precio)
        {
            Nombre = nombre;
            Precio = precio;
        }

        public abstract void Preparar();
    }
}