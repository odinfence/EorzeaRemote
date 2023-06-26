using EorzeaRemote.Data;
using Lumina.Excel.GeneratedSheets;
using Riptide;
using System;
using System.Timers;

namespace EorzeaRemote
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Eorzea Remote";
            Console.Clear();

            var NetworkManager = new NetworkManager();
            var ClientTestManager = new ClientTestManager();
            
            Console.ReadLine();
        }
    }
}