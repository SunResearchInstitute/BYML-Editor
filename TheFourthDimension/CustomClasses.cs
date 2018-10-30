using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace The4Dimension
{

    class CustomStringWriter : System.IO.StringWriter
    {
        private readonly Encoding encoding;

        public CustomStringWriter(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }
}
