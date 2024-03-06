using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megalistonator3k
{
    public class titleList
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Folder { get; set; }
        public titleList(string title, string description)
        {
            Title = title; Description = description; 
        }

    }
}
