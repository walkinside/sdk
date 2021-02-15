using vrcontext.walkinside.sdk;

namespace WIExample
{
    /// <summary>
    /// A Walkinside plugin example concerning how to create a Walkinside context menu item.
    /// </summary>
    /// <remarks>
    /// Creates a context menu item and registers an event handler for the menu item click event.
    /// This context menu item pops up on mouse right click in 3D window.
    /// To activate the context menu, you need to click the "Example 6" menu item
    /// under Walkinside plugin menu.
    /// The plugin also shows how to use the raycast result. 
    /// </remarks>
    public class WIPlugin : IVRPlugin
    {
        internal static VRPluginDescriptor pDescriptor = new VRPluginDescriptor(
            VRPluginType.Unknown, 1, "", "19/01/2009", "Example 6: Context Menu.", "Walkinside Plugin Step 6.", "Vrcontext_SDK");
        /// <summary>
        /// Get the plugin descriptor without creating the plugin.
        /// </summary>
        static public VRPluginDescriptor GetDescriptor
        {
            get
            {
                return pDescriptor;
            }
        }

        /// <summary>
        /// Get the plugin descriptor.
        /// </summary>
        public VRPluginDescriptor Description
        {
            get
            {
                return pDescriptor;
            }
        }

        private IVRRegisteredCommand pCommand;
        private MainForm pForm;

        /// <summary>
        /// Called when attaching the plugin to the user interface.
        /// </summary>
        /// <param name="viewer">
        /// The walkinside viewer instance.
        /// </param>
        /// <returns>
        /// True if construction request succeeded.
        /// </returns>
        public bool CreatePlugin(IVRViewerSdk viewer)
        {
            // Create my menu item called "Example 6".
            pCommand = viewer.UiCommandManager.RegisterPluginMenuCommand(
                getNames: () => new[] { "Example 6" },
                execute: () =>
                {
                    pForm = new MainForm
                    {
                        DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight
                    };
                    pForm.Show();
                    pForm.Closing += (o, e) => { pForm = null; };
                },
                getState: () => pForm == null ? VRCommandState.Available : VRCommandState.Disabled);

            return true;
        }


        /// <summary>
        /// Called when detaching the plugin from the user interface.
        /// </summary>
        /// <param name="viewer">
        /// The walkinside viewer instance.
        /// </param>
        /// <returns>
        /// True if destruction request succeeded.
        /// </returns>
        public bool DestroyPlugin(IVRViewerSdk viewer)
        {
            // Remove my menu item called "Example 6" from the plugin menu.
            pCommand.Unregister();
            pForm?.Dispose();

            return true;
        }
    }
}
