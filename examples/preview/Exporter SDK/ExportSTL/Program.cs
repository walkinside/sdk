using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportSimple
{
    /// <summary>
    /// An example of translating an STL file to a Walkinside model.
    /// Information of fileformat can be found here http://www.eng.nus.edu.sg/LCEL/RP/u21/wwwroot/stl_library.htm
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Let the user select an stl file.
            OpenFileDialog d = new OpenFileDialog();
            if (d.ShowDialog() != DialogResult.OK)
                return;
            
            // Create the STL Binary exporter.
            BinarySTL export = new BinarySTL();

            // Initialize the translation process, using the selected file as basis.
            string folder = System.IO.Path.GetDirectoryName(d.FileName);
            string projectname = System.IO.Path.GetFileNameWithoutExtension(d.FileName);
            export.Initialize(folder, projectname);
            
            // STL does not store color information, so lets use some fancy color.
            export.UseColor(System.Drawing.Color.LightGoldenrodYellow); 
            
            // Start translating the STL file to a walkinside model.
            export.ExportFile(d.FileName);
            
            // Terminate the process.
            export.Finalize();
        }
    }
}
