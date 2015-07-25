# Mono interop - Debian 32-bit

I think this may be an issue in mono (any versions) running on Debian 32-bit.

The following C# program fails:

```
$ sudo mcs use-libdl.cs
$ sudo mono use-libdl.exe
```

```
Stacktrace:

  at <unknown> <0xffffffff>
  at (wrapper managed-to-native) TestLibdl.PlatformApis/LinuxApis.dlopen (string,int) <0xffffffff>
  at TestLibdl.PlatformApis/LinuxApis.Main () <0x0001b>
  at (wrapper runtime-invoke) object.runtime_invoke_void (object,intptr,intptr,intptr) <0xffffffff>

Native stacktrace:

	mono() [0x81027e3]
	mono() [0x814ea14]
	mono() [0x806d977]
	linux-gate.so.1(__kernel_rt_sigreturn+0) [0xb77ce40c]
	/lib/ld-linux.so.2(+0xf0f6) [0xb77de0f6]
	/lib/ld-linux.so.2(+0x12245) [0xb77e1245]
	/usr/lib/i386-linux-gnu/libdl.so(+0xcbc) [0xb71b4cbc]
	/lib/ld-linux.so.2(+0xe716) [0xb77dd716]
	/usr/lib/i386-linux-gnu/libdl.so(+0x136c) [0xb71b536c]
	/usr/lib/i386-linux-gnu/libdl.so(dlopen+0x41) [0xb71b4d71]
	[0xb7392f8c]
	[0xb7392e34]
	[0xb7392ef7]
	mono() [0x8072101]

Debug info from gdb:


=================================================================
Got a SIGSEGV while executing native code. This usually indicates
a fatal error in the mono runtime or one of the native libraries 
used by your application.
=================================================================

Aborted
```

Create the 

```
gcc -Wall -fPIC -c libdl-debian-32-bit.c 
gcc -shared -o libdl-debian-32-bit.so libdl-debian-32-bit.o
sudo cp libdl-debian-32-bit.so /lib
```

The following C# program works:

```
$ sudo mcs use-libdl.cs
$ sudo mono use-libdl.exe
```


I said "it worked" because it didn't crash when opening libuv.
I guess I have to find out how to test it  in Microsoft.AspNet.Server.Kestrel/Networking/PlatformApis.cs

Any help will be appreciated. I am a newbie and I have been enjoying Debian. 

## Source

https://github.com/aspnet/KestrelHttpServer/issues/78

https://bugzilla.xamarin.com/show_bug.cgi?id=4190

http://www.mono-project.com/docs/advanced/pinvoke/#troubleshooting
