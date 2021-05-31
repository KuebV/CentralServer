using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer
{
    public class ColoredConsole
    {

        public static void Write(string Text, ConsoleColor? TextColor = null)
        {
            if (Text == null) return;
            if (TextColor != null)
            {
                Console.ForegroundColor = TextColor.Value;
            }

            Console.Write(Text);
        }

        public static void WriteLine(string Text, ConsoleColor? TextColor = null)
        {
            if (Text == null) return;
            if (TextColor != null)
            {
                Console.ForegroundColor = TextColor.Value;
            }

            Console.WriteLine(Text);
        }
    }
}
