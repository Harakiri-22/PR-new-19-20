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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }

        forDataBase2Entities db = forDataBase2Entities.GetContext();

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (tt1.Text.Length == 0)
            {
                errors.AppendLine("введите код обуви!");
            }

            if (tt2.Text.Length == 0)
            {
                errors.AppendLine("введите артикул обуви!");
            }

            if (tt3.Text.Length == 0)
            {
                errors.AppendLine("введите наименование обуви!");
            }

            if (tt4.Text.Length == 0)
            {
                errors.AppendLine("введите количество пар!");
            }

            if (tt5.Text.Length == 0)
            {
                errors.AppendLine("введите стоимость одной пары!");
            }

            if (tt6.Text.Length == 0)
            {
                errors.AppendLine("введите имеющиеся размеры!");
            }

            if (tt7.Text.Length == 0)
            {
                errors.AppendLine("введите название фабрики!");
            }

            if (tt8.Text.Length == 0)
            {
                errors.AppendLine("введите срок поставки!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            table1 p1 = new table1();

            p1.Код_обуви = Convert.ToInt32(tt1.Text);
            p1.Артикул_обуви = Convert.ToString(tt2.Text);
            p1.Наименование_обуви = Convert.ToString(tt3.Text);
            p1.Количество_пар = Convert.ToInt32(tt4.Text);
            p1.Стоимость_одной_пары = Convert.ToInt32(tt5.Text);
            p1.Имеющиеся_размеры = Convert.ToInt32(tt6.Text);
            p1.Название_фабрики = Convert.ToString(tt7.Text);
            p1.Срок_поставки_в_магазин = Convert.ToDateTime(tt8.Text);

            try
            {
                db.table1.Add(p1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
