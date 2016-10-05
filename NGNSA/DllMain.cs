using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using WebSocketSharp;
using Memory;
using static Newtonsoft.Json.JsonConvert;
using System.Security.Cryptography;
using Binarysharp.MemoryManagement;
using NGNSA.Forms;
using WebSocketSharp.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace NGNSA
{
    public class Startup
    {
        static readonly string[] blacklist = new string[] { "olly", "engine", "immunity", "calc" };
        static List<int> killedProcesses = new List<int>();
        static WebSocket ws;
        static string sessionToken = "";
        static Thread pingThread;
        static MemorySharp memsharp;
        static ConsoleControl.ConsoleControl console;

        static int EntryPoint(string pwzArgument)
        {            
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler;
            var regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\NoGameNoLife\NoGameNoStory");
            regKey.DeleteValue("anticheat");
            regKey.Close();
            try
            {
                memsharp = new MemorySharp(Process.GetCurrentProcess());
                new Thread(() =>
                {
                    //MessageBox.Show("Good morning infidels!");
                    new Thread(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                foreach (Process process in Process.GetProcesses())
                                {
                                    if(blacklist.Any(entry => process.MainWindowTitle.ToLower().Contains(entry) || 
                                                              process.ProcessName.ToLower().Contains(entry) ))
                                    {
                                        process.Crash();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            Thread.Sleep(1000);
                        }
                    }).Start();
                    ws = new WebSocket("wss://gs.nogameno.life:20876");
                    ws.OnMessage += MessageHandler;
                    ws.OnOpen += OnOpenHandler;
                    ws.OnClose += OnCloseHandler;
                    ws.OnError += OnErrorHandler;
                    ClientSslConfiguration configuration = new ClientSslConfiguration("gs.nogameno.life");
                    configuration.CheckCertificateRevocation = false;
                    configuration.ServerCertificateValidationCallback += CertificateValidationCallback;
                    ws.SslConfiguration = configuration;
                    ws.Connect();
                }).Start();
            }
            catch (Exception ex)
            {
                writeToConsole(ex.Message + " - " + ex.Source);
            }
            return 1;
        }

        private static bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

        private static void writeToConsole(String str)
        {
            if(console != null)
            {
                console.WriteOutput(str+"\r\n", System.Drawing.Color.White);
                console.InternalRichTextBox.ScrollToCaret();
            }
        }
        #region Handlers
        private static void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            writeToConsole($"{ex.Message} - {ex.TargetSite.Name}");
        }

        private static void OnErrorHandler(object sender, ErrorEventArgs e)
        {
            var exception = e.Exception;
            writeToConsole($"{exception.Message} - {exception.Source}");
        }

        private static void OnCloseHandler(object sender, CloseEventArgs e)
        {
            writeToConsole(e.Reason);
        }

        private static void OnOpenHandler(object sender, EventArgs e)
        {
            if(pingThread != null && pingThread.IsAlive)
            {
                pingThread.Abort();
            }
            pingThread = new Thread(() => {
                while(true)
                {
                    ws.Send(new Message("ping", sessionToken));
                    Thread.Sleep(new Random().Next(3000,10000));
                }
            });
            pingThread.Start();
        }
        #endregion
        private static int getCID()
        {
            try
            {
                int cid = memsharp.Read<int>(new IntPtr(0x00BF3CC0));
                return cid;
            }
            catch (Exception) { return 0; }

        }

        private static void MessageHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var msg = DeserializeObject<Message>(e.Data);
                switch (msg.type)
                {
                    case "hello": {
                            sessionToken = msg.sessionToken;
                            break;
                        }
                    case "change_token": {
                            sessionToken = msg.sessionToken;
                            break;
                        }
                    case "auth_request": {
                            ws.Send(new AuthMessage(getCID(), sessionToken != null ? sessionToken : ""));
                        break;
                        }
                    case "asm_push": {
                            writeToConsole(e.Data);
                            ASMMessage asmreq = DeserializeObject<ASMMessage>(e.Data);
                            memsharp.Write<byte>((IntPtr)Convert.ToUInt64(asmreq.address), asmreq.opcodes);
                            writeToConsole($"ASM recv'd, address:{String.Format("0x{0:X}",Convert.ToUInt64(asmreq.address))} - opcodes: {String.Join(", ",asmreq.opcodes.Select(b => String.Format("0x{0:X}", b)).ToArray())}");
                            break;
                        }
                    case "pong": {
                        break;
                        }
                    default:
                        {
                            writeToConsole($"Got {msg.type}!");
                            break;
                        }
                }
            } catch(Exception ex) {
                writeToConsole(ex.ToString());
            }
        }
    }
    #region Message classes
    public class Message
    {
        public string type;
        public long timestamp;
        public string sessionToken;
        public string randomPayload;

        public Message(string type, string tick)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long epoch = (long)t.TotalSeconds;
            this.type = type;
            this.timestamp = epoch;
            this.sessionToken = tick;
            this.randomPayload = RandomStringGenerator.Generate(64, 256);
        }

        internal string toJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
    public class ASMMessage : Message
    {
        public string address;
        public byte[] opcodes;
        public ASMMessage(string sessionToken, string address, params byte[] opcodes) : base("asm_request", sessionToken)
        {
            this.address = address;
            this.opcodes = opcodes;
        }
    }
    public class AuthMessage : Message
    {
        public int cid;
        public AuthMessage(int cid, string sessionToken): base("auth_response", sessionToken)
        {
            this.cid = cid;
            this.sessionToken = sessionToken;
        }
    }
    #endregion
    public class Hack
    {
        private UIntPtr _address;
        private byte[] _opcodes;
        private int _size;
        public Hack(UIntPtr address, params byte[] opcodes)
        {
            this.SetAddress(address);
            this.SetOpcodes(opcodes);
        }
        public Hack(int address, params byte[] opcodes)
        {
            this.SetAddress((UIntPtr)address);
            this.SetOpcodes(opcodes);
        }
        public void SetAddress(UIntPtr address)
        {
            this._address = address;
        }
        public void SetOpcodes(byte[] opcodes)
        {
            this._opcodes = opcodes;
            this._size = opcodes.Length;
        }
        public UIntPtr GetAddress()
        {
            return this._address;
        }
        public byte[] GetOpcodes()
        {
            return this._opcodes;
        }
        public int GetSize()
        {
            return this._size;
        }
    }
    public static class Extensions
    {
        public static void Crash(this Process process)
        {
            try
            {
                Mem mem = new Mem();
                if (mem.OpenGameProcess(process.Id))
                {
                    byte[] corrupting = new byte[65536];
                    var random = new Random();
                    random.NextBytes(corrupting);
                    UIntPtr address = (UIntPtr)process.MainModule.EntryPointAddress.ToInt64();
                    mem.writeByte(new Hack(address, corrupting));
                }
            }
            catch (Exception)
            {
                try { process.Kill(); } catch (Exception) { }
            }
        }
        public static void Send(this WebSocket ws, Message msg)
        {
            ws.Send(msg.toJSON());
        }
    }
}
