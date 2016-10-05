using System;
using static Newtonsoft.Json.JsonConvert;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using System.Windows.Forms;

namespace WSTester
{
    class Program
    {
        static WebSocket ws;
        static void Main(string[] args)
        {
            ws = new WebSocket("ws://10.211.55.2:8080");
            ws.MessageReceived += MessageHandler;
            ws.Opened += OnStartHandler;
            ws.Closed += OnCloseHandler;
            ws.Error += OnErrorHandler;
            ws.Open();
            Console.ReadLine();
            ws.Close();
        }

        private static void OnCloseHandler(object sender, EventArgs e)
        {
            ws.Open();
        }

        private static void OnErrorHandler(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private static void OnStartHandler(object sender, EventArgs e)
        {
            ws.Send(new Message("client_start"));
            ws.Send(new ASMMessage("0xdeadbeef",0x1,0x2,0x3,0x4,0x5));
        }

        private static void MessageHandler(object sender, MessageReceivedEventArgs e)
        {
            var msg = DeserializeObject<Message>(e.Message);
            switch (msg.type)
            {
                case "asm_request_ack" :
                    ASMMessage asmreq = DeserializeObject<ASMMessage>(e.Message);
                    Console.WriteLine($"ASM recv'd, address:{asmreq.address} - opcodes {BitConverter.ToString(asmreq.opcodes).Replace('-',',')}");
                   break;
                default:
                    Console.WriteLine($"Got {msg.type}!");
                    break;
            }
        }
    }
    public static class WSExtensions
    {
        public static void Send(this WebSocket ws, Message msg)
        {
            ws.Send(msg.toJSON());
        }
    }
    public class Message
    {
        public string type;
        public long timestamp;

        public Message(string type)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long epoch = (long)t.TotalSeconds;
            this.type = type;
            this.timestamp = epoch;
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
        public ASMMessage(string address, params byte[] opcodes) : base("asm_request")
        {
            this.address = address;
            this.opcodes = opcodes;
        }
    }
}
