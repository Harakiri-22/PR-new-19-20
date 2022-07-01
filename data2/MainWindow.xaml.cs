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
using System.Data.Entity;

namespace data2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        forDataBase2Entities db = forDataBase2Entities.GetContext();

        private void Window_Loaded(object sender, RoutedEventArgs e, Window mainWindow)
        {
            db.table1.Load();
            dataGrid1.ItemsSource = db.table1.Local.ToBindingList();

            Авторизация w = new Авторизация();
            w.Owner = mainWindow;
            w.ShowDialog();

            if (Convert.ToBoolean(Class1.Логин) == false) Close();

            string right;
            if (Class1.Доступ == "A") right = "Администратор";
            else
            {
                right = "Пользователь";

                //btnDelete.IsEnabled = false;
            }

            mainWindow.Title = mainWindow.Title + " " + Class1.Фамилия + " " +
                Class1.Имя + "(" + right + ")";

        }

        private void bb1_Click(object sender, RoutedEventArgs e)
        {
            Add f = new Add();
            f.ShowDialog();
            dataGrid1.Focus();

            db.table1.Load();
            dataGrid1.ItemsSource = db.table1.ToList();
            dataGrid1.ItemsSource = db.table1.Local.ToBindingList();
        }

        private void bb2_Click(object sender, RoutedEventArgs e)
        {
            int indexRow = dataGrid1.SelectedIndex;
            if (indexRow != -1)
            {
                table1 row = (table1)dataGrid1.Items[indexRow];

                Data.Код_обуви = row.Код_обуви;
                Data.Артикул_обуви = row.Артикул_обуви;
                Data.Наименование_обуви = row.Наименование_обуви;
                Data.Количество_пар = row.Количество_пар;
                Data.Стоимость_одной_пары = row.Стоимость_одной_пары;
                Data.Имеющиеся_размеры = row.Имеющиеся_размеры;
                Data.Название_фабрики = row.Название_фабрики;
                Data.Срок_поставки_в_магазин = row.Срок_поставки_в_магазин;

                Edit f = new Edit();
                f.ShowDialog();

                dataGrid1.Items.Refresh();
                dataGrid1.Focus();
            }
        }

        private void bb3_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("удалить запись?", "удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    table1 row = (table1)dataGrid1.SelectedItems[0];

                    db.table1.Remove(row);
                    db.SaveChanges();
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("выберите запись");
                }
            }
        }

        private void bbZAP_Click(object sender, RoutedEventArgs e)
        {
            var article1 = from p in db.table1
                           where p.Артикул_обуви.StartsWith("g")
                           select p.Артикул_обуви;
            dataGrid1.ItemsSource = article1.ToList();

            var article2 = from p in db.table1
                           where p.Артикул_обуви.StartsWith("g")
                           select new
                           {
                               Артикул_обуви = p.Артикул_обуви,
                               Наименование_обуви = p.Наименование_обуви
                           };
            dataGrid1.ItemsSource = article2.ToList();

            var article3 = db.table1.Select(p => p.Артикул_обуви);
        }

        private void bbZAP2_Click(object sender, RoutedEventArgs e)
        {
            var Fabrica1 = from p in db.table1
                           where p.Название_фабрики.StartsWith("F")
                           select p.Название_фабрики;
            dataGrid1.ItemsSource = Fabrica1.ToList();

            var Fabrica2 = from p in db.table1
                           where p.Название_фабрики.StartsWith("F")
                           select new
                           {
                               Название_фабрики = p.Название_фабрики,
                               Срок_поставки_в_магазин = p.Срок_поставки_в_магазин
                           };
            dataGrid1.ItemsSource = Fabrica2.ToList();

            var Fabrica3 = db.table1.Select(p => p.Название_фабрики);
        }

        private void bbZAP3_Click(object sender, RoutedEventArgs e)
        {
            var sotr1 = from p in db.table1
                        orderby p.Стоимость_одной_пары
                        select p;
            sotr1 = from p in db.table1
                    orderby p.Стоимость_одной_пары, p.Количество_пар descending
                    select p;
            dataGrid1.ItemsSource = sotr1.ToList();
        }

        private void bbZAP4_Click(object sender, RoutedEventArgs e)
        {
            var kol1 = from p in db.table1
                       select new { Count = db.table1.Count() };
            int Count = db.table1.Count();

            var kol2 = from p in db.table1
                       group p by p.Количество_пар into g
                       select new { Количество_пар = g.Key, Count = g.Count() };
            dataGrid1.ItemsSource = kol2.ToList();
        }

        private void bbZAP5_Click(object sender, RoutedEventArgs e)
        {
            var para1 = from p in db.table1
                        select new { Average = db.table1.Max(g => g.Стоимость_одной_пары) };
            int max = (int)db.table1.Max(p => p.Стоимость_одной_пары);

            var para2 = from p in db.table1
                        group p by p.Стоимость_одной_пары into g
                        select new { Стоимость_одной_пары = g.Key, Max = g.Max(p => p.Стоимость_одной_пары) };
            dataGrid1.ItemsSource = para2.ToList();
        }
    }
}
