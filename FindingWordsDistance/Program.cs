using System.Linq;

namespace FindingWordsDistance
{
	class Program
	{
		static void Main(string[] args)
		{
			Executor.Execute(args[0], args.Skip(1).Take(2).ToArray());
		}
	}
}
