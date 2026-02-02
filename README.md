# 3DGE - 3d Game Engine

C# - https://learn.microsoft.com/en-us/dotnet/csharp/

Veldrid - https://veldrid.dev/articles/intro.html

## TODO List
- Create a native application window.
- Initialize the graphics device.
- Create and manage the swapchain.
- clear screen
- triangle
- camera
- depth buffer
- mesh + transform
- ECS
- Render graph
- Asset loading
- lighting
- materials
- debug


## Directory Trees

/src/Engine
│
├─ Engine.csproj
│
├─ Core
│   ├─ EngineContext.cs
│   ├─ EngineTime.cs
│   └─ EngineConfig.cs
│
├─ Graphics
│   ├─ Renderer.cs
│   ├─ GraphicsDeviceManager.cs
│   ├─ SwapchainContext.cs
│   └─ RenderFrame.cs
│
├─ ECS
│   ├─ World.cs
│   ├─ Entity.cs
│   ├─ Systems
│   └─ Components
│
├─ Assets
│   ├─ AssetManager.cs
│   ├─ AssetHandle.cs
│   └─ Loaders
│
├─ Math
│   ├─ Transform.cs
│   ├─ Camera.cs
│   └─ BoundingBox.cs
│
├─ Platform
│   ├─ Window.cs
│   └─ Input.cs
│
└─ Utils
    ├─ Log.cs
    └─ DisposablePool.cs


/src/Game
│
├─ Game.csproj
│
├─ GameApp.cs
│
├─ Systems
│   ├─ PlayerSystem.cs
│   └─ MovementSystem.cs
│
├─ Components
│   ├─ PlayerComponent.cs
│   └─ HealthComponent.cs
│
├─ Scenes
│   └─ TestScene.cs
│
└─ GameConfig.cs


/src/Host
│
├─ Host.csproj
│
├─ Program.cs
│
└─ PlatformHost.cs


/assets
│
├─ shaders
│   ├─ basic.vert
│   └─ basic.frag
│
├─ meshes
│   └─ cube.mesh
│
├─ textures
│   └─ checker.png
│
└─ scenes
    └─ test.scene

/tools
│
├─ AssetCompiler
│   └─ AssetCompiler.csproj
│
└─ SceneEditor


### Terminology

**Swapchain** - A Queue of image buffers (front & back buffers) used in
graphics programming to render frames for displaying on the screen.