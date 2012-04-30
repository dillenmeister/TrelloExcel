using System;
using System.Collections.Generic;

namespace TrelloExcelAddIn
{
	public sealed class MessageBus : IMessageBus
	{
		private readonly Dictionary<Type, List<Object>> subscribers = new Dictionary<Type, List<Object>>();

		public void Subscribe<TMessage>(Action<TMessage> handler)
		{
			if (subscribers.ContainsKey(typeof(TMessage)))
			{
				var handlers = subscribers[typeof(TMessage)];
				handlers.Add(handler);
			}
			else
			{
				var handlers = new List<Object> { handler };
				subscribers[typeof(TMessage)] = handlers;
			}
		}

		public void Unsubscribe<TMessage>(Action<TMessage> handler)
		{
			if (subscribers.ContainsKey(typeof(TMessage)))
			{
				var handlers = subscribers[typeof(TMessage)];
				handlers.Remove(handler);

				if (handlers.Count == 0)
				{
					subscribers.Remove(typeof(TMessage));
				}
			}
		}

		public void Publish<TMessage>(TMessage message)
		{
			if (subscribers.ContainsKey(typeof(TMessage)))
			{
				var handlers = subscribers[typeof(TMessage)];
				foreach (Action<TMessage> handler in handlers)
				{
					handler.Invoke(message);
				}
			}
		}

		public void Publish(Object message)
		{
			var messageType = message.GetType();
			if (subscribers.ContainsKey(messageType))
			{
				var handlers = subscribers[messageType];
				foreach (var handler in handlers)
				{
					var actionType = handler.GetType();
					var invoke = actionType.GetMethod("Invoke", new[] { messageType });
					invoke.Invoke(handler, new[] { message });
				}
			}
		}
	}
}
