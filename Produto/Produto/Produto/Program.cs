using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace Produto
{
    class Program
    {
        static void Main(string[] args)
        {
            // Criando conexão com o servidor
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                // Canal onde temos a maior parte da API que realiza as funções
                using (var channel = connection.CreateModel())
                {
                    // Declara uma fila para enviar
                    channel.QueueDeclare(queue: "Hello",
                                          durable: false,
                                          exclusive: false,
                                          autoDelete: false,
                                          arguments: null);

                    // O conteúdo da mensagem é uma matriz de bytes
                    int count = 0;
                    while (true)
                    {
                        string message = $"{count++} Olá Mundo!";
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "Hello",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine("[x] Enviado {0}", message);
                        Thread.Sleep(200);
                    }
                }
            }
        }
    }
}
