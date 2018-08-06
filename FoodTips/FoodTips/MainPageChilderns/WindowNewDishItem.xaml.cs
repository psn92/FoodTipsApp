using FoodTips.GridItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FoodTips.MainPageChilderns
{
    /// <summary>
    /// Interaction logic for WindowNewDishItem.xaml
    /// </summary>
    public partial class WindowNewDishItem : Window
    {
        public static ApllicationOptions options;
        public static NamesList nameList;
        public MainPage parentMainPage;
        public bool editMode { get; private set; }

        public string name;
        public string description;

        public WindowNewDishItem(bool editMode, ApllicationOptions o)
        {
            InitializeComponent();
            this.editMode = editMode;
            options = o;
            //fillText(options.language);
        }
        public WindowNewDishItem(bool editMode, ApllicationOptions o, NamesList w) : this(editMode, o)
        {
            nameList = w;
        }

        public void setParent(MainPage parent)
        {
            parentMainPage = parent;
        }

        public void setName(string name)
        {
            /*newWineWindow.Title = name;
            this.name = name;
            Grid_Name_Value.Visibility = Visibility.Hidden;
            Grid_AvailableOptions_Button_NewItem.Visibility = Visibility.Hidden;
            Grid_Name_Value_Label.Content = name;
            Grid_Name_Value.Text = name;
            Grid_Name_Value_Label.Visibility = Visibility.Visible;*/
        }

        public void setIngredients(string name)
        {
        }

        public void setSpicesMatchings(string name)
        {
        }

        public void setWinesMatchings(string name)
        {
        }

        public void setEditModeVariant()
        {
            /*if (editMode)
            {
                setEditModeVariant_Values(Visibility.Visible);
                setEditModeVariant_Values_Label(Visibility.Hidden);
            }
            else
            {
                setEditModeVariant_Values(Visibility.Hidden);
                setEditModeVariant_Values_Label(Visibility.Visible);
            }*/
        }
    }
}
