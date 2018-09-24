using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vrcontext.walkinside.sdk;

namespace WIExample
{    
    /// <summary>
    /// A Walkinside plugin example concerning how to retrieve information from ProjectManager and IVRBranch.
    /// </summary>
    /// <remarks>
    /// Displays the underlying Walkinside model data through ProjectManager and IVRBranch Interface. 
    /// </remarks>
    public class WIPlugin : IVRPlugin
    {
		internal static VRPluginDescriptor pDescriptor = new VRPluginDescriptor(
            VRPluginType.Unknown, 1, "", "19/01/2009", "Example 4: Projects and Branches.", "Walkinside Plugin Step 4.", "Vrcontext_SDK");
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
            // Create my menu item called "Example 4".
            pCommand = viewer.CommandManager.RegisterPluginMenuCommand(
                getNames: () => new[] { "Example 4" },
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
            pForm?.Dispose();
            // Remove my menu item called "Example 4" from the plugin menu.
            pCommand.Unregister();

            return true;
        }
    }
}
