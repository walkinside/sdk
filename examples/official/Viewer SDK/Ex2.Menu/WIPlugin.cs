using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vrcontext.walkinside.sdk;

namespace WIExample
{
    /// <summary>
    /// A Walkinside plugin example concerning how to add a menu item into Walkinside plugin menu.
    /// </summary>
    /// <remarks>
    /// Creates a menu item and adds it into Walkinside "plugin menu", which is the 
    /// "View" menu item toolstrip under Walkinside main menu.
    /// </remarks>
    public class WIPlugin : IVRPlugin
    {
        internal static VRPluginDescriptor pDescriptor = new VRPluginDescriptor(
            VRPluginType.Unknown, 1, "", "19/01/2009", "Example 2: Plugin Menu", "Walkinside Plugin Step 2.", "Vrcontext_SDK");
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

        ToolStripItem m_ToolStripItem = null;

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
            // Create a menu item called "Example 2".
            m_ToolStripItem = viewer.UI.PluginMenu.DropDownItems.Add("Example 2");
            m_ToolStripItem.Click += new EventHandler(m_ToolStripItem_Click);
            return true;
        }

        /// <summary>
        /// Called when "Example 2" menu item is clicked.
        /// </summary>
        void m_ToolStripItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello there, this is example 2");
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
            m_ToolStripItem.Click -= m_ToolStripItem_Click;
            // Remove "Example 2" menu item from the plugin menu.
            viewer.UI.PluginMenu.DropDownItems.Remove(m_ToolStripItem);
            m_ToolStripItem = null;
            return true;
        }
    }
}
