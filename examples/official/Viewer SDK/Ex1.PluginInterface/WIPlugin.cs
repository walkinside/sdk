using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using vrcontext.walkinside.sdk;

namespace WIExample
{
    /// <summary>
    /// A simple plugin fulfilling the minimum requirements to be loaded, enabled and run in Walkinside.
    /// </summary>
    /// <remarks>
    /// This plugin does nothing else than showing message boxes when being 
    /// created or destroyed. Through this example, SDK user can learn how 
    /// to use IVRPlugin interface.
    /// </remarks>
    public class WIPlugin : IVRPlugin
    {
        internal static VRPluginDescriptor pDescriptor = new VRPluginDescriptor(
            VRPluginType.Unknown, 1, "", "19/01/2009", "Example 1: Plugin Interface", "Walkinside Plugin Step 1.", "Vrcontext_SDK");
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
            // Do initialisation of plugin instance.
            string message = "Walkinside Plugin Example 1 : Initialised";
            Console.WriteLine(message);
            MessageBox.Show(message);            
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
            // Do finalisation of plugin instance.
            string message = "Walkinside  Plugin Example 1 : Destroyed";
            Console.WriteLine(message);
            MessageBox.Show(message);
            return true;
        }
    }
}
