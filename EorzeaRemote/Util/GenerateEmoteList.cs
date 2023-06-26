using Lumina.Excel.GeneratedSheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EorzeaRemote.Util
{
    internal static class GenerateEmoteList
    {
        public static void Generate()
        {
            var emoteNames = new List<string>();

            var lumina = new Lumina.GameData("C:/Program Files (x86)/Steam/steamapps/common/FINAL FANTASY XIV Online/game/sqpack");
            var emotes = lumina.GetExcelSheet<Emote>();
            if (emotes != null)
            {
                for (uint i = 0; i < emotes.RowCount; i++)
                {
                    var e = emotes.GetRow(i);
                    if (e != null)
                    {
                        if (e.Name.ToString() != string.Empty)
                        {
                            var command = e.TextCommand.Value;
                            if (command != null)
                            {
                                emoteNames.Add($"\"{command.Command.ToString()[1..]}\"");
                                if (command.ShortCommand.RawString != string.Empty)
                                {
                                    emoteNames.Add($"\"{command.ShortCommand.ToString()[1..]}\"");
                                }
                            }
                        }
                    }
                }
            }

            emoteNames.Sort();

            foreach (string na in emoteNames)
            {
                Console.WriteLine(na);
            }
            Console.WriteLine(emoteNames.Count);

            File.WriteAllLines("Here.txt", emoteNames);
        }
    }
}
