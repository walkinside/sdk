using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using vrcontext.walkinside.exporter;

namespace ExportGrid
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        int width = 50;
        int height = 50;
        int depth = 50;
        private void Build_Click(object sender, EventArgs e)
        {
            int.TryParse(TxtWidth.Text, out width);
            int.TryParse(TxtHeight.Text, out height);
            int.TryParse(TxtDepth.Text, out depth);

            FolderBrowserDialog d = new FolderBrowserDialog();
            if (DialogResult.OK != d.ShowDialog())
                return;

            Bindings.vrBegin();
            if (Bindings.vrSettingsFileNameW(d.SelectedPath + "\\Grid.model") == 0)
                return;
            VRT_VECTOR3 xa = new VRT_VECTOR3(1, 0, 0);
            VRT_VECTOR3 ya = new VRT_VECTOR3(0, 1, 0);
            VRT_VECTOR3 za = new VRT_VECTOR3(0, 0, 1);

            Bindings.vrSettingsAxisOrientation(xa, ya, za);
            Bindings.vrSettingsCadVersion("Grid");
            Bindings.vrSettingsUnit("M", 1);

            IntPtr scenenode = Bindings.vrOpenSceneNode(IntPtr.Zero, "Grid", VRT_ENUM_MATTABLE.New);

            long branch = Bindings.vrOpenBranch(0, 0);
            Bindings.vrBranchSetNameW(branch, "GridBranch");
            Bindings.vrCloseBranch(branch);
            

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        double dx = (double)x * 0.5;
                        double dy = (double)y * 0.5;
                        double dz = (double)z * 0.5;
                        VRT_VECTOR3 min = new VRT_VECTOR3(dx, dy, dz);
                        VRT_VECTOR3 max = new VRT_VECTOR3(dx + 0.5, dy + 0.5, dz + 0.5);
                        IntPtr elm = Bindings.vrBeginElement();
                        UseColor(GetRandomColor());
                        CreateBox(min, max);
                        Bindings.vrEndElement();
                        Bindings.vrBranchAddElement(branch, elm);
                    }
                }
            }

            Bindings.vrCloseSceneNode(scenenode);

            Bindings.vrEnd();
        }

        Random rand = new Random();
        Color pColor = Color.White;
        private System.Drawing.Color GetRandomColor()
        {
            int r = rand.Next(2) * 50 + 128;
            int g = rand.Next(2) * 50 + 128;
            int b = rand.Next(2) * 50 + 128;

            //pColor = Color.FromArgb(100, (pColor.R + r) / 2, (pColor.G + g)/2, (pColor.B + b) / 2);
            pColor = Color.FromArgb(255, r, g, b);
            return pColor;
        }

        private void CreateBox(VRT_VECTOR3 minimum, VRT_VECTOR3 maximum)
        {
            // ---- calculate the 8 corner points ----
            VRT_VECTOR3[] verCorner = new VRT_VECTOR3[8];

            // corners are:
            // 0 =  x -y -z
            // 1 =  x -y  z
            // 2 = -x -y  z
            // 3 = -x -y -z
            // 4 =  x  y -z
            // 5 =  x  y  z
            // 6 = -x  y  z
            // 7 = -x  y -z

            // set vertex corner coordinates
            verCorner[0].x = maximum.x; verCorner[0].y = minimum.y; verCorner[0].z = minimum.z;
            verCorner[1].x = maximum.x; verCorner[1].y = minimum.y; verCorner[1].z = maximum.z;
            verCorner[2].x = minimum.x; verCorner[2].y = minimum.y; verCorner[2].z = maximum.z;
            verCorner[3].x = minimum.x; verCorner[3].y = minimum.y; verCorner[3].z = minimum.z;
            verCorner[4].x = maximum.x; verCorner[4].y = maximum.y; verCorner[4].z = minimum.z;
            verCorner[5].x = maximum.x; verCorner[5].y = maximum.y; verCorner[5].z = maximum.z;
            verCorner[6].x = minimum.x; verCorner[6].y = maximum.y; verCorner[6].z = maximum.z;
            verCorner[7].x = minimum.x; verCorner[7].y = maximum.y; verCorner[7].z = minimum.z;

            // now write the polys to the file
            // polys are formed from corner points:
            // face 0 = 0 1 2 3
            // face 1 = 4 5 1 0
            // face 2 = 7 6 5 4
            // face 3 = 3 2 6 7 
            // face 4 = 3 7 4 0
            // face 5 = 6 2 1 5


            VRT_VECTOR3[] verts = new VRT_VECTOR3[4];
            
            int[] coord = new int[4];

            // Begin the primitive solution.
            Bindings.vrBeginPrimitive();

            for (int nrFaces = 0; nrFaces < 6; nrFaces++)
            {
                switch (nrFaces)
                {
                    case 0:
                        coord[0] = 0; coord[1] = 1; coord[2] = 2; coord[3] = 3;
                        break;
                    case 1:
                        coord[0] = 0; coord[1] = 4; coord[2] = 5; coord[3] = 1;
                        break;
                    case 2:
                        coord[0] = 4; coord[1] = 7; coord[2] = 6; coord[3] = 5;
                        break;
                    case 3:
                        coord[0] = 7; coord[1] = 3; coord[2] = 2; coord[3] = 6;
                        break;
                    case 4:
                        coord[0] = 0; coord[1] = 3; coord[2] = 7; coord[3] = 4;
                        break;
                    case 5:
                        coord[0] = 5; coord[1] = 6; coord[2] = 2; coord[3] = 1;
                        break;
                }

                int counter = 0;
                for (counter = 0; counter < 4; counter++)
                {
                    verts[counter].x = verCorner[coord[counter]].x;
                    verts[counter].y = verCorner[coord[counter]].y;
                    verts[counter].z = verCorner[coord[counter]].z;
                }

                Bindings.vrBeginPolygon();
                Bindings.vrPolyVertexArray(4, verts, 0);
                Bindings.vrEndPolygon();
            }

            Bindings.vrEndPrimitive();
        }

        public void UseColor(System.Drawing.Color color)
        {
            string materialname = color.ToString();
            if (0 == Bindings.vrMaterialAssignKey(materialname))
            {
                Bindings.vrBeginMaterial(materialname);

                float[] c = new float[4];
                c[0] = (float)color.R / 255.0f;
                c[1] = (float)color.G / 255.0f;
                c[2] = (float)color.B / 255.0f;
                c[3] = (float)color.A / 255.0f;
                Bindings.vrMaterialColor(c);

                Bindings.vrEndMaterial();
                int test = Bindings.vrMaterialAssignKey(materialname);
            }
        }
    }
}
