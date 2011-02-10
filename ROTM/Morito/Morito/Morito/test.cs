using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Morito
{
    public partial class test : Component
    {
        public test()
        {
            InitializeComponent();
        }

        public test(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
