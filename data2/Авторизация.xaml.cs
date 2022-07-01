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
using System.Data.Entity;

namespace data2
{
    /// <summary>
    /// Логика взаимодействия для Авторизация.xaml
    /// </summary>
    public partial class Авторизация : Window
    {
        public Авторизация()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //txtLogin.Focus();
            _ = Convert.ToBoolean(Class1.Логин) == false;
        }

        data2Entities db = data2Entities.GetContext();

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            var user = from p in db.Авторизация
                       where p.Логин == t1.Text && Convert.ToString(p.Пароль) == t2.Text
                       select p;

            if (user.Count() == 1)
            {
                Class1.Логин = Convert.ToString(true);
                Class1.Фамилия = user.First().Фамилия;
                Class1.Имя = user.First().Имя;
                Class1.Доступ = user.First().Доступ;
                Close();
            }
            else
            {
                MessageBox.Show("логин или пароль не верны! повторите ввод");
                t1.Focus();
            }
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
