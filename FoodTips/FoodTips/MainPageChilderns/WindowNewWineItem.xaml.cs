using FoodTips.GridItems;
using FoodTips.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for WindowNewWineItem.xaml
    /// </summary>
    public partial class WindowNewWineItem : Window
    {
        public static ApllicationOptions options;
        public static NamesList winesList;
        private MainPage parentMainPage;
        public static CodeTypes codeTypes = new CodeTypes();
        public bool editMode { get; private set; }

        public string name;
        public int variant;
        public int specification;
        public string region;
        public string score;
        public string description;

        public WindowNewWineItem(bool editMode, ApllicationOptions o)
        {
            InitializeComponent();
            this.editMode = editMode;
            options = o;
            fillText(options.language);
        }
        public WindowNewWineItem(bool editMode, ApllicationOptions o, NamesList w) : this(editMode, o)
        {
            winesList = w;
        }

        public void setParent(MainPage parent)
        {
            parentMainPage = parent;
        }

        public void setName(string name)
        {
            newWineWindow.Title = name;
            this.name = name;
            Grid_Name_Value.Visibility = Visibility.Hidden;
            Grid_AvailableOptions_Button_NewItem.Visibility = Visibility.Hidden;
            Grid_Name_Value_Label.Content = name;
            Grid_Name_Value.Text = name;
            Grid_Name_Value_Label.Visibility = Visibility.Visible;
        }

        public void setVariant(string variant)
        {
            Grid_Variant_Value_Label.Content = variant;

            Grid_Variant_Value.Text = variant;
            Grid_Variant_Value_LostFocus(new object(), new RoutedEventArgs());
        }

        public void setSpecification(string specification)
        {
            Grid_Specification_Value_Label.Content = specification;

            if (!Grid_Variant_Value.Text.Equals(options.language.WindowNewWineItem_Get_Grid_Variant_Value_Mead()))
                Grid_Specification_Value_Wine.Text = specification;
            else
                Grid_Specification_Value_Mead.Text = specification;
        }

        public void setRegion(string region)
        {
            Grid_Region_Value_Label.Content = region;

            this.region = region;
            Grid_Region_Value.Text = region;
        }

        public void setScore(string score)
        {
            Grid_Score_Value_Label.Content = score;

            this.score = score;
            if (!score.Equals(""))
            {
                Grid_Score_Value_Slider.Value = double.Parse(score, System.Globalization.CultureInfo.InvariantCulture);
                Grid_Score_Value_Slider_GotFocus(new object(), new RoutedEventArgs());
            }
        }

        public void setDescription(string description)
        {
            Grid_Description_Value_Label.Text = description;

            this.description = description;
            Grid_Description_Value.Text = description;
        }

        public void setEditModeVariant()
        {
            if (editMode)
            {
                setEditModeVariant_Values(Visibility.Visible);
                setEditModeVariant_Values_Label(Visibility.Hidden);
            }
            else
            {
                setEditModeVariant_Values(Visibility.Hidden);
                setEditModeVariant_Values_Label(Visibility.Visible);
            }
        }

        private void setEditModeVariant_Values(Visibility v)
        {
            Grid_AvailableOptions_Button_Delete.Visibility = v;
            Grid_AvailableOptions_Button_Save.Visibility = v;
            Grid_Variant_Value.Visibility = v;
            if (!Grid_Variant_Value_Label.Content.Equals(options.language.WindowNewWineItem_Get_Grid_Variant_Value_Mead()))
                Grid_Specification_Value_Wine.Visibility = v;
            else
                Grid_Specification_Value_Mead.Visibility = v;
            Grid_Region_Value.Visibility = v;
            Grid_Score_Value_Slider.Visibility = v;
            if (score != null)
                Grid_Score_Value.Visibility = v;
            Grid_Description_Value.Visibility = v;
        }

        private void setEditModeVariant_Values_Label(Visibility v)
        {
            Grid_AvailableOptions_Button_Edit.Visibility = v;
            Grid_Variant_Value_Label.Visibility = v;
            Grid_Specification_Value_Label.Visibility = v;
            Grid_Region_Value_Label.Visibility = v;
            Grid_Score_Value_Label.Visibility = v;
            Grid_Description_Value_Label.Visibility = v;
        }

        private void Grid_Name_Value_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckReservedSymbols_KeyDown(sender, e);

            string nameValue = Grid_Name_Value.Text;

            if (nameValue.Equals("") || winesList.checkIfAlreadyExists(nameValue))
            {
                Grid_AvailableOptions_Button_NewItem.IsEnabled = false;
                Grid_Name_Label.Content = options.language.WindowNewWineItem_Get_Grid_Name_Label_AlreadyAdded();
                Grid_Name_Label.Foreground = Brushes.Red;
                Grid_Name_Label.FontSize = 12;
            }
            else
            {
                Grid_AvailableOptions_Button_NewItem.IsEnabled = true;
                Grid_Name_Label.Content = options.language.WindowNewWineItem_Get_Grid_Name_Label();
                Grid_Name_Label.Foreground = Brushes.Black;
                Grid_Name_Label.FontSize = 15;
            }
        }

        private void Grid_Variant_Value_LostFocus(object sender, RoutedEventArgs e)
        {
            string variantValue = Grid_Variant_Value.Text;
            if (Grid_Variant_Value.Text == "")
                return ;

            if (!Grid_Specification_Value_Wine.IsEnabled)
                Grid_Specification_Value_Wine.IsEnabled = true;

            if (!variantValue.Equals(options.language.WindowNewWineItem_Get_Grid_Variant_Value_Mead()))
            {
                if (Grid_Specification_Value_Mead.Visibility == Visibility.Visible)
                {
                    Grid_Specification_Value_Mead.SelectedItem = null;
                    Grid_Specification_Value_Mead.Visibility = Visibility.Hidden;
                    Grid_Specification_Value_Wine.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (Grid_Specification_Value_Wine.Visibility == Visibility.Visible)
                {
                    Grid_Specification_Value_Wine.SelectedItem = null;
                    Grid_Specification_Value_Wine.Visibility = Visibility.Hidden;
                    Grid_Specification_Value_Mead.Visibility = Visibility.Visible;
                }
            }
        }

        private void Grid_Score_Value_Slider_GotFocus(object sender, RoutedEventArgs e)
        {
            Grid_Score_Value_Slider.Margin = new Thickness(170, 7, 25, 0);
            Grid_Score_Value_Slider.Width = 425;
            Grid_Score_Value_Slider.ValueChanged += Grid_Score_Value_Slider_ValueChanged;
            Grid_Score_Value_Slider.GotFocus -= Grid_Score_Value_Slider_GotFocus;

            Grid_Score_Value_Slider_ChangeValue();
        }

        private void Grid_Score_Value_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Grid_Score_Value_Slider_ChangeValue();
        }

        private void Grid_Score_Value_Slider_ChangeValue()
        {
            Grid_Score_Value.Content = Grid_Score_Value_Slider.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public void Grid_AvailableOptions_Button_NewItem_Click(object sender, RoutedEventArgs e)
        {
            refreshFieldsContent();
            insertRow();
            parentMainPage.wineDataGridShow();
            newWineWindow.Close();
        }

        private void Grid_AvailableOptions_Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if(System.Windows.MessageBox.Show(options.language.MainPage_Get_DeleteQuestion(),
                options.language.MainPage_Get_DeleteWindowTitle(),
                System.Windows.MessageBoxButton.OKCancel)
                == MessageBoxResult.Cancel)
                return;
            string content;
            using (StreamReader sr = new StreamReader("data\\" + options.wineSource, Encoding.GetEncoding("Windows-1250")))
                content = sr.ReadToEnd().ToString();

            string nameEscaped = Regex.Escape(name);
            string regex = "\n?(" + nameEscaped + "(;[^;\n]*){5}\n?)";
            Match match = Regex.Match(content, regex);
            content = content.Replace(match.Groups[1].Value, "");
            File.WriteAllText("data\\" + options.wineSource, content, Encoding.GetEncoding("Windows-1250"));
            parentMainPage.wineDataGridShow();
            newWineWindow.Close();
        }

        private void Grid_AvailableOptions_Button_Save_Click(object sender, RoutedEventArgs e)
        {
            string content;
            using (StreamReader sr = new StreamReader("data\\" + options.wineSource, Encoding.GetEncoding("Windows-1250")))
                content = sr.ReadToEnd().ToString();

            string nameEscaped = Regex.Escape(name);
            string regex = "\n?(" + nameEscaped + "(;[^;\n]*){5})";
            Match match = Regex.Match(content, regex);
            refreshFieldsContent();
            content = content.Replace(match.Groups[1].Value, preperLine());
            File.WriteAllText("data\\" + options.wineSource, content, Encoding.GetEncoding("Windows-1250"));
            parentMainPage.wineDataGridShow();
            newWineWindow.Close();
        }

        private void Grid_AvailableOptions_Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            editMode = true;
            setEditModeVariant();
        }

        private string preperLine()
        {
            StringBuilder rowContent = new StringBuilder(name);
            rowContent.Append(";").Append(variant).Append(";").Append(specification).Append(";").
                Append(region).Append(";").Append(score).Append(";").Append(description);

            return rowContent.ToString();
        }

        private void refreshFieldsContent()
        {
            name = Grid_Name_Value.Text;

            if (Grid_Variant_Value.SelectedItem != null)
                variant = codeTypes.WineVariant[((ComboBoxItem)Grid_Variant_Value.SelectedItem).Name];
            else variant = 0;

            if (Grid_Specification_Value_Wine.SelectedItem != null)
                specification = codeTypes.WineSpecification[((ComboBoxItem)Grid_Specification_Value_Wine.SelectedItem).Name];
            else if (Grid_Specification_Value_Mead.SelectedItem != null)
                specification = codeTypes.WineSpecification[((ComboBoxItem)Grid_Specification_Value_Mead.SelectedItem).Name];
            else specification = 0;

            region = Grid_Region_Value.Text;

            if (Grid_Score_Value.Content != null)
                score = Grid_Score_Value.Content.ToString();
            else
                score = "";

            description = Grid_Description_Value.Text;
        }

        private void insertRow()
        {
            string content = File.ReadAllText("data\\" + options.wineSource);
            char lastSymbol = content[content.Length - 1];
            
            string rowToInsert;
            if (lastSymbol == '\n')
                rowToInsert = preperLine();
            else
                rowToInsert = Environment.NewLine + preperLine();
            File.AppendAllText("data\\" + options.wineSource, rowToInsert + Environment.NewLine, Encoding.GetEncoding("Windows-1250"));
        }



        private void fillText(DictionaryType d)
        {
            newWineWindow.Title = d.WindowNewWineItem_Get_newWineWindow();
            Grid_Name_Label.Content = d.WindowNewWineItem_Get_Grid_Name_Label() + ":";
            Grid_Variant_Label.Content = d.WindowNewWineItem_Get_Grid_Variant_Label() + ":";
            Grid_Variant_Value_Red.Content = d.WindowNewWineItem_Get_Grid_Variant_Value_Red();
            Grid_Variant_Value_Rose.Content = d.WindowNewWineItem_Get_Grid_Variant_Value_Rose();
            Grid_Variant_Value_White.Content = d.WindowNewWineItem_Get_Grid_Variant_Value_White();
            Grid_Variant_Value_Mead.Content = d.WindowNewWineItem_Get_Grid_Variant_Value_Mead();
            Grid_Specification_Label.Content = d.WindowNewWineItem_Get_Grid_Specification_Label() + ":";
            Grid_Specification_Value_Dry.Content = d.WindowNewWineItem_Get_Grid_Specification_Value_Dry();
            Grid_Specification_Value_SemiDry.Content = d.WindowNewWineItem_Get_Grid_Specification_Value_SemiDry();
            Grid_Specification_Value_SemiSweet.Content = d.WindowNewWineItem_Get_Grid_Specification_Value_SemiSweet();
            Grid_Specification_Value_Sweet.Content = d.WindowNewWineItem_Get_Grid_Specification_Value_Sweet();
            Grid_Specification_Value_Poltorak.Content = d.WindowNewWineItem_Get_Grid_Specification_Value_Poltorak();
            Grid_Specification_Value_Dwojniak.Content = d.WindowNewWineItem_Get_Grid_Specification_Value_Dwojniak();
            Grid_Specification_Value_Trojniak.Content = d.WindowNewWineItem_Get_Grid_Specification_Value_Trojniak();
            Grid_Specification_Value_Czworniak.Content = d.WindowNewWineItem_Get_Grid_Specification_Value_Czworniak();
            Grid_Region_Label.Content = d.WindowNewWineItem_Get_Grid_Region_Label() + ":";
            Grid_Score_Label.Content = d.WindowNewWineItem_Get_Grid_Score_Label() + ":";
            Grid_Description_Label.Content = d.WindowNewWineItem_Get_Grid_Description_Label() + ":";
            Grid_AvailableOptions_Button_NewItem.Content = d.WindowNewWineItem_Get_Grid_AvailableOptions_Button_NewItem();
            Grid_AvailableOptions_Button_Delete.Content = d.MainWindow_Get_Grid_Options_ButtonDelete();
            Grid_AvailableOptions_Button_Save.Content = d.MainWindow_Get_Grid_Options_ButtonSave();
            Grid_AvailableOptions_Button_Edit.Content = d.MainPage_Get_TabControl_Wines_TabItem_ButtonsPanel_Grid_Edit_Button();
        }

        private void CheckReservedSymbols_KeyDown(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Contains(";"))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.ToString().Replace(";", "");
                System.Windows.MessageBox.Show(options.language.WindowNewWineItem_ReservedSymbols());
            }
        }
    }
}
