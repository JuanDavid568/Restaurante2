using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante
{
    public abstract class FabricaComida
    {
        public abstract Comida CrearComida();
    }

    public class FabricaHamburguesa : FabricaComida
    {
        public override Comida CrearComida()
        {
            return new Hamburguesa();
        }
    }

    public class FabricaPizza : FabricaComida
    {
        public override Comida CrearComida()
        {
            return new Pizza();
        }
    }

    public class FabricaPerroCaliente : FabricaComida
    {
        public override Comida CrearComida()
        {
            return new PerroCaliente();
        }
    }
}