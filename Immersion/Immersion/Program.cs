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
            using (ImmersionGame game = new ImmersionGame(map))
            {
                game.Run();
            }
        }
    }
#endif
}

