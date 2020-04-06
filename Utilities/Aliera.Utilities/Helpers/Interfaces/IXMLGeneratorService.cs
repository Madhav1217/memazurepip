using System;
using System.Collections.Generic;
using System.Text;


namespace Aliera.Utilities.Helpers
{
    public interface IXMLGeneratorService
    {
        void GenerateXML(object hrpObject, string fileName);
    }
}
