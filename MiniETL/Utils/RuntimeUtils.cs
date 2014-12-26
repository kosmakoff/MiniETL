namespace MiniETL.Utils
{
	public static class RuntimeUtils
	{
		public static bool IsDebug
		{
#if DEBUG
			get { return true; }
#else
			get { return false; }
#endif
		}
	}
}
