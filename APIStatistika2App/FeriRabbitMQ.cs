using Microsoft.AspNetCore.Connections;
using System.Text;
using RabbitMQ.Client;


namespace APIStatistikaApp
{
    public class FeriRabbitMQ
    {
        private string _message;
        private const string strExchange = "iir-rv1-3";
        private const string strQueue = "iir-rv1-3";
        private const string strUrl = "http://studentdocker.informatika.uni-mb.si:10013/statistika";

        public FeriRabbitMQ(string message)
        {
            _message = message;
            CreateRabbitMQObject();
        }

        public void CreateRabbitMQObject()
        {
            var factory = new ConnectionFactory();

            factory.UserName = "student";
            factory.Password = "student123";
            factory.HostName = "studentdocker.informatika.uni-mb.si";
            factory.Port = 5672;


            IConnection conn = factory.CreateConnection();

            using (var channel = conn.CreateModel())
            {
                // Deklariramo Exchange. Tip je Direct.
                channel.ExchangeDeclare(exchange: strExchange, type: ExchangeType.Direct, durable: true);

                // Definiramo osnovne lastnosti sporocila (basicProperites), ki jih uporabimo pri objavi (BasicPublish).
                var lastnostiSporocila = channel.CreateBasicProperties();
                lastnostiSporocila.Persistent = true;
                lastnostiSporocila.Headers = new Dictionary<string, object>();
                lastnostiSporocila.CorrelationId = "S001";
                lastnostiSporocila.Type = "INFO";
                lastnostiSporocila.Headers.Add("timestamp", DateTime.Now.ToString());
                lastnostiSporocila.Headers.Add("url", strUrl);
                lastnostiSporocila.Headers.Add("imeApl", "StatistikaAPI");

                var messageBody = Encoding.UTF8.GetBytes(_message);
                channel.BasicPublish(exchange: strExchange, routingKey: "", basicProperties: lastnostiSporocila, body: messageBody);
            }
        }
    }




}
