using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTips.Language
{
    class EnglishDictionary_WindowNewSpiceItem : EnglishDictionary_WindowNewDishItem
    {
        public string WindowNewSpiceItem_Get_Grid_Name_Label() { return "Name"; }
        public string WindowNewSpiceItem_Get_Grid_Matchings_Label() { return "Matchings"; }
        public string WindowNewSpiceItem_Get_Grid_Description_Label() { return "Description"; }
        public string WindowNewSpiceItem_Get_Grid_LinkToSource_Label() { return "Link to source"; }
    }
}
