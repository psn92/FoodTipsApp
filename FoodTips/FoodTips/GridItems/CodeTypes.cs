using FoodTips.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTips.GridItems
{
    public class CodeTypes
    {
        public DictionaryType dyctionaryType;

        public Dictionary<string, int> WineVariant = new Dictionary<string, int>()
        {
            { "", 0 }
            , { "Grid_Variant_Value_Red", 1 }
            , { "Grid_Variant_Value_Rose", 2 }
            , { "Grid_Variant_Value_White", 3 }
            , { "Grid_Variant_Value_Mead", 4 }
        };

        public Dictionary<string, int> WineSpecification = new Dictionary<string, int>()
        {
            { "Grid_Specification_Value_Empty", 0 }
            , { "Grid_Specification_Value_Dry", 1 }
            , { "Grid_Specification_Value_SemiDry", 2 }
            , { "Grid_Specification_Value_SemiSweet", 3 }
            , { "Grid_Specification_Value_Sweet", 4 }
            , { "Grid_Specification_Value_EmptyMead", 0 }
            , { "Grid_Specification_Value_Poltorak", 5 }
            , { "Grid_Specification_Value_Dwojniak",  6 }
            , { "Grid_Specification_Value_Trojniak", 7 }
            , { "Grid_Specification_Value_Czworniak", 8 }
        };

        public CodeTypes()
        {
        }
        public CodeTypes(DictionaryType d)
        {
            dyctionaryType = d;
        }

        public string WineVariantCodeToString(int code)
        {
            string toString = "";
            switch (code)
            {
                case 1:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Variant_Value_Red();
                    break;
                case 2:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Variant_Value_Rose();
                    break;
                case 3:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Variant_Value_White();
                    break;
                case 4:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Variant_Value_Mead();
                    break;
            }

            return toString;
        }

        public string WineSpecificationCodeToString(int code)
        {
            string toString = "";
            switch (code)
            {
                case 1:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Specification_Value_Dry();
                    break;
                case 2:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Specification_Value_SemiDry();
                    break;
                case 3:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Specification_Value_SemiSweet();
                    break;
                case 4:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Specification_Value_Sweet();
                    break;
                case 5:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Specification_Value_Poltorak();
                    break;
                case 6:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Specification_Value_Dwojniak();
                    break;
                case 7:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Specification_Value_Trojniak();
                    break;
                case 8:
                    toString = dyctionaryType.WindowNewWineItem_Get_Grid_Specification_Value_Czworniak();
                    break;
            }

            return toString;
        }
    }
}
