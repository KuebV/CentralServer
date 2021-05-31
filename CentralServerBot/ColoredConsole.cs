using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServerBot
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
