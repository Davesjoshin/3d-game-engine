using System;
using System.Threading;
using Veldrid;
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

            // Create the window
            Sdl2Window window = VeldridStartup.CreateWindow(ref windowCreateInfo);

            // Graphics device config
            GraphicsDeviceOptions options = new GraphicsDeviceOptions
            {
                Debug = true,
                SwapchainDepthFormat = PixelFormat.R16_UNorm,
                SyncToVerticalBlank = true
            };

            GraphicsDevice graphicsDevice = 
                VeldridStartup.CreateGraphicsDevice(
                    window, 
                    options,
                    GraphicsBackend.Metal
                );

            // Basic event loop.
            while (window.Exists)
            {
                window.PumpEvents();
                Thread.Sleep(16);
            }

            graphicsDevice.Dispose();
        }
    }
}