using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTips.GridItems
{
    public class NamesList
    {
        private List<string> names;

        public NamesList()
        {
            names = new List<string>();
        }

        public void add(string name)
        {
            names.Add(name);
        }

        public bool checkIfAlreadyExists(string candidate)
        {
            foreach (string name in names)
                if (name.Equals(candidate))
                    return true;
            return false;
        }
    }
}
