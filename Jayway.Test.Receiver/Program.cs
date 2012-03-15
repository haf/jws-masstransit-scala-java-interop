using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jayway.Test.Messages;
using MassTransit;

namespace Jayway.Test.Receiver
{
	class Program
	{
		static void Main(string[] args)
		{
			var sb = ServiceBusFactory.New(sbc =>
				{
					sbc.ReceiveFrom("rabbitmq://isomorphism/Jayway.Test.Receiver");
					sbc.UseRabbitMqRouting();
					sbc.Subscribe(s => s.Consumer<ConsoleLoggerForMessages>());
				});

			Console.WriteLine("press a key to exit");
			Console.ReadKey(true);

			sb.Dispose();
		}
	}

	class ConsoleLoggerForMessages
		: Consumes<ChatMessage>.All
	{
		public void Consume(ChatMessage message)
		{
			Console.WriteLine("{0}: {1}", message.SeqId, message.Spoken);
		}
	}
}
