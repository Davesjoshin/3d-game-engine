# 3DGE - 3d Game Engine

## Build info
.Net v8 SDK & Runtime
```dotnet run --project src/Host```

## Third party docs
C# - https://learn.microsoft.com/en-us/dotnet/csharp/

Veldrid - https://veldrid.dev/
- GraphicsDevice - https://veldrid.dev/api/Veldrid.GraphicsDevice.html
- GraphicsDeviceOptions - https://veldrid.dev/api/Veldrid.GraphicsDeviceOptions.html

## TODO List
- ~~Create a native application window.~~
- ~~Initialize the graphics device.~~
    - ~~Create the object that represents the connection to the GPU. (GraphicsDevice in Veldrid)~~
    - ~~Responsible for: GPU context, Resource creation (buffers, textures, pipelines), command submission,
        synchronization, and the swapchain.~~
- ~~Create and manage the swapchain.~~
- ~~Clear the screen every frame.~~
- ~~Render a single text triangle.~~
- Handle window resize
- Introduce a depth buffer.
- Move triangle positions out of clip space (into world space).
- Implement a camera system.
- Add a depth buffer to the render pipeline.
- Render a mesh with a transform.
- Support basic vertex and index buffers.
- Introduce an ECS (Entity Component System).
- Separate update and render systems.
- Define a basic render graph.
- Support multiple render passes.
- Load shaders from disk.
- Load mesh data from assets.
- Load textures from assets.
- Add an asset lifetime manager.
- Implement basic lighting.
- Add material definitions.
- Support per-object materials.
- Enable basic shading models.
- Add debug logging.
- Render debug geometry.
- Display frame timing stats.
- Enable runtime validation layers.

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


## Terminology

**Depth Buffer** - (Also known as a Z-buffer) is a dedicated memory buffer
that stores the depth information (distance from camera) for each pixel in
an image.

**Render Graph** - (Also known as a frame graph) is a high-level, declarative
data structure used to manage the rendering pipeline by representing rendering
operations as a directed acyclic graph.

**Shader** - Small programs that run on GPUs to calculate rendering effects, transforming
3D models into 2D pixels on screen. They define how surfaces interact with light,
colors, and textures, enabling real-time visual effects like reflections, shadows, and
post processing, rather than relying on the CPU.

**Swapchain** - A Queue of image buffers (front & back buffers) used in
graphics programming to render frames for displaying on the screen.

**Vertex** - (plural: vertices) is the fundamental data structure used to
define the building blocks of 3d models and geometry in a virutal environment.
