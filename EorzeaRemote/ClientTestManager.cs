using EorzeaRemote.Data;
using Riptide.Utils;
using Riptide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EorzeaRemote
{
    internal class ClientTestManager
    {
        private static readonly Client Client = new();

        public ClientTestManager()
        {
            RiptideLogger.Initialize(Console.WriteLine, true);

            var updateLoop = new System.Timers.Timer
            {
                Interval = 60,
                AutoReset = true
            };
            updateLoop.Elapsed += Update;
            updateLoop.Enabled = true;

            Client.Connect("127.0.0.1:7777");

            var zero = new System.Timers.Timer
            {
                Interval = 1000,
                AutoReset = true
            };
            zero.Elapsed += Zero;
            zero.AutoReset = false;
            zero.Enabled = true;

            var r = Message.Create(MessageSendMode.Reliable, (ushort)MessageSendType.RegisterName);
            r.AddString("My Name!");

            Client.Send(r);
        }

        private void Zero(object? sender, ElapsedEventArgs e)
        {
            var r = Message.Create(MessageSendMode.Reliable, (ushort)MessageSendType.RegisterName);
            r.AddString("My Name!");

            Client.Send(r);
        }

        private void Update(object? sender, ElapsedEventArgs e)
        {
            Client.Update();
        }
    }
}
