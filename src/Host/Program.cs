using System;
using System.Threading;
using Veldrid.StartupUtilities;
using Veldrid.Sdl2;


namespace Host
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Simple native window.
            WindowCreateInfo windowCreateInfo = new WindowCreateInfo
            {
                X = 100,
                Y = 100,
                WindowWidth = 1280,
                WindowHeight = 720,
                WindowTitle = "3DGE"
            };

            Sdl2Window window = VeldridStartup.CreateWindow(ref windowCreateInfo);

            // Basic event loop.
            while (window.Exists)
            {
                window.PumpEvents();
                Thread.Sleep(16);
            }
        }
    }
}