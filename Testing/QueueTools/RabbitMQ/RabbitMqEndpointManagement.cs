using System.Collections.Generic;
using RabbitMQ.Client;

namespace QueueTools.RabbitMQ
{
    public class RabbitMqEndpointManagement
    {
        IConnection _connection;
        bool _disposed;

        public RabbitMqEndpointManagement(IConnection connection)
        {
            _connection = connection;
        }

		// Binds Queue to Exchange 
		public void BindQueue(string queueName, string exchangeName, string exchangeType, string routingKey,
            IDictionary<string, object> queueArguments)
        {
            using (IModel model = _connection.CreateModel())
            {
                string queue = model.QueueDeclare(queueName, true, false, false, queueArguments);
                model.ExchangeDeclare(exchangeName, exchangeType, true);

                model.QueueBind(queue, exchangeName, routingKey);

                model.Close(200, "ok");
            }
        }

        public void UnbindQueue(string queueName, string exchangeName, string routingKey)
        {
            using (IModel model = _connection.CreateModel())
            {
                model.QueueUnbind(queueName, exchangeName, routingKey, null);

                model.Close(200, "ok");
            }
        }

		// Binds Exchange to Exchange 
        public void BindExchange(string destination, string source, string exchangeType, string routingKey)
        {
            using (IModel model = _connection.CreateModel())
            {
                model.ExchangeDeclare(destination, exchangeType, true, false, null);
                model.ExchangeDeclare(source, exchangeType, true, false, null);

                model.ExchangeBind(destination, source, routingKey);
				

                model.Close(200, "ok");
            }
        }

        public void UnbindExchange(string destination, string source, string routingKey)
        {
            using (IModel model = _connection.CreateModel())
            {
                model.ExchangeUnbind(destination, source, routingKey, null);

                model.Close(200, "ok");
            }
        }

        public void Purge(string queueName)
        {
            using (IModel model = _connection.CreateModel())
            {
                try
                {
                    model.QueueDeclarePassive(queueName);
                    model.QueuePurge(queueName);
                }
                catch
                {
                }

                model.Close(200, "purged queue");
            }
        }

		public void Delete(string queueName)
		{
			using (IModel model = _connection.CreateModel())
			{
				try
				{
					model.QueueDelete(queueName);
					model.ExchangeDelete(queueName);
				}
				catch
				{
				}

				model.Close(200, "Deleted queue");
			}
		}

		public void DeleteExchange(string exchangeName)
		{
			using (IModel model = _connection.CreateModel())
			{
				try
				{
					model.ExchangeDelete(exchangeName);
				}
				catch
				{
				}

				model.Close(200, "Deleted Exchange");
			}
		}

        public void Dispose()
        {
            if (_disposed)
                return;

			_connection.Close();
            _connection = null;

            _disposed = true;
        }
    }
}