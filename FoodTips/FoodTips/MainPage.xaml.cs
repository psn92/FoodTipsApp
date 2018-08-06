using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using FoodTips.Language;
using FoodTips.GridItems;
using FoodTips.MainPageChilderns;

namespace FoodTips
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public static ApllicationOptions options;
        public static NamesList winesList;
        public static NamesList dishesList;
        public static NamesList spicesList;
        public static CodeTypes codeTypes;

        public MainPage(ApllicationOptions o)
        {
            InitializeComponent();

            options = o;
            codeTypes = new CodeTypes(options.language);

            wineDataGridShow();
            dishDataGridShow();
            spiceDataGridShow();
            fillText(options.language);
        }

        public void wineDataGridShow()
        {
            try
            {
                winesList = new NamesList();
                TabControl_Wines_TabItem_WineData_Grid.ItemsSource = dataLoad<WineGrid>(new WineGrid(options), winesList);
            }
            catch (NameCollisionException exception)
            {
                System.Windows.MessageBox.Show(exception.Message);
                throw exception;
            }
            catch (IndexOutOfRangeException indexException)
            {
                System.Windows.MessageBox.Show("File " + options.wineSource + " is invalid. Not every data lines could be loaded.\n\n" + indexException.ToString());
            }
            dataGridSetWidth(TabControl_Wines_TabItem_WineData_Grid, 650);
        }

        public void TabControl_Wines_TabItem_WineData_Grid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            TabControl_Wines_TabItem_ButtonsPanel_Grid_ShowDetails_Button_Click(sender, e);
        }

        private void TabControl_Wines_TabItem_ButtonsPanel_Grid_NewPosition_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowNewWineItem newWineItem = new WindowNewWineItem(true, options, winesList);
            newWineItem.setParent(this);
            newWineItem.Height = 2 * MainWindow.ActualHeight / 3;
            newWineItem.Width = MainWindow.ActualWidth / 2;
            newWineItem.ShowDialog();
        }

        private void TabControl_Wines_TabItem_ButtonsPanel_Grid_ShowDetails_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowNewWineItem viewWineItem = new WindowNewWineItem(false, options);
            openItemWine(viewWineItem);
        }

        private void TabControl_Wines_TabItem_ButtonsPanel_Grid_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowNewWineItem editWineItem = new WindowNewWineItem(true, options);
            openItemWine(editWineItem);
        }

        private void openItemWine(WindowNewWineItem item)
        {
            DataRowView dataRow = (DataRowView)TabControl_Wines_TabItem_WineData_Grid.SelectedItem;
            try
            {
                item.setName(dataRow.Row.ItemArray[0].ToString());
                if (dataRow.Row.ItemArray[1] != null)
                {
                    item.setVariant(dataRow.Row.ItemArray[1].ToString());
                    if (dataRow.Row.ItemArray[2] != null)
                        item.setSpecification(dataRow.Row.ItemArray[2].ToString());
                }
                if (dataRow.Row.ItemArray[3] != null)
                    item.setRegion(dataRow.Row.ItemArray[3].ToString());
                if (dataRow.Row.ItemArray[4] != null)
                    item.setScore(dataRow.Row.ItemArray[4].ToString());
                if (dataRow.Row.ItemArray[5] != null)
                    item.setDescription(dataRow.Row.ItemArray[5].ToString());
                item.setEditModeVariant();

                item.setParent(this);
                item.Height = 2 * MainWindow.ActualHeight / 3;
                item.Width = MainWindow.ActualWidth / 2;
                item.ShowDialog();
            }
            catch (NullReferenceException nullReference)
            {
                System.Windows.MessageBox.Show(options.language.MainPage_ChooseDatagridPossition());
            }
        }



        public void dishDataGridShow()
        {
            try
            {
                dishesList = new NamesList();
                TabControl_Dishes_TabItem_DishData_Grid.ItemsSource = dataLoad<DishGrid>(new DishGrid(options), dishesList);
            }
            catch (NameCollisionException exception)
            {
                System.Windows.MessageBox.Show(exception.Message);
                throw exception;
            }
            catch (IndexOutOfRangeException indexException)
            {
                System.Windows.MessageBox.Show("File " + options.dishSource + " is invalid. Not every data lines could be loaded.\n\n" + indexException.ToString());
            }
            dataGridSetWidth(TabControl_Dishes_TabItem_DishData_Grid, 800);
        }

        private void TabControl_Dishes_TabItem_DishData_Grid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            TabControl_Dishes_TabItem_ButtonsPanel_Grid_ShowDetails_Button_Click(sender, e);
        }

        private void TabControl_Dishes_TabItem_ButtonsPanel_Grid_NewPosition_Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TabControl_Dishes_TabItem_ButtonsPanel_Grid_ShowDetails_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowNewDishItem viewDishItem = new WindowNewDishItem(false, options);
            openItemDish(viewDishItem);
        }

        private void TabControl_Dishes_TabItem_ButtonsPanel_Grid_Edit_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void openItemDish(WindowNewDishItem item)
        {
            DataRowView dataRow = (DataRowView)TabControl_Dishes_TabItem_DishData_Grid.SelectedItem;
            try
            {
                item.setName(dataRow.Row.ItemArray[0].ToString());
                if (dataRow.Row.ItemArray[1] != null)
                    item.setIngredients(dataRow.Row.ItemArray[1].ToString());
                if (dataRow.Row.ItemArray[2] != null)
                    item.setSpicesMatchings(dataRow.Row.ItemArray[2].ToString());
                if (dataRow.Row.ItemArray[3] != null)
                    item.setWinesMatchings(dataRow.Row.ItemArray[3].ToString());
                item.setEditModeVariant();

                item.setParent(this);
                item.Height = 2 * MainWindow.ActualHeight / 3;
                item.Width = MainWindow.ActualWidth / 2;
                item.ShowDialog();
            }
            catch (NullReferenceException nullReference)
            {
                System.Windows.MessageBox.Show(options.language.MainPage_ChooseDatagridPossition());
            }
        }



        public void spiceDataGridShow()
        {
            try
            {
                spicesList = new NamesList();
                TabControl_Spices_TabItem_SpiceData_Grid.ItemsSource = dataLoad<SpiceGrid>(new SpiceGrid(options), spicesList);
            }
            catch (NameCollisionException exception)
            {
                System.Windows.MessageBox.Show(exception.Message);
                throw exception;
            }
            catch (IndexOutOfRangeException indexException)
            {
                System.Windows.MessageBox.Show("File " + options.spiceSource + " is invalid. Not every data lines could be loaded.\n\n" + indexException.ToString());
            }
            dataGridSetWidth(TabControl_Spices_TabItem_SpiceData_Grid, 800);
        }

        private void TabControl_Spices_TabItem_SpiceData_Grid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            TabControl_Spices_TabItem_ButtonsPanel_Grid_ShowDetails_Button_Click(sender, e);
        }

        private void TabControl_Spices_TabItem_ButtonsPanel_Grid_NewPosition_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_Spices_TabItem_ButtonsPanel_Grid_ShowDetails_Button_Click(object sender, RoutedEventArgs e)
        {

        }



        private DataView dataLoad<T>(T dataGridType, NamesList names) where T : GridType_Parameters, GridType
        {
            DataTable table = dataGridType.columnNamesLoad();

            string[] source = System.IO.File.ReadAllLines(dataGridType.dataSource, Encoding.GetEncoding("Windows-1250"));
            string[] row = null;


            foreach (string fileRow in source)
            {
                row = fileRow.Split(';');

                if (!names.checkIfAlreadyExists(row[0]))
                    names.add(row[0]);
                else
                    throw new NameCollisionException(true, dataGridType.dataSource, row[0]);
                if (dataGridType.specialColumns)
                {
                    try
                    {
                        row = dataGridType.convertSpecialColumns(row, codeTypes);
                    }
                    catch (IndexOutOfRangeException indexException)
                    {
                        System.Windows.MessageBox.Show(row[0] + " has invalid format: " + fileRow);
                        throw indexException;
                    }
                }
                table.Rows.Add(row);
            }

            DataView dv = new DataView(table);
            dv.Sort = dataGridType.sortMethod;

            return dv;
        }
        private void dataGridSetWidth(DataGrid grid, int colLength)
        {
            grid.MaxColumnWidth = colLength;
        }

        private void fillText(DictionaryType d)
        {
            TabControl_Wines_TabItem.Header = d.MainPage_Get_TabControl_Wines_TabItem();
            TabControl_Wines_TabItem_ButtonsPanel_Grid_NewPosition_Button.Content = d.MainPage_Get_TabControl_Wines_TabItem_ButtonsPanel_Grid_NewPosition_Button();
            TabControl_Wines_TabItem_ButtonsPanel_Grid_ShowDetails_Button.Content = d.MainPage_Get_TabControl_Wines_TabItem_ButtonsPanel_Grid_ShowDetails_Button();
            TabControl_Wines_TabItem_ButtonsPanel_Grid_Edit_Button.Content = d.MainPage_Get_TabControl_Wines_TabItem_ButtonsPanel_Grid_Edit_Button();
            TabControl_Dishes_TabItem.Header = d.MainPage_Get_TabControl_Dishes_TabItem();
            TabControl_Dishes_TabItem_ButtonsPanel_Grid_NewPosition_Button.Content = d.MainPage_Get_TabControl_Dishes_TabItem_ButtonsPanel_Grid_NewPosition_Button();
            TabControl_Dishes_TabItem_ButtonsPanel_Grid_ShowDetails_Button.Content = d.MainPage_Get_TabControl_Dishes_TabItem_ButtonsPanel_Grid_ShowDetails_Button();
            TabControl_Dishes_TabItem_ButtonsPanel_Grid_Edit_Button.Content = d.MainPage_Get_TabControl_Dishes_TabItem_ButtonsPanel_Grid_Edit_Button();
            TabControl_Spices_TabItem.Header = d.MainPage_Get_TabControl_Spices_TabItem();
            TabControl_Spices_TabItem_ButtonsPanel_Grid_NewPosition_Button.Content = d.MainPage_Get_TabControl_Spices_TabItem_ButtonsPanel_Grid_NewPosition_Button();
            TabControl_Spices_TabItem_ButtonsPanel_Grid_ShowDetails_Button.Content = d.MainPage_Get_TabControl_Spices_TabItem_ButtonsPanel_Grid_ShowDetails_Button();
            TabControl_Spices_TabItem_ButtonsPanel_Grid_Edit_Button.Content = d.MainPage_Get_TabControl_Spices_TabItem_ButtonsPanel_Grid_Edit_Button();
        }
    }



    interface GridType
    {
        DataTable columnNamesLoad();
        string[] convertSpecialColumns(string[] row, CodeTypes codeTypes);
    }
    public class GridType_Parameters
    {
        public ApllicationOptions ao;
        public string dataSource;
        public bool specialColumns;
        public string sortMethod;

        public GridType_Parameters(ApllicationOptions ao)
        {
            this.ao = ao;
        }
    }
    public class WineGrid : GridType_Parameters, GridType
    {
        public WineGrid(ApllicationOptions ao) : base(ao)
        {
            dataSource = "data\\" + ao.wineSource;
            specialColumns = true;
            sortMethod = ao.language.WindowNewWineItem_Get_Grid_Score_Label() + " DESC, "
                + ao.language.WindowNewWineItem_Get_Grid_Name_Label() + " ASC";
        }

        public DataTable columnNamesLoad()
        {
            DataTable wineTable = new DataTable();

            wineTable.Columns.Add(ao.language.WindowNewWineItem_Get_Grid_Name_Label(), typeof(string));
            wineTable.Columns.Add(ao.language.WindowNewWineItem_Get_Grid_Variant_Label(), typeof(string));
            wineTable.Columns.Add(ao.language.WindowNewWineItem_Get_Grid_Specification_Label(), typeof(string));
            wineTable.Columns.Add(ao.language.WindowNewWineItem_Get_Grid_Region_Label(), typeof(string));
            wineTable.Columns.Add(ao.language.WindowNewWineItem_Get_Grid_Score_Label(), typeof(string));
            wineTable.Columns.Add(ao.language.WindowNewWineItem_Get_Grid_Description_Label(), typeof(string));

            return wineTable;
        }

        public string[] convertSpecialColumns(string[] row, CodeTypes codeTypes)
        {
            row[1] = codeTypes.WineVariantCodeToString(int.Parse(row[1]));
            row[2] = codeTypes.WineSpecificationCodeToString(int.Parse(row[2]));

            return row;
        }
    }
    public class DishGrid : GridType_Parameters, GridType
    {
        public DishGrid(ApllicationOptions ao) : base(ao)
        {
            dataSource = "data\\" + ao.dishSource;
            specialColumns = false;
            sortMethod = ao.language.WindowNewWineItem_Get_Grid_Name_Label() + " ASC";
        }

        public DataTable columnNamesLoad()
        {
            DataTable spiceTable = new DataTable();

            spiceTable.Columns.Add(ao.language.WindowNewDishItem_Get_Grid_Name_Label(), typeof(string));
            spiceTable.Columns.Add(ao.language.WindowNewDishItem_Get_Grid_Ingredients_Label(), typeof(string));
            spiceTable.Columns.Add(ao.language.WindowNewDishItem_Get_Grid_SpicesMatchings_Label(), typeof(string));
            spiceTable.Columns.Add(ao.language.WindowNewDishItem_Get_Grid_WinesMatchings_Label(), typeof(string));

            return spiceTable;
        }

        public string[] convertSpecialColumns(string[] row, CodeTypes codeTypes)
        {
            return row;
        }
    }
    public class SpiceGrid : GridType_Parameters, GridType
    {
        public SpiceGrid(ApllicationOptions ao) : base(ao)
        {
            dataSource = "data\\" + ao.spiceSource;
            specialColumns = false;
            sortMethod = ao.language.WindowNewSpiceItem_Get_Grid_Name_Label() + " ASC";
        }

        public DataTable columnNamesLoad()
        {
            DataTable spiceTable = new DataTable();

            spiceTable.Columns.Add(ao.language.WindowNewSpiceItem_Get_Grid_Name_Label(), typeof(string));
            spiceTable.Columns.Add(ao.language.WindowNewSpiceItem_Get_Grid_Matchings_Label(), typeof(string));
            spiceTable.Columns.Add(ao.language.WindowNewSpiceItem_Get_Grid_Description_Label(), typeof(string));
            spiceTable.Columns.Add(ao.language.WindowNewSpiceItem_Get_Grid_LinkToSource_Label(), typeof(string));

            return spiceTable;
        }

        public string[] convertSpecialColumns(string[] row, CodeTypes codeTypes)
        {
            return row;
        }
    }



    public class NameCollisionException : Exception
    {
        public NameCollisionException() : base("Source file is invalid. At least two positions have the same name.\n" +
            "Please go to options and set correct file (you are allow to use empty file).") { }

        public NameCollisionException(bool addFileName, string fileName, string wineName) : base("Source file (\"" + fileName +
            "\") is invalid. At least two positions have the same name: '" + wineName + "'.\nPlease go to options and set correct file (you are allow to use empty file).") { }

        public NameCollisionException(string message) : base(message) { }
    }
}
