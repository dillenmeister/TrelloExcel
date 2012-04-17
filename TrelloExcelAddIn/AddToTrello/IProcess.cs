using System.Diagnostics;

namespace TrelloExcelAddIn
{
	public interface IProcess
	{
		void Start(string fileName);
	}

	internal class ProcessImpl : IProcess
	{
		public void Start(string fileName)
		{
			Process.Start(fileName);
		}
	}
}