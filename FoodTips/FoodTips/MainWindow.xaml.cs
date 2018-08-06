using FoodTips.Language;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodTips
{
    public partial class MainWindow : Window
    {
        public static ApllicationOptions options;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Openning.WindowState = WindowState.Maximized;
                try
                {
                    options = new ApllicationOptions();
                }
                catch (OptionFileNotExistException exception)
                {
                    System.Windows.MessageBox.Show(exception.Message);
                    throw exception;
                }
                fillText(options.language);
                fillTextColor((SolidColorBrush)new BrushConverter().ConvertFromString(options.fontColor));
                fillDropDowns();
            }
            catch (Exception exception)
            {
                System.Windows.MessageBox.Show("We apologise you but the program cannot be open.\n\n" + exception.ToString());
                throw new Exception();
            }
        }

        public void CloseStartingPage_Key(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.KeyDown -= CloseStartingPage_Key;
                CloseStartingPage();
            }
        }
        public void CloseStartingPage_Click(object sender, RoutedEventArgs e)
        {
            CloseStartingPage();
        }
        public void CloseStartingPage()
        {
            this.Title = options.language.MainPage_Get_MainWindow(); ;
            StartingPage.Visibility = Visibility.Hidden;
            this.ContentControl.Content = new MainPage(options);
        }

        public void GoToOptions_Click(object sender, RoutedEventArgs e)
        {
            this.KeyDown -= CloseStartingPage_Key;
            Grid_Openning.Visibility = Visibility.Hidden;
            Grid_Options.Visibility = Visibility.Visible;
        }

        public void Options_ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string[] optionsSet = new string[6];
            optionsSet[0] = Grid_Options_Language_Value.Text;
            optionsSet[1] = Grid_Options_FontColor_Value.Text;
            optionsSet[2] = Grid_Options_Background_Value.Text;
            optionsSet[3] = Grid_Options_Wines_Value.Text;
            optionsSet[4] = Grid_Options_Dishes_Value.Text;
            optionsSet[5] = Grid_Options_pices_Value.Text;

            options.newOptionSet(optionsSet);
            fillText(options.language);
            fillTextColor((SolidColorBrush)new BrushConverter().ConvertFromString(options.fontColor));

            System.Windows.MessageBox.Show(options.language.MainWindow_AfterOptionsSave());
        }

        public void Options_ButtonsReturn_Click(object sender, RoutedEventArgs e)
        {
            this.KeyDown += CloseStartingPage_Key;
            Grid_Openning.Visibility = Visibility.Visible;
            Grid_Options.Visibility = Visibility.Hidden;
        }


        
        private void fillText(DictionaryType d)
        {
            Openning.Title = d.MainWindow_Get_Openning();
            Button_GoToApp.Content = d.MainWindow_Get_Button_GoToApp();
            Button_Options.Content = d.MainWindow_Get_Button_Options();
            TextBlock_keyEnter.Text = d.MainWindow_Get_TextBlock_keyEnter();
            Grid_Options_Name.Content = d.MainWindow_Get_Grid_Options_Name();
            Grid_Options_Language_Label.Content = d.MainWindow_Get_Grid_Options_Language_Label();
            Grid_Options_FontColor_Label.Content = d.MainWindow_Get_Grid_Options_FontColor_Label();
            Grid_Options_Background_Label.Content = d.MainWindow_Get_Grid_Options_Background_Label();
            Grid_Options_Background_Value.Text = options.background;
            fillBackground();
            Grid_Options_Wines_Label.Content = d.MainWindow_Get_Grid_Options_Wines_Label();
            Grid_Options_Wines_Value.Text = options.wineSource;
            Grid_Options_Dishes_Label.Content = d.MainWindow_Get_Grid_Options_Dishes_Label();
            Grid_Options_Dishes_Value.Text = options.dishSource;
            Grid_Options_pices_Label.Content = d.MainWindow_Get_Grid_Options_Spices_Label();
            Grid_Options_pices_Value.Text = options.spiceSource;
            Grid_Options_ButtonSave.Content = d.MainWindow_Get_Grid_Options_ButtonSave();
            Grid_Options_ButtonsReturn.Content = d.MainWindow_Get_Grid_Options_ButtonsReturn();
        }
        private void fillBackground()
        {
            try
            {
                ContentControl_ImageBrush.ImageSource = new BitmapImage(new Uri(options.background));
            }
            catch (System.IO.FileNotFoundException fileException)
            {
                ContentControl_ImageBrush.ImageSource = new BitmapImage(new Uri("Images\\defaultBackground.jpg", UriKind.Relative));
                System.Windows.MessageBox.Show(options.language.MainWindow_BackgroundFileNotFound());
            }
            catch (Exception exception)
            {
                System.Windows.MessageBox.Show("Cannot find default image.\n" + exception.ToString());
            }
        }
        private void fillDropDowns()
        {
            fill_Grid_Options_Language_Value();
            fill_Grid_Options_FontColor_Value();
        }
        private void fill_Grid_Options_Language_Value()
        {
            foreach(string l in options.map_language.Values)
                Grid_Options_Language_Value.Items.Add(l);
            Grid_Options_Language_Value.Text = options.languageName;
        }
        private void fill_Grid_Options_FontColor_Value()
        {
            foreach (var fc in Enum.GetValues(typeof(ApllicationOptions.enum_fontColor)))
                Grid_Options_FontColor_Value.Items.Add(fc.ToString());
            Grid_Options_FontColor_Value.Text = options.fontColor;
        }
        private void fillTextColor(SolidColorBrush c)
        {
            Button_GoToApp.Foreground = c;
            Button_Options.Foreground = c;
            TextBlock_keyEnter.Foreground = c;
            Grid_Options_Name.Foreground = c;
            Grid_Options_Language_Label.Foreground = c;
            Grid_Options_FontColor_Label.Foreground = c;
            Grid_Options_Background_Label.Foreground = c;
            Grid_Options_Wines_Label.Foreground = c;
            Grid_Options_Dishes_Label.Foreground = c;
            Grid_Options_pices_Label.Foreground = c;
            Grid_Options_ButtonSave.Foreground = c;
            Grid_Options_ButtonsReturn.Foreground = c;
            /*
            Button_GoToApp.Foreground = c;
            Button_Options.Foreground = c;
            TextBlock_keyEnter.Foreground = c;
            Grid_Options_Name.Foreground = c;
            Grid_Options_Language_Label.Foreground = c;
            Grid_Options_Language_Value.Foreground = c;
            Grid_Options_FontColor_Label.Foreground = c;
            Grid_Options_FontColor_Value.Foreground = c;
            Grid_Options_Background_Label.Foreground = c;
            Grid_Options_Background_Value.Foreground = c;
            Grid_Options_Wines_Label.Foreground = c;
            Grid_Options_Wines_Value.Foreground = c;
            Grid_Options_Dishes_Label.Foreground = c;
            Grid_Options_Dishes_Value.Foreground = c;
            Grid_Options_pices_Label.Foreground = c;
            Grid_Options_pices_Value.Foreground = c;
            Grid_Options_ButtonSave.Foreground = c;
            Grid_Options_ButtonsReturn.Foreground = c;
             */
        }

        private void Grid_Options_Background_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*." 
                + string.Join(";*.", options.imageExtenctions) + ")|*."
                + string.Join(";*.", options.imageExtenctions);
            if (openFileDialog.ShowDialog() == true)
                Grid_Options_Background_Value.Text = openFileDialog.FileName;
        }
    }
}
