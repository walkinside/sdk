using System;
using vrcontext.walkinside.sdk.Preview.Core;
using Comos.Walkinside.Common.Math;

namespace CoreSdkExamples
{
    public class BrowserView : IDisposable
    {
        private readonly CoreSdk pCoreSdk;
        private const uint pResolutionWidth = 2048u;
        private const uint pResolutionHeight = 2048u;
        private string pUri;

        private WebBrowser pBrowser;
        public BrowserView(CoreSdk coreSdk, string uri, string name)
        {
            pCoreSdk = coreSdk;
            pUri = uri;
            pBrowser = pCoreSdk.ShapeManager.MakeWebBrowser(
                Url: pUri,
                ResolutionWidth: pResolutionWidth,
                ResolutionHeight: pResolutionHeight);
            Name = name;
        }

        public override string ToString() => Name;

        public string Name { get; }

        public string Uri
        {
            get => pUri;
            set
            {
                if (pUri == value)
                {
                    return;
                }
                pUri = value;
                RecreateBrowser();
            }
        }

        public Point3d Position
        {
            get => (Point3d)pBrowser.Position;
            set => pBrowser.Position = (Vector3d)value;
        }

        public Quaternion Orientation
        {
            get => pBrowser.Orientation;
            set => pBrowser.Orientation = value;
        }

        public bool Visibile
        {
            get => pBrowser.Visible;
            set => pBrowser.Visible = value;
        }

        public double Width
        {
            get => pBrowser.Size.Width;
            set => pBrowser.Size = new Size2d(value, pBrowser.Size.Height);
        }

        public double Height
        {
            get => pBrowser.Size.Height;
            set => pBrowser.Size = new Size2d(pBrowser.Size.Width, value);
        }

        private void RecreateBrowser()
        {
            var position = pBrowser.Position;
            var orientation = pBrowser.Orientation;
            var size = pBrowser.Size;
            pBrowser.Dispose();

            pBrowser = pCoreSdk.ShapeManager.MakeWebBrowser(
                Url: pUri,
                ResolutionWidth: pResolutionWidth,
                ResolutionHeight: pResolutionHeight);
            pBrowser.Position = position;
            pBrowser.Orientation = orientation;
            pBrowser.Size = size;
        }

        public void Dispose()
        {
            pBrowser.Dispose();
        }
    }
}
