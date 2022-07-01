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

namespace data2
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public Edit()
        {
            InitializeComponent();
        }


        forDataBase2Entities db = forDataBase2Entities.GetContext();

        table1 p1;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            p1 = db.table1.Where(p => p.Код_обуви == Data.Код_обуви).FirstOrDefault();

            tb1.Text = Convert.ToString(p1.Код_обуви);
            tb2.Text = Convert.ToString(p1.Артикул_обуви);
            tb3.Text = Convert.ToString(p1.Наименование_обуви);
            tb4.Text = Convert.ToString(p1.Количество_пар);
            tb5.Text = Convert.ToString(p1.Стоимость_одной_пары);
            tb6.Text = Convert.ToString(p1.Имеющиеся_размеры);
            tb7.Text = Convert.ToString(p1.Название_фабрики);
            tb8.Text = Convert.ToString(p1.Срок_поставки_в_магазин);
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (tb1.Text.Length == 0)
            {
                errors.AppendLine("введите код обуви!");
            }

            if (tb2.Text.Length == 0)
            {
                errors.AppendLine("введите артикул обуви!");
            }

            if (tb3.Text.Length == 0)
            {
                errors.AppendLine("введите наименование обуви!");
            }

            if (tb4.Text.Length == 0)
            {
                errors.AppendLine("введите количество пар!");
            }

            if (tb5.Text.Length == 0)
            {
                errors.AppendLine("введите стоимость одной пары!");
            }

            if (tb6.Text.Length == 0)
            {
                errors.AppendLine("введите имеющиеся размеры!");
            }

            if (tb7.Text.Length == 0)
            {
                errors.AppendLine("введите название фабрики!");
            }

            if (tb8.Text.Length == 0)
            {
                errors.AppendLine("введите срок поставки!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            p1.Код_обуви = Convert.ToInt32(tb1.Text);
            p1.Артикул_обуви = Convert.ToString(tb2.Text);
            p1.Наименование_обуви = Convert.ToString(tb3.Text);
            p1.Количество_пар = Convert.ToInt32(tb4.Text);
            p1.Стоимость_одной_пары = Convert.ToInt32(tb5.Text);
            p1.Имеющиеся_размеры = Convert.ToInt32(tb6.Text);
            p1.Название_фабрики = Convert.ToString(tb7.Text);
            p1.Срок_поставки_в_магазин = Convert.ToDateTime(tb8.Text);

            try
            {
                db.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
