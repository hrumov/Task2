using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public struct Journals
    {
        public string journalName;
        public List<Navigations> navigation;
    }


    public struct Navigations
    {
        public string bigItem;
        public string item;
    }
}