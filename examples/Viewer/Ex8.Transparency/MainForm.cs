using Comos.Walkinside.Common.Branches;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using vrcontext.walkinside.sdk;

namespace WIExample
{
    public partial class MainForm : VRForm
    {
        // The common Selection group we will modify its properties in this SDK example.
        IVRGroupBuilder pGroup = null;

        public MainForm()
        {
            InitializeComponent();
            // Create the group to store the elemnts from the search.
            pGroup = SDKViewer.GroupManager.CreateEmptyGroup();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // When closing the window, we will release elements in the group.
            pGroup.Clear();
            // Release the resource in the 3D viewer engine.
            pGroup.Dispose();
            // Remove the reference to the menu items objects.
            base.OnClosing(e);
        }

        private void TrckBarTransparancy_ValueChanged(object sender, EventArgs e)
        {
            // Aply the transparancy to the group.
            var c = Color.FromArgb(TrckBarTransparancy.Value, BtnColor.BackColor.R, BtnColor.BackColor.G, BtnColor.BackColor.B); // Very transparent green.
            pGroup.ApplyColor(c);
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            // Let the user pick a color.
            ColorDialog d = new ColorDialog();
            d.ShowDialog();
            // Update the color on the button.
            BtnColor.BackColor = d.Color;
            // Apply the color to the group.
            var c = Color.FromArgb(TrckBarTransparancy.Value, BtnColor.BackColor.R, BtnColor.BackColor.G, BtnColor.BackColor.B); // Very transparent green.
            pGroup.ApplyColor(c);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // User hitted the search button.
            Search();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            // Check if user hitted enter key, in the search text area.
            if (e.KeyCode == Keys.Return)
            {
                // Search for the elements.
                Search();
            }
        }

        private void Search()
        {
            // Lookup the branches matching the name the user placed in the search text area.
            var branches = SDKViewer.ProjectManager.CurrentProject.BranchManager.GetBranchesByExactNamesAndKind(new string[] { textBox1.Text }, BranchKind.Cad);
            // Clear the previous result from the group.
            pGroup.Clear();
            // Iterate all the results, and add the elements to the group.
            foreach (IVRBranch b in branches)
            {
                pGroup.Add(b);
            }
            // APply the color to the group of elements.
            var c = Color.FromArgb(TrckBarTransparancy.Value, BtnColor.BackColor.R, BtnColor.BackColor.G, BtnColor.BackColor.B); // Very transparent green.
            pGroup.ApplyColor(c);
            // Jump the camara to the group.
            pGroup.JumpTo();
        }

        private void CheckBlinking_CheckedChanged(object sender, EventArgs e)
        {
            // Blinking style switched on or off by user.
            if (CheckBlinking.Checked)
                pGroup.ApplyStyle(VRStyle.Blinking);
            else
                pGroup.ApplyStyle(VRStyle.None);
        }

    }
}
