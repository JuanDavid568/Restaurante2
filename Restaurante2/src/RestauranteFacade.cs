using System;

namespace Restaurante
{
    public class RestauranteFacade
    {
        public void RealizarPedido(
            int opcionComida,
            bool queso,
            bool tocineta,
            bool papas,
            bool pepperoni,
            bool champinones,
            bool aceitunas,
            bool maiz)
        {
            FabricaComida fabrica;

            switch (opcionComida)
            {
                case 1:
                    fabrica = new FabricaHamburguesa();
                    break;

                case 2:
                    fabrica = new FabricaPizza();
                    break;

                case 3:
                    fabrica = new FabricaPerroCaliente();
                    break;

                default:
                    Console.WriteLine("Opción de comida no válida.");
                    return;
            }

            Comida comida = fabrica.CrearComida();

            if (opcionComida == 1)
            {
                if (queso)
                    comida = new ConQueso(comida);

                if (tocineta)
                    comida = new ConTocineta(comida);

                if (papas)
                    comida = new ConPapas(comida);
            }

            if (opcionComida == 2)
            {
                if (pepperoni)
                    comida = new ConPepperoni(comida);

                if (champinones)
                    comida = new ConChampinones(comida);

                if (aceitunas)
                    comida = new ConAceitunas(comida);
            }

            if (opcionComida == 3)
            {
                if (queso)
                    comida = new ConQueso(comida);

                if (tocineta)
                    comida = new ConTocineta(comida);

                if (maiz)
                    comida = new ConMaiz(comida);
            }

            Pedido pedido = new Pedido(comida);

            PedidoProxy proxy = new PedidoProxy(pedido);

            proxy.ProcesarPedido();
        }
    }
}
