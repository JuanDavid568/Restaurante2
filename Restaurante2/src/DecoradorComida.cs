using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante
{
    public abstract class DecoradorComida : Comida
    {
        protected Comida _comida;

        protected DecoradorComida(Comida comida) : base(comida.Nombre, comida.Precio)
        {
            _comida = comida;
        }

        public override void Preparar()
        {
            _comida.Preparar();
        }
    }

    public class ConQueso : DecoradorComida
    {
        public ConQueso(Comida comida) : base(comida)
        {
            Nombre += " con queso";
            Precio += 2000;
        }

        public override void Preparar()
        {
            base.Preparar();
            Console.WriteLine("Agregando queso...");
        }
    }

    public class ConTocineta : DecoradorComida
    {
        public ConTocineta(Comida comida) : base(comida)
        {
            Nombre += " con tocineta";
            Precio += 3000;
        }

        public override void Preparar()
        {
            base.Preparar();
            Console.WriteLine("Agregando tocineta...");
        }
    }

    public class ConPapas : DecoradorComida
    {
        public ConPapas(Comida comida) : base(comida)
        {
            Nombre += " con papas";
            Precio += 4000;
        }

        public override void Preparar()
        {
            base.Preparar();
            Console.WriteLine("Agregando papas...");
        }
    }

    public class ConPepperoni : DecoradorComida
    {
        public ConPepperoni(Comida comida) : base(comida)
        {
            Nombre += " con pepperoni";
            Precio += 3000;
        }

        public override void Preparar()
        {
            base.Preparar();
            Console.WriteLine("Agregando pepperoni...");
        }
    }

    public class ConChampinones : DecoradorComida
    {
        public ConChampinones(Comida comida) : base(comida)
        {
            Nombre += " con champiñones";
            Precio += 2000;
        }

        public override void Preparar()
        {
            base.Preparar();
            Console.WriteLine("Agregando champiñones...");
        }
    }

    public class ConAceitunas : DecoradorComida
    {
        public ConAceitunas(Comida comida) : base(comida)
        {
            Nombre += " con aceitunas";
            Precio += 2000;
        }

        public override void Preparar()
        {
            base.Preparar();
            Console.WriteLine("Agregando aceitunas...");
        }
    }

    public class ConMaiz : DecoradorComida
    {
        public ConMaiz(Comida comida) : base(comida)
        {
            Nombre += " con maíz";
            Precio += 2000;
        }

        public override void Preparar()
        {
            base.Preparar();
            Console.WriteLine("Agregando maíz...");
        }
    }
}
