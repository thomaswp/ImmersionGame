using System;

namespace Immersion
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            String map = args.Length == 0 ? null : args[0];
            using (Game1 game = new Game1(map))
            {
                game.Run();
            }
        }
    }
#endif
}

