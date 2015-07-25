using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace TestLibdl
{
	public static class PlatformApis
	{
		public static class LinuxApis
		{

			[DllImport("libdl-debian-32-bit")]
			public static extern IntPtr dlopen(String fileName, int flags);

        		public static void Main()
        		{
				dlopen("libuv.so.1", 2);
                		Console.WriteLine("open :)");

        		}	
		}
	}
}
