using System;

namespace TrelloExcelAddIn
{
	public interface IMessageBus
	{
		void Subscribe<TMessage>(Action<TMessage> handler);
		void Unsubscribe<TMessage>(Action<TMessage> handler);
		void Publish<TMessage>(TMessage message);
		void Publish(Object message);
	}
}
