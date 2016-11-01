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
            // Create a menu item called "Example 3".
            m_ToolStripItem = viewer.UI.PluginMenu.DropDownItems.Add("Example 3") as ToolStripMenuItem;
            //  Register a form with the menu item created. So Walkinside will take care of the user click event handling.
            viewer.UI.RegisterVRFormWithMenu(Keys.NoName, m_ToolStripItem, typeof(MyForm));
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
            viewer.UI.UnregisterVRFORM(m_ToolStripItem, typeof(MyForm));
            // Remove my menu item called "Example 3" from the plugin menu.
            viewer.UI.PluginMenu.DropDownItems.Remove(m_ToolStripItem);
            m_ToolStripItem = null;
            return true;
        }
    }
}
