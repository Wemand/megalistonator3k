using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Megalistonator3k
{

    public class titleFolders
    {
        public string Name { get; set; }
        public List<titleList> includedTitles = new List<titleList> {};
        public titleFolders(string name) { Name = name;}
    }

}
