using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vrcontext.walkinside.sdk;

namespace WIExample
{
    /// <summary>
    /// A Walkinside plugin example concerning how to add a custom form into Walkinside viewer.
    /// </summary>
    /// <remarks>
    /// Creates a form and integrates it into Walkinside viewer. 
    /// A menu item called "Example3" is also created and added to Walkinside Plugin Menu. 
    /// On mouse click of this menu item, a custom dockable form will be shown in Walkinside viewer.
    /// </remarks>
    public class WIPlugin : IVRPlugin
    {
        internal static VRPluginDescriptor pDescriptor = new VRPluginDescriptor(
            VRPluginType.Unknown, 1, "", "19/01/2009", "Example 3: Plugin Form", "Walkinside Plugin Step 3.", "Vrcontext_SDK");

        private IVRRegisteredCommand pCommand;
        private MyForm pForm;

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
            // Create a menu item called "Example 3".
            pCommand = viewer.UiCommandManager.RegisterPluginMenuCommand(
                getNames: () => new[] {"Example 3"},
                execute: () =>
                {
                    pForm = new MyForm
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
            pForm?.Dispose();
            // Remove my menu item called "Example 3" from the plugin menu.
            pCommand.Unregister();
            return true;
        }
    }
}
