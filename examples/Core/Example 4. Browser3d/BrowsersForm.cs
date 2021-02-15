using System;
using vrcontext.walkinside.sdk;
using System.Linq;
using Comos.Walkinside.Viewer.Actors;

namespace CoreSdkExamples
{
    public partial class BrowsersForm : VRForm
    {
        private readonly IVRViewerSdk pViewer;
        private const double pBrowserDistance = 5.0;

        public BrowsersForm(IVRViewerSdk viewer)
        {
            InitializeComponent();
            pViewer = viewer;
            System.Windows.Forms.Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            ButtonRemove.Enabled = ListBrowsers.SelectedItem != null;
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            PropertyGridBrowser.SelectedObject = ListBrowsers.SelectedItem;
        }

        private int browserId = 1;
        private void AddBrowser(object sender, EventArgs e)
        {
            var actor = pViewer.CurrentActor;
            var browser = new BrowserView(pViewer.ForPreview.Core, "www.google.com", $"Browser {browserId++}");
            browser.Position = actor.Position + actor.GetForwardVector() * pBrowserDistance;
            browser.Orientation = actor.Orientation;
            ListBrowsers.Items.Add(browser);
        }

        private void RemoveBrowser(object sender, EventArgs e)
        {
            ((BrowserView)ListBrowsers.SelectedItem).Dispose();
            ListBrowsers.Items.RemoveAt(ListBrowsers.SelectedIndex);
        }

        protected override void OnClosed(EventArgs e)
        {
            System.Windows.Forms.Application.Idle -= Application_Idle;
            foreach (var browser in ListBrowsers.Items.Cast<BrowserView>())
            {
                browser.Dispose();
            }
        }
    }
}
