using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using vrcontext.walkinside.sdk;

namespace WIExample
{
    public partial class MainForm : VRForm
    {
        ToolStripMenuItem m_Item = null;     // The reference to the menu item in ContextMenu3D of example 7
        ToolStripItem m_ItemCreate = null;   // The reference to the sub menu item "Create" of example 7
        ToolStripItem m_ItemDestroy = null;  // The reference to the sub menu item "Destroy" of example 7
        public MainForm()
        {
            InitializeComponent();
            // Create a menu item in the 3D Context Menu, called "Example 7".
            m_Item = SDKViewer.UI.Control.ContextMenuStrip.Items.Add("Example 7") as ToolStripMenuItem;
            // Create sub menu items to create and destroy labels.
            m_ItemCreate = m_Item.DropDownItems.Add("Create Label");
            m_ItemDestroy = m_Item.DropDownItems.Add("Destroy Label");
            // Attach a listener to detect when the user clicked create or destroy menu.
            m_ItemCreate.Click += new EventHandler(m_ItemCreate_Click);
            m_ItemDestroy.Click += new EventHandler(m_ItemDestroy_Click);

            // Create a group of labels in the 3D engine.
            m_LabelGroup = SDKViewer.CreateLabelGroup("Example7");
        }

        // All the label objects owned by this plugin. Stored in a dictionary to find easily the instance based on the IVRLabel.ID.
        Dictionary<uint, IVRLabel> m_Labels = new Dictionary<uint,IVRLabel>(); 
        IVRLabelGroup m_LabelGroup = null; // The label group owned by this plugin.

        void m_ItemDestroy_Click(object sender, EventArgs e)
        {
            // Get the click information from the Tag property as a VRRaycastResult type.
            VRRayCastResult res = SDKViewer.UI.Control.ContextMenuStrip.Tag as VRRayCastResult;
            IVRLabel label = null;

            // Try to get the label instance matching the ID. If not found, probably user clicked on a walkinside redline, or a label from other plugin.
            if (m_Labels.TryGetValue(res.TagID, out label))
            {
                // Remove the tag from the dictionary.
                m_Labels.Remove(res.TagID);
                // Remove the label from the 3D engine.
                m_LabelGroup.Remove(label);
                label = null;
                // Dump in the window text area the ID of the label destroyed.
                m_RichTextBox.Text += "Destroyed Label with ID : " + res.TagID.ToString() + "\r\n";
            }
            else
            {
                // Dump in the window text area the ID of the label clicked but not owned by this plugin.
                m_RichTextBox.Text += "Destroyed Label with ID : " + res.TagID.ToString() + "\r\n";
            }
        }

        void m_ItemCreate_Click(object sender, EventArgs e)
        {
            // Get the click information from the Tag property as a VRRaycastResult type.
            VRRayCastResult res = SDKViewer.UI.Control.ContextMenuStrip.Tag as VRRayCastResult;

            // Create the label at the location the user clicked, and set the text of the label to "New Label" and a next line with the position.
            IVRLabel label = m_LabelGroup.Add("New Label\n"+res.Position.ToString("f2"), res.Position);
            // Add it to the dictionary, for later reference (see m_ItemDestroy_Click)
            m_Labels.Add(label.ID, label);
            // Dump in the window text area the ID of the label created.
            m_RichTextBox.Text += "Created a new Label of ID : " + label.ID.ToString() + "\r\n";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Remove the listener from our context menu items.
            m_ItemCreate.Click -= new EventHandler(m_ItemCreate_Click);
            m_ItemDestroy.Click -= new EventHandler(m_ItemDestroy_Click);

            // Remove the menu item "Example 7" from the 3D Context Menu.
            SDKViewer.UI.Control.ContextMenuStrip.Items.Remove(m_Item);


            m_Labels.Clear(); // clear the dictionary, no need for it anymore.
            // Remove all the labels from the 3D engine by clearing labelgroups and Delete the label group from the 3D engine.
            m_LabelGroup.Clear();
            m_LabelGroup.Dispose();
            m_LabelGroup = null;

            // Remove the reference to the menu items objects.
            m_Item = null;
            m_ItemCreate = null;
            m_ItemDestroy = null;
            base.OnClosing(e);
        }
    }
}