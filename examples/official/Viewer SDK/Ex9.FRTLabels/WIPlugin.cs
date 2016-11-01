using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using vrcontext.walkinside.sdk;
using System.Xml;
using System.Xml.XPath;

namespace WIExample
{
    /// <summary>
    /// A Walkinside plugin example concerning how to create FRT labels.
    /// </summary>
    /// <remarks>
    /// Creates labels by reading Data/Labels.xml.
    /// Note that when opening a new project all the objects are reset to original state.
    /// </remarks>
    public class WIPlugin : IVRPlugin
    {
		internal static VRPluginDescriptor pDescriptor = new VRPluginDescriptor(
			VRPluginType.Unknown, 1, "", "19/01/2009", "Example 9: Labels - Show Labels.", "Walkinside Plugin Step 9.", "Vrcontext_SDK");
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
        IVRViewerSdk pviewer = null;
        IVRLabelGroup m_LabelGroup = null; // The label group owned by this plugin.

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
            // Create my menu item called "Show labels".
            m_ToolStripItem = viewer.UI.PluginMenu.DropDownItems.Add("Example 9") as ToolStripMenuItem;
            pviewer = viewer;
            m_ToolStripItem.Click += myItem_Click;

            viewer.ProjectManager.OnProjectClose += ProjectManager_OnProjectClose;

            return true;
        }

        void ProjectManager_OnProjectClose(object sender, VRProjectEventArgs e)
        {
            if (m_ToolStripItem.Checked)
            {
                DisposeLabelGroup();
            }
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
            viewer.ProjectManager.OnProjectClose -= ProjectManager_OnProjectClose;

            // Remove my menu item called "Example 9" from the plugin menu.
            viewer.UI.PluginMenu.DropDownItems.Remove(m_ToolStripItem);

            if (m_ToolStripItem.Checked)
            {
                DisposeLabelGroup();
            }

            m_ToolStripItem = null;
            return true;
        }

        void DisposeLabelGroup()
        {
            m_ToolStripItem.Checked = false;
            m_LabelGroup.Clear();
            m_LabelGroup.Dispose();
            m_LabelGroup = null;
        }

        void CreateLabelGroup()
        {
            m_ToolStripItem.Checked = true;
            m_LabelGroup = null;
            m_LabelGroup = pviewer.CreateLabelGroup("ShowLabels");
            m_LabelGroup.MaximumDistance = 10.0f;

            m_LabelGroup.Color = Color.DeepPink;
        }
        private void myItem_Click(object sender, EventArgs e)
        {
            if (m_ToolStripItem.Checked)
            {
                DisposeLabelGroup();
            }
            else 
            {
                CreateLabelGroup();
                if (!load_Labels())
                {
                    string fn = pviewer.ProjectManager.CurrentProject.FolderPath + "/Data/Labels.xml";
                    if (System.IO.File.Exists(fn))
                    {
                        System.IO.File.Delete(fn);
                    }
                    write_labels(fn);
                    load_Labels();
                }
            }
        }

        private bool load_Labels()
        {
            string fn = pviewer.ProjectManager.CurrentProject.FolderPath + "/Data/Labels.xml";
            if (!System.IO.File.Exists(fn))
            {
                return false;
            }

            XPathDocument doc = new XPathDocument(fn);
            XPathNavigator nav = doc.CreateNavigator();
            var xml = nav.Select("Labels/Label");
            while(xml.MoveNext())
            {
                string xpos = xml.Current.GetAttribute("x", "");
                string ypos = xml.Current.GetAttribute("y", "");
                string zpos = xml.Current.GetAttribute("z", "");
                string name = xml.Current.GetAttribute("name", "");
                double x = double.Parse(xpos, System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(ypos, System.Globalization.CultureInfo.InvariantCulture);
                double z = double.Parse(zpos, System.Globalization.CultureInfo.InvariantCulture);
                VRVector3D pos = new VRVector3D(x, y, z);
                IVRLabel label = m_LabelGroup.Add(name, pos);
            }

            return true;
        }

        private void write_labels(string fn)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlTextWriter.Create(fn, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Labels");

                var frtRoots = pviewer.ProjectManager.CurrentProject.BranchManager.GetRoots(VRBranchKind.Frt);
                foreach (IVRBranch branch in frtRoots)
                {
                    write_labels(branch,writer);
                }
                
                writer.WriteEndElement();
            }
        }

        private void write_labels(IVRBranch branch, XmlWriter writer)
        {
            if (branch.HasChildren)
            {
                foreach (IVRBranch child in branch.Children)
                {
                    write_labels(child,writer);
                }
            }
            else
            {
                VRVector3D pos;
                string name;

                if (calc_label(branch, out pos, out name))
                {
                    writer.WriteStartElement("Label");
                    writer.WriteAttributeString("x", pos.X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("y", pos.Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("z", pos.Z.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("name", name);
                    writer.WriteEndElement();
                }
            }
        }


        private bool calc_label(IVRBranch branch, out VRVector3D pos, out string name)
        {
            name = branch.Name;
            pos = branch.AABB.Center;
            if ((pos.X == 0) && (pos.Y == 0) && (pos.Z == 0))
                return false;
            return true;
        }
    }
}
