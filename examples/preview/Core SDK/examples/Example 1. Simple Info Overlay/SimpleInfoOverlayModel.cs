using System;
using System.Linq;

using ViewerSdk = vrcontext.walkinside.sdk;
using CoreSdk = vrcontext.walkinside.sdk.Preview.Core;

namespace CoreSdkExamples
{
    sealed class SimpleInfoOverlayModel : CoreSdk.Model
    {
        public readonly string ProjectName;

        public SimpleInfoOverlayModel(
            ViewerSdk.IVRViewerSdk viewerSdk,
            CoreSdk.ModelManager modelManager)
            : base(modelManager)
        {
            this.runningSince = DateTime.UtcNow;
            this.viewerSdk = viewerSdk;

            // We will use monospaced font, 32 pixels high.
            this.fontFace = new CoreSdk.FontFace("Courier New", height: 32);

            // Tell Core SDK in which pass and stage we want to render.
            this.Subscribe(
                CoreSdk.PassSubscription.Overlay,
                CoreSdk.StageSubscription.Text);

            var currentProject = viewerSdk.ProjectManager.CurrentProject;

            // As rendering happens in a separate thread and viewer is not thread-safe, 
            // it is recommended to cache any viewer SDK variable locally for later use in the Render method. 
            this.ProjectName = currentProject.Name;

            // Register our model for rendering.
            this.Setup();
        }

        // Core will call this when a frame is being rendered.
        public override void Render(CoreSdk.RenderingContext context)
        {
            var text = string.Format(
                "Project:      {0}\n" +
                "Uptime (sys): {1:g}\n" +
                "Uptime (sim): {2:g}",
                ProjectName,
                DateTime.UtcNow - this.runningSince,
                this.simDuration);

            // Scale 300 will preserve our desired font line height
            // (here 32 screen pixels).
            const float scale = 300;

            // Bottom-left corner is (0, 0).
            var position = new CoreSdk.Vec2f(
                x: 10,
                y: context.Viewport.Height - 32); // Leaving space for 32 pixel line height.
            var shadowPosition = new CoreSdk.Vec2f(
                position.X + 2,
                position.Y - 2);

            // White opaque text with black opaque shadow.
            var color = new CoreSdk.Vec4f(1, 1, 1, 1);
            var shadowColor = new CoreSdk.Vec4f(0, 0, 0, 1);

            // We should restore rendering mode when we're done. Easiest way
            // is to wrap it in "using".
            using (context.Renderer.SetRenderingMode(CoreSdk.RenderingMode.Default2d))
            {
                context.Renderer.Draw(this.fontFace, shadowColor, shadowPosition, text, scale);
                context.Renderer.Draw(this.fontFace, color, position, text, scale);
            }
        }

        public override void Update(float timeDelta)
        {
            this.simDuration = this.simDuration.Add(TimeSpan.FromSeconds(timeDelta));
        }

        readonly ViewerSdk.IVRViewerSdk viewerSdk;
        readonly CoreSdk.FontFace fontFace;
        readonly DateTime runningSince;
        TimeSpan simDuration;
    }
}
