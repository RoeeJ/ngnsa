using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Memory;
using Microsoft.Win32;
using WebSocket4Net;
using ngnlauncher.Properties;
// ReSharper disable LoopCanBeConvertedToQuery

// ReSharper disable UnusedParameter.Local

namespace ngnlauncher
{
    internal class Program
    {
         static readonly byte[] Key = Resources.key;
         static readonly byte[] Iv = Resources.IV;
         static readonly int ShuffleKey = Key[3] + Iv[1] + Key[4] + Key[1] + Iv[5];
         static readonly string clientPath = Directory.GetCurrentDirectory() + "/client.exe";
         const FileAttributes attributes = FileAttributes.ReadOnly | FileAttributes.System | FileAttributes.Hidden | FileAttributes.NotContentIndexed | FileAttributes.Temporary;
         static List<string> tempFiles = new List<string>();
        static void Main(string[] args)
         {
             try
             {

                 Application.ApplicationExit += handleShutdown;
                 var clientBytes = GetClientBytes();
                 var tempPath = IsUsingExternalClient() ? clientPath : Directory.GetCurrentDirectory() + "/" +
                        Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16) + ".exe";
                 if (!IsUsingExternalClient())
                 {
                     File.WriteAllBytes(tempPath, clientBytes.DeShuffle(ShuffleKey).Decrypt(Key, Iv).DeShuffle(ShuffleKey));
                     File.SetAttributes(tempPath, attributes);
                 }
                 var ngnsaBytes = Properties.Resources.NGNSA;
                 var bootstrapBytes = Properties.Resources.bootstrap;
                 var ngnsaPath = String.Format(@"{0}\{1}", Path.GetTempPath(), Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16) + ".dll");
                 var bootstrapPath = String.Format(@"{0}\{1}", Path.GetTempPath(), Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16) + ".dll");
                 tempFiles.Add(ngnsaPath);
                 File.WriteAllBytes(ngnsaPath, ngnsaBytes);
                 File.WriteAllBytes(bootstrapPath, bootstrapBytes);
                 File.SetAttributes(ngnsaPath, attributes);
                 File.SetAttributes(bootstrapPath, attributes);
                 var regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\NoGameNoLife\NoGameNoStory");
                 regKey.SetValue("anticheat", ngnsaPath);
                 regKey.Close();
                 var psi = new ProcessStartInfo
                 {
                     Arguments = "",
                     FileName = tempPath,
                     WorkingDirectory = Directory.GetCurrentDirectory()
                 };
                 using (var proc = Process.Start(psi))
                 {
                     var filename = Path.GetFileNameWithoutExtension(tempPath);
                     var memlib = new Mem();
                     var procid = proc.Id;
                     if (procid != 0) {
                         if (memlib.OpenGameProcess(procid))
                         {
                            try
                            {
                                Memedit(memlib);
                            } catch(Exception ex) {
                                MessageBox.Show($"{ex.Message} - {ex.Source}");
                            }
                         }
                        DllInjector injector = DllInjector.GetInstance;
                        var injectionResult = injector.Inject(proc.ProcessName, bootstrapPath);
                        if (injectionResult != DllInjectionResult.Success)
                        {
                            proc.Kill();
                            Process.GetCurrentProcess().Kill();
                        }
                     } else {
                         proc.Kill();
                     }
                     proc.WaitForExit();
                     if (!IsUsingExternalClient())
                     {
                         File.SetAttributes(tempPath, FileAttributes.Normal);
                         File.Delete(tempPath);
                     }
                    tempFiles.ForEach((file) =>
                    {
                        if (File.Exists(file))
                        {
                            try
                            {
                                new FileInfo(file).IsReadOnly = false;
                                File.SetAttributes(file, FileAttributes.Normal);
                                File.Delete(file);
                            }
                            catch (Exception) { }
                        }
                    });
                    Process.GetCurrentProcess().Kill();
                 }
             }
             catch (Exception ex)
             {
                tempFiles.ForEach(file =>
                {
                    if (File.Exists(file))
                    {
                        try {
                            new FileInfo(file).IsReadOnly = false;
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                        } catch (Exception) { }
                    }
                });
                 MessageBox.Show(ex.Message);
                 MessageBox.Show(ex.Source);
             }
        }

         static void handleShutdown(object sender, EventArgs e)
         {


             
         }

        static void HandleWSocketMessage(object sender, MessageReceivedEventArgs e)
        {

        }

         static byte[] GetClientBytes()
        {
            return File.Exists(clientPath) ? File.ReadAllBytes(clientPath) : Properties.Resources.client;
        }
         static bool IsUsingExternalClient()
        {
            return File.Exists(clientPath);
        }
         static byte[] GetEditsBytes()
        {
            var editsPath = Directory.GetCurrentDirectory() + "/edits.txt";
            return File.Exists(editsPath) ? File.ReadAllBytes(editsPath) : Resources.edits.DeShuffle(ShuffleKey).Decrypt(Key, Iv).DeShuffle(ShuffleKey);
        }
         static void Memedit(Mem memlib)
        {
            var edits = Encoding.ASCII.GetString(GetEditsBytes()).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var hacks = edits.Select(str => str.Split(',')).Select(edit =>
            {
                Hack hack = null;
                try
                {
                    var address = (UIntPtr)Convert.ToUInt64(edit[0], 16);
                    hack = new Hack(address, edit.Skip(1).Select(opcode => Convert.ToByte(opcode, 16)).ToArray());
                }
                catch (Exception) { }
                return hack;
            }).Where(hack => hack != null);
            foreach (var hack in hacks)
            {
                memlib.writeByte(hack);
            }
        }
    }

    public static class ProcessExtension
    {
        [Flags]
        public enum ThreadAccess
        {
            Terminate = 0x0001,
            SuspendResume = 0x0002,
            GetContext = 0x0008,
            SetContext = 0x0010,
            SetInformation = 0x0020,
            QueryInformation = 0x0040,
            SetThreadToken = 0x0080,
            Impersonate = 0x0100,
            DirectImpersonation = 0x0200
        }

        [DllImport("kernel32.dll")]
         static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
         static extern uint SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
         static extern int ResumeThread(IntPtr hThread);

        public static void Suspend(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SuspendResume, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                SuspendThread(pOpenThread);
            }
        }

        public static void Resume(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SuspendResume, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                ResumeThread(pOpenThread);
            }
        }
        public static bool IsWin64Emulator(this Process process)
        {
            if ((Environment.OSVersion.Version.Major > 5)
                || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)))
            {
                bool retVal;

                return NativeMethods.IsWow64Process(process.Handle, out retVal) && retVal;
            }

            return false; // not on 64-bit Windows Emulator
        }
        internal static class NativeMethods
        {
            [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
        }
    }

    public static class Extensions
    {
        public static IEnumerable<TU> Map<T, TU>(this IEnumerable<T> s, Func<T, TU> f)
        {
            foreach (var item in s)
                yield return f(item);
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            var result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static byte[] GetBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(this byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        //Decrypt byte[]
        public static byte[] Decrypt(this byte[] data, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            var alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            var cs = new CryptoStream(ms,
              alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.Close();
            var decryptedData = ms.ToArray();
            return decryptedData;
        }

        //Encrypt byte[]
        public static byte[] Encrypt(this byte[] data, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            var alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            var cs = new CryptoStream(ms,
              alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.Close();
            var encryptedData = ms.ToArray();
            return encryptedData;
        }

        public static int[] GetShuffleExchanges(int size, int key)
        {
            var exchanges = new int[size - 1];
            var rand = new Random(key);
            for (var i = size - 1; i > 0; i--)
            {
                var n = rand.Next(i + 1);
                exchanges[size - 1 - i] = n;
            }
            return exchanges;
        }

        public static byte[] Shuffle(this byte[] toShuffle, int key)
        {
            var size = toShuffle.Length;
            var exchanges = GetShuffleExchanges(size, key);
            for (var i = size - 1; i > 0; i--)
            {
                var n = exchanges[size - 1 - i];
                var tmp = toShuffle[i];
                toShuffle[i] = toShuffle[n];
                toShuffle[n] = tmp;
            }
            return toShuffle;
        }

        public static byte[] DeShuffle(this byte[] shuffled, int key)
        {
            var size = shuffled.Length;
            var exchanges = GetShuffleExchanges(size, key);
            for (var i = 1; i < size; i++)
            {
                var n = exchanges[size - i - 1];
                var tmp = shuffled[i];
                shuffled[i] = shuffled[n];
                shuffled[n] = tmp;
            }
            return shuffled;
        }
    }
}
