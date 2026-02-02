using System;
using System.Numerics;
using System.Threading;
using Veldrid;
using Veldrid.StartupUtilities;
using Veldrid.Sdl2;
using Veldrid.SPIRV;


namespace Host
{
    internal static class Program
    {
        struct Vertex
        {
            public Vector2 Position;

            public Vertex(Vector2 position)
            {
                Position = position;
            }
        }

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
                SwapchainDepthFormat = PixelFormat.D24_UNorm_S8_UInt,
                SyncToVerticalBlank = true
            };

            GraphicsDevice graphicsDevice = 
                VeldridStartup.CreateGraphicsDevice(
                    window, 
                    options,
                    GraphicsBackend.Metal
                );

            // Resource factory
            ResourceFactory factory = graphicsDevice.ResourceFactory;

            Vertex[] vertices =
            {
                new Vertex(new Vector2(0.0f, 0.5f)),
                new Vertex(new Vector2(0.5f, -0.5f)),
                new Vertex(new Vector2(-0.5f, -0.5f)),
            };

            // Vertex buffer
            // This is a buffer that contains the vertex data
            DeviceBuffer vertexBuffer = factory.CreateBuffer(
                new BufferDescription(
                    (uint)(vertices.Length * sizeof(float) * 2),
                    BufferUsage.VertexBuffer
                )
            );

            // Update the buffer, this is where the vertex data is stored
            graphicsDevice.UpdateBuffer(
                vertexBuffer,
                0,
                vertices
            );
        
            // Shaders
            Shader[] shaders = factory.CreateFromSpirv(
                new ShaderDescription(
                    ShaderStages.Vertex,
                    File.ReadAllBytes("Shaders/triangle.vert"),
                    "main"
                ),    
                new ShaderDescription(
                    ShaderStages.Fragment,
                    File.ReadAllBytes("Shaders/triangle.frag"),
                    "main"      
                )
            );

            // Depth Resources
            Texture depthTexture = factory.CreateTexture(
                TextureDescription.Texture2D(
                    (uint)window.Width,
                    (uint)window.Height,
                    mipLevels: 1,
                    arrayLayers: 1,
                    PixelFormat.D24_UNorm_S8_UInt,
                    TextureUsage.DepthStencil
                )
            );

            // Framebuffer
            Framebuffer framebuffer = factory.CreateFramebuffer(
                new FramebufferDescription(
                    depthTexture,
                    graphicsDevice.SwapchainFramebuffer.ColorTargets[0].Target
                )
            );

            GraphicsPipelineDescription pipelineDesc = new GraphicsPipelineDescription
            {
                BlendState = BlendStateDescription.SingleOverrideBlend, // No blending
                DepthStencilState = DepthStencilStateDescription.DepthOnlyLessEqual,
                RasterizerState = RasterizerStateDescription.Default, // Default rasterizer
                PrimitiveTopology = PrimitiveTopology.TriangleList, // Default primitive topology
                ResourceLayouts = Array.Empty<ResourceLayout>(), // No resource layouts
                ShaderSet = new ShaderSetDescription( 
                    new[]
                    {
                        new VertexLayoutDescription(
                            new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2)
                        )  
                    },
                    shaders),
                Outputs = graphicsDevice.SwapchainFramebuffer.OutputDescription

            };

            Pipeline pipeline = factory.CreateGraphicsPipeline(pipelineDesc);

            // Command list
            CommandList commandList = graphicsDevice.ResourceFactory.CreateCommandList();

            // Window Resize
            bool resized = false;
            window.Resized += () => resized = true;

            // Basic Render Loop
            while (window.Exists)
            {
                // Adjust for window resize
                if (resized)
                {
                    resized = false;

                    graphicsDevice.ResizeMainWindow(
                        (uint)window.Width,
                        (uint)window.Height
                    );

                    depthTexture.Dispose();
                    framebuffer.Dispose();

                    depthTexture = factory.CreateTexture(
                        TextureDescription.Texture2D(
                            (uint)window.Width,
                            (uint)window.Height,
                            mipLevels: 1,
                            arrayLayers: 1,
                            PixelFormat.D24_UNorm_S8_UInt,
                            TextureUsage.DepthStencil
                        )
                    );

                    framebuffer = factory.CreateFramebuffer(
                        new FramebufferDescription(
                            depthTexture,
                            graphicsDevice.SwapchainFramebuffer.ColorTargets[0].Target
                        )
                    );
                    
                    pipeline.Dispose();
                    pipelineDesc.Outputs = graphicsDevice.SwapchainFramebuffer.OutputDescription;
                    pipeline = factory.CreateGraphicsPipeline(pipelineDesc);

                }

                // Process events
                InputSnapshot input = window.PumpEvents();

                // Toggle fullscreen
                foreach (var keyEvent in input.KeyEvents)
                {
                    if (keyEvent.Down && keyEvent.Key == Key.F11)
                    {
                        window.WindowState =
                            window.WindowState == WindowState.FullScreen
                                ? WindowState.Normal
                                : WindowState.FullScreen;
                    }
                }

                // Begin recording GPU commands.
                commandList.Begin();
                commandList.SetFramebuffer(graphicsDevice.SwapchainFramebuffer);
                commandList.ClearColorTarget(0, RgbaFloat.CornflowerBlue); // Clear the framebuffer
                commandList.ClearDepthStencil(1f);

                commandList.SetPipeline(pipeline);
                commandList.SetVertexBuffer(0, vertexBuffer);
                commandList.Draw(3);

                commandList.End(); // End recording

                graphicsDevice.SubmitCommands(commandList); // Submit the command list
                graphicsDevice.SwapBuffers(); // Present the frame

                Thread.Sleep(16);
            }

            // Clean up
            commandList.Dispose();
            framebuffer.Dispose();
            pipeline.Dispose();
            depthTexture.Dispose();
            graphicsDevice.Dispose();
        }
    }
}