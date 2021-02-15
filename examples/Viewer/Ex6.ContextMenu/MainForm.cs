using System.ComponentModel;


using vrcontext.walkinside.sdk;

namespace WIExample
{
    public partial class MainForm : VRForm
    {
        private readonly IVRRegisteredCommand pCommand;
        public MainForm()
        {
            InitializeComponent();
            // Create a menu item in the 3D Context Menu, called "Example 6".
            pCommand = SDKViewer.UiCommandManager.RegisterGeometryCommand(
                result => new[] { "Example 6" },
                Ex6ContextMenu_Click
            );
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Remove the menu item "Example 6" from the 3D Context Menu.
            pCommand.Unregister();
            base.OnClosing(e);
        }

        void Ex6ContextMenu_Click(VRRayCastResult res)
        {
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
