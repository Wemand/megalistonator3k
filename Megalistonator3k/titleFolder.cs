using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Megalistonator3k
{

    public class titleFolders
    {
        public string Name { get; set; }
        public List<titleList> includedTitles = new List<titleList> {};
        public titleFolders(string name) { Name = name;}
        public void addFolder(string folderName, ListBox foldersList, List<titleFolders> titleFoldersList, titleFolders titleFolders)
        {
            if (!string.IsNullOrEmpty(folderName))
            {
                foldersList.Items.Add(folderName);
                titleFoldersList.Add(titleFolders);
                folderName = null;
            }
        }
    }

}
