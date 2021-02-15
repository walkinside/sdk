using System;
using System.Diagnostics;

using ViewerSdk = vrcontext.walkinside.sdk;

namespace CoreSdkExamples
{
    /// <summary>
    /// A simple Viewer SDK plug-in that makes use of Core SDK to display
    /// "9... 8... 7..." countdown in 3D view.
    /// </summary>
    public class ExamplePlugin : ViewerSdk.IVRPlugin
    {
        static ViewerSdk.VRPluginDescriptor descriptor = new ViewerSdk.VRPluginDescriptor(
            shortDescription: "Core SDK Example 2: Countdown",
            longDescription: "Demonstrates some animated text.",
            type: ViewerSdk.VRPluginType.Unknown,
            version: 1,
            group: string.Empty,
            compileDate: "10/02/2016",
            license: "Vrcontext_SDK");

        static public ViewerSdk.VRPluginDescriptor GetDescriptor
        {
            get { return descriptor; }
        }

        public ViewerSdk.VRPluginDescriptor Description
        {
            get { return descriptor; }
        }

        public bool CreatePlugin(ViewerSdk.IVRViewerSdk viewerSdk)
        {
            this.viewerSdk = viewerSdk;

            if (viewerSdk.ForPreview.Core != null)
            {
                // Core SDK is available - initialize model right away.
                this.initializeModel();
            }

            viewerSdk.ForPreview.CoreSdkInitialized += this.viewerSdk_CoreSdkInitialized;
            viewerSdk.ForPreview.CoreSdkDisposing += this.viewerSdk_CoreSdkDisposing;

            return true;
        }

        void viewerSdk_CoreSdkInitialized(object sender, EventArgs eventArgs)
        {
            this.initializeModel();
        }

        void viewerSdk_CoreSdkDisposing(object sender, EventArgs e)
        {
            this.deinitializeModel();
        }

        public bool DestroyPlugin(ViewerSdk.IVRViewerSdk viewerSdk)
        {
            if (this.model != null)
            {
                this.deinitializeModel();
            }

            // As core SDK is disposed when a project is loaded, we listen to its disposal/initialization
            // in order to restore model when a new project is loaded.
            viewerSdk.ForPreview.CoreSdkInitialized -= this.viewerSdk_CoreSdkInitialized;
            viewerSdk.ForPreview.CoreSdkDisposing -= this.viewerSdk_CoreSdkDisposing;

            return true;
        }

        void initializeModel()
        {
            Debug.Assert(this.model == null);

            this.model = new CountdownModel(
                this.viewerSdk.ForPreview.Core.ModelManager,
                startingNumber: 9,
                finalMessage: "Go!");
        }

        void deinitializeModel()
        {
            Debug.Assert(this.model != null);

            this.model.Dispose();
            this.model = null;
        }

        ViewerSdk.IVRViewerSdk viewerSdk;
        IDisposable model;
    }
}
