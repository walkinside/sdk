using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using vrcontext.walkinside.sdk;

using vrcontext.walkinside.sdk.Preview.Core;

namespace CoreSdkExamples
{
    public partial class NightClubForm : VRForm
    {
        const string LetsGoHomeText = "Lets Go Home!";
        const string LetsPartyText = "Let's Party!";

        readonly IVRViewerSdk viewerSdk;
        readonly CoreSdk coreSdk;

        List<SpotLight> allLights = new List<SpotLight>();
        Timer propertyUpdateTimer;

        Random Randomizer = new Random(1985);

        Sound2d music = null;

        public NightClubForm(IVRViewerSdk viewer)
        {
            InitializeComponent();

            this.viewerSdk = viewer;
            this.coreSdk = viewerSdk.ForPreview.Core;

            propertyUpdateTimer = new Timer();
            propertyUpdateTimer.Tick += pTimer_Tick;
            propertyUpdateTimer.Interval = 100;
            propertyUpdateTimer.Start();

            PartyButton.Text = LetsPartyText;
            PartyButton.Click += LetsParty;
        }

        private void DisposeLights()
        {
            allLights.ForEach(l => l.Dispose());
            allLights.Clear();
        }

        private void LetsParty(object sender, EventArgs e)
        {
            var numberOfLights = (int)this.numericUpDownNumberOfLights.Value;

            PartyButton.Click -= LetsParty;
            PartyButton.Click += LetsGoHome;

            DisposeLights();
            for (int i = 0; i < numberOfLights; ++i)
            {
                var spotLight = this.coreSdk.LightManager.MakeSpotLight();

                RandomizeLightProperties(spotLight);

                allLights.Add(spotLight);
            }
            PartyButton.Text = LetsGoHomeText;

            if (music != null)
            {
                music.Play(true);
            }

            TableLayoutPanelMain.Enabled = false;
        }

        private void LetsGoHome(object sender, EventArgs e)
        {
            PartyButton.Click -= LetsGoHome;
            PartyButton.Click += LetsParty;

            DisposeLights();
            PartyButton.Text = LetsPartyText;
            if (music != null)
            {
                music.Stop();
            }
            TableLayoutPanelMain.Enabled = true;
        }

        private void RandomizeLightProperties(SpotLight spotLight)
        {
            var position = viewerSdk.Camera.Position;

            VRVector3D direction = new VRVector3D(
                Randomizer.NextDouble() - 0.5,
                -1.0,
                Randomizer.NextDouble() - 0.5);
            direction.Normalize();

            spotLight.Color = new Vector3f(
                (float)Randomizer.NextDouble(),
                (float)Randomizer.NextDouble(),
                (float)Randomizer.NextDouble());

            spotLight.CutOff = (float)Randomizer.Next(5, 60);
            spotLight.Direction = direction.ToVector3f();
            spotLight.Intensity = (float)Randomizer.Next(0, 100);
            spotLight.Range = (float)Randomizer.Next(5, 20);
            spotLight.Position = new Vector3f(
                (float)(position.X + (Randomizer.NextDouble() - 0.5) * 20),
                (float)position.Y + 5.0f,
                (float)(position.Z + (Randomizer.NextDouble() - 0.5) * 20));
        }

        void pTimer_Tick(object sender, EventArgs e)
        {
            if (allLights.Count == 0)
                return;

            foreach (var light in allLights)
            {
                bool enabled = Randomizer.Next(0, 1) == 0;
                light.Enabled = enabled;
                if (enabled)
                {
                    RandomizeLightProperties(light);
                }
            }
        }

        private void BrowseMusic(object sender, EventArgs e)
        {
            var browseMusicDialog = new OpenFileDialog();
            browseMusicDialog.Filter = "Sound Files|*.wav;*.ogg";

            if (browseMusicDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            try
            {
                var newMusic = this.coreSdk.SoundManager.MakeSound2d(browseMusicDialog.FileName);
                if (music != null)
                {
                    music.Dispose();
                }
                music = newMusic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
