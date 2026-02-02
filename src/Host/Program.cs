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

            // Command list
            CommandList commandList = graphicsDevice.ResourceFactory.CreateCommandList();

            // Basic event loop.
            while (window.Exists)
            {
                // Process events
                window.PumpEvents();

                // Begin recording GPU commands.
                commandList.Begin();

                // Set the framebuffer
                commandList.SetFramebuffer(graphicsDevice.SwapchainFramebuffer);

                // Clear the framebuffer
                commandList.ClearColorTarget(0, RgbaFloat.CornflowerBlue);

                // End recording
                commandList.End();

                // Submit the command list
                graphicsDevice.SubmitCommands(commandList);

                // Present the frame
                graphicsDevice.SwapBuffers();

                Thread.Sleep(16);
            }

            // Clean up
            commandList.Dispose();
            graphicsDevice.Dispose();
        }
    }
}