using System;
using System.Collections.Generic;
using System.IO;
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
					sbc.Subscribe(s =>
						{
							s.Consumer<ConsoleLoggerForMessages>();
							s.Consumer<RawLogger>();
						});
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

	class RawLogger
		: Consumes<IConsumeContext<ChatMessage>>.All
	{
		public void Consume(IConsumeContext<ChatMessage> context)
		{
			using (var ms = new MemoryStream())
			{
				context.BaseContext.CopyBodyTo(ms);
				var msg = Encoding.UTF8.GetString(ms.ToArray());
				Console.WriteLine(string.Format("{0} body:\n {1}", DateTime.UtcNow, msg));
			}
		}
	}
}
