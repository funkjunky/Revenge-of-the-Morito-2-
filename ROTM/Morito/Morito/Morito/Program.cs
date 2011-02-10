
namespace Morito
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MoritoFighterGame MFgame = new MoritoFighterGame())
            {
                MFgame.Run();
            }
        }
    }
}

