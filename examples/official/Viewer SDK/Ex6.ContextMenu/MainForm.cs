using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using vrcontext.walkinside.sdk;

namespace WIExample
{
    public partial class MainForm : VRForm
    {
        ToolStripItem m_Item = null;
        public MainForm()
        {
            InitializeComponent();
            // Create a menu item in the 3D Context Menu, called "Example 6".
            m_Item = SDKViewer.UI.Control.ContextMenuStrip.Items.Add("Example 6");
            // Attach a listener to detect when the Context menu opens.
            m_Item.Click += new EventHandler(Ex6ContextMenu_Click);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Remove the listener from our context menu item.
            m_Item.Click -= new EventHandler(Ex6ContextMenu_Click);
            // Remove the menu item "Example 6" from the 3D Context Menu.
            SDKViewer.UI.Control.ContextMenuStrip.Items.Remove(m_Item);
            // Remove the reference to the menu item object.
            m_Item = null;
            base.OnClosing(e);
        }

        void Ex6ContextMenu_Click(object sender, EventArgs e)
        {
            // Get the click information from the Tag property as a VRRaycastResult type.
            VRRayCastResult res = SDKViewer.UI.Control.ContextMenuStrip.Tag as VRRayCastResult;
            // The Point2D contains the pixel position in 3D window space.
            m_RichTextBox.Text = "Clicked on window position = " + res.Point2D.ToString();
            // The position contains the 3D coordinate where the user clicked on the element.
            m_RichTextBox.Text += "\r\nClicked on 3D position = " + res.Position.ToString();
            // The origin of the ray, defines the 3D position in world space, corresponding with the Point2D coordinate projected on to camera screen.
            m_RichTextBox.Text += "\r\nThe click creates a line from position = \r\n\t" + res.Ray.Origin.ToString();
            // The direction of the ray. Could also be calculated from the position and ray.origin.
            m_RichTextBox.Text += "\r\n\twith a direction = \r\n\t\t" + res.Ray.Direction.ToString();
            // There is an indirect relationship between the 3D element and the CAD/FRT elements.
            // So this code finds back all Branch objects this 3D element belongs to.
            m_RichTextBox.Text += "\r\nThe branches the 3d element belongs to = ";
            if (res.Branches != null)
            {
                foreach (IVRBranch branch in res.Branches)
                {
                    m_RichTextBox.Text += "\r\n\t" + branch.Name;
                }
            }
            else
            {
                m_RichTextBox.Text += "\r\n\t This element is not part of any branch !!";
            }
        }
    }
}