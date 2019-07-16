﻿using System;
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
    /// <summary>
    /// Inherit your form from VRForm which handles all the docking behavior.
    /// </summary>
    /// <remarks>
    /// If your form can not be opened in the designer of Visual Studio, rename VRForm to Form,
    /// Open it in the designer and make modifications. When finished rename Form back to VRForm.
    /// </remarks>
    public partial class MyForm : VRForm
    {
        public MyForm()
        {
            InitializeComponent();
        }
    }
}
