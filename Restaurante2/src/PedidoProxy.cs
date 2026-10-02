using System;

namespace Restaurante
{
    public class Pedido
    {
        public Comida Comida { get; set; }

        public Pedido(Comida comida)
        {
            Comida = comida;
        }

        public void Procesar()
        {
            Console.WriteLine();
            Console.WriteLine("Procesando pedido...");

            Comida.Preparar();

            Console.WriteLine();
            Console.WriteLine("Pedido preparado correctamente.");
            Console.WriteLine("Producto: " + Comida.Nombre);
            Console.WriteLine("Precio: $" + Comida.Precio);
        }
    }

    public class PedidoProxy
    {
        private Pedido pedido;

        public PedidoProxy(Pedido pedido)
        {
            this.pedido = pedido;
        }

        public void ProcesarPedido()
        {
            if (pedido == null || pedido.Comida == null)
            {
                Console.WriteLine("No se puede procesar el pedido.");
                Console.WriteLine("El pedido está vacío.");
                return;
            }

            Console.WriteLine("Pedido validado correctamente.");

            pedido.Procesar();
        }
    }
}
