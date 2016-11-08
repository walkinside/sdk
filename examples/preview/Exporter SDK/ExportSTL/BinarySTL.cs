using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vrcontext.walkinside.exporter;
using System.IO;

namespace ExportSimple
{
    class BinarySTL
    {
        // The scenenode reference of the Walkinside exporter SDK.
        IntPtr m_SceneNode = IntPtr.Zero;
        // Reference the root CAD hierarchy element in Walkinside model.
        long m_RootBranch = -1;

        /// <summary>
        /// Initialize the walkinside exporter sdk. Making it ready to process STL geometry.
        /// </summary>
        /// <param name="projectpath">The folder where the walkinside model will be created.</param>
        /// <param name="projectname">The name of the folder that contains the main walkinside project file (.vrp). </param>
        /// <returns></returns>
        public bool Initialize(string projectpath, string projectname)
        {
            // Initialize the SDK.
            if (0 == Bindings.vrBegin())
                return false;

            // Define the Walkinside model location.
            if (Bindings.vrSettingsFileNameW(projectpath + "\\" + projectname) == 0)
                return false;
            
            // Define the axis transformation to orientate STL geometry according Walkinside Axis.
            // Walkinside:          STL:
            //     X = Right            X = Right
            //     Y = Up               Y = ?
            //     Z = Depth            Z = ?
            VRT_VECTOR3 xa = new VRT_VECTOR3(1,  0,  0);
            VRT_VECTOR3 ya = new VRT_VECTOR3(0,  0, -1); // ToDo : Possible STL axis is not well defined, and these values need to change depending model.
            VRT_VECTOR3 za = new VRT_VECTOR3(0,  1,  0);
            Bindings.vrSettingsAxisOrientation(xa, ya, za);
            // Just provide some information of origin of walkinside model.
            Bindings.vrSettingsCadVersion("Stl sdk example");
            // Use meter as unit of measure.
            Bindings.vrSettingsUnit("M", 1);
            // Create a scene to store the geomtrical information.
            m_SceneNode = Bindings.vrOpenSceneNode(IntPtr.Zero, "projectname", VRT_ENUM_MATTABLE.New);
            // Create the root element in walkinside cad hierarchy.
            m_RootBranch = Bindings.vrOpenBranch(0, 0);
            Bindings.vrBranchSetNameW(m_RootBranch, projectname);
            Bindings.vrCloseBranch(m_RootBranch);

            return true;
        }

        public bool ExportFile(string filename)
        {
            using (BinaryReader reader = new BinaryReader(System.IO.File.Open(filename, FileMode.Open)))
            {
                // Read string of element (must be 80)
                char[] header = reader.ReadChars(80);
                string text = new string(header);

                // Define a CAD Hierarchy element to reference the 3D. (Not required, but allows the user to select the element in 3D)
                long cadbranch = Bindings.vrOpenBranch(m_RootBranch, 10);
                Bindings.vrBranchSetNameW(cadbranch, text);
                Bindings.vrCloseBranch(cadbranch);

                // Create the 3D element and assign it to the CAD Hierarchy item.
                IntPtr element = Bindings.vrBeginElement();
                Bindings.vrBranchAddElement(cadbranch,element);
                
                // Read number of facets in STL file.
                uint nbFacets = reader.ReadUInt32();

                // Start describing the 3d primitive that will contain the mesh.
                Bindings.vrBeginPrimitive();

                
                // Iterate all facets.
                for (ulong i = 0; i < nbFacets; i++)
                {
                    // Read the normal of the facet.
                    VRT_VECTOR3 normal = new VRT_VECTOR3();
                    normal.x = reader.ReadSingle();
                    normal.y = reader.ReadSingle();
                    normal.z = reader.ReadSingle();

                    // Read the 3 vertices of the triangle (= facet).
                    VRT_VECTOR3[] vertices = new VRT_VECTOR3[3];
                    for (int v = 0; v < 3; v++)
                    {
                        vertices[v] = new VRT_VECTOR3();
                        vertices[v].x = reader.ReadSingle();
                        vertices[v].y = reader.ReadSingle();
                        vertices[v].z = reader.ReadSingle();
                    }
                    // The attribute count should be 0 according spec.
                    ushort attributebytecount = reader.ReadUInt16();
                    System.Diagnostics.Debug.Assert(attributebytecount == 0);

                    // Start describing a polygon.
                    Bindings.vrBeginPolygon();

                    // Send the information as an array of normals and vertices. 
                    var normals = new VRT_VECTOR3[3] { normal, normal, normal };
                    Bindings.vrPolyNormalArray(3, normals, 0);
                    Bindings.vrPolyVertexArray(3, vertices, 0);

                    Bindings.vrEndPolygon();
                }

                Bindings.vrEndPrimitive();

                Bindings.vrEndElement();
            }
            return true;
        }

        public void UseColor(System.Drawing.Color color)
        {
            // Use the name of the color as a key to walkinside sdk.
            string materialname = color.ToString();
            // Set the walkinside builder sdk to use the material. (if not known SDK returns 0)
            if (0 == Bindings.vrMaterialAssignKey(materialname))
            {
                // Start defining the material to the SDK.
                Bindings.vrBeginMaterial(materialname);

                float[] c = new float[4];
                c[0] = (float)color.R / 255.0f;
                c[1] = (float)color.G / 255.0f;
                c[2] = (float)color.B / 255.0f;
                c[3] = (float)color.A / 255.0f;
                Bindings.vrMaterialColor(c);

                // End the material definition.
                Bindings.vrEndMaterial();
                // Assign the material again to use.
                int test = Bindings.vrMaterialAssignKey(materialname);
                System.Diagnostics.Debug.Assert(test != 0); // must be not 0 as material is just defined. 
            }
        }

        /// <summary>
        /// Terminate the export. The SDK will still do some processing on the provided triangle soup, so this can take a while depending the size, memory and disk speed.
        /// </summary>
        /// <returns></returns>
        public bool Finalize()
        {
            Bindings.vrCloseSceneNode(m_SceneNode);
            Bindings.vrEnd();
            return true;
        }
    }
}
