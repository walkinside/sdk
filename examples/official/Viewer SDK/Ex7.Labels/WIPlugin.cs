using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vrcontext.walkinside.sdk;

namespace WIExample
{
    /// <summary>
    /// A Walkinside plugin example concerning how to create and destroy labels in Walkinside 3D viewer.
    /// </summary>
    /// <remarks>
    /// Creates and destroy labels in Walkinside 3D viewer.
    /// Context menu items "Create Label" and "Destroy Label" are created for this functionality.
    /// To active context menu items, user needs to click the "Example 7" menu item under plugin menu.
    /// </summary>
    public class WIPlugin : IVRPlugin
    {
		internal static VRPluginDescriptor pDescriptor = new VRPluginDescriptor(
            VRPluginType.Unknown, 1, "", "19/01/2009", "Example 7: Labels.", "Walkinside Plugin Step 7.", "Vrcontext_SDK");
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

        ToolStripMenuItem m_ToolStripItem = null;

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
            // Create my menu item called "Example 7".
            m_ToolStripItem = viewer.UI.PluginMenu.DropDownItems.Add("Example 7") as ToolStripMenuItem;
            //  Register a form with the menu item created. So Walkinside will take care of the user click event handling.
            viewer.UI.RegisterVRFormWithMenu(Keys.NoName, m_ToolStripItem, typeof(MainForm));

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
            // Remove the form registration from the menu item. This is necessary if the user disables the plugin, while it has been activated.
            viewer.UI.UnregisterVRFORM(m_ToolStripItem, typeof(MainForm));
            // Remove my menu item called "Example 7" from the plugin menu.
            viewer.UI.PluginMenu.DropDownItems.Remove(m_ToolStripItem);
            m_ToolStripItem = null;

            return true;
        }
    }
}
