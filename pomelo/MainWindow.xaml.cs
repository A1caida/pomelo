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
using Microsoft.EntityFrameworkCore;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

namespace bdpomela
{

    public class ApplicationContext : DbContext
    {
        public DbSet<User> kek/*тут название таблицы*/ { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=;database=a1caida"//database - название бд
            );
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void LettersOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Char.IsDigit(e.Text, 0);
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            int age = Convert.ToInt32(Age.Text);

            using (ApplicationContext db = new ApplicationContext())
            {
                User user1 = new User { Name = name, Age = age };

                db.kek.AddRange(user1);
                db.SaveChanges();

                MessageBox.Show("ok");
            }
        }

        private void cout(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.kek.ToList();
                view.Text = "";
                foreach (User u in users)
                {
                    view.Text += $"{u.Id}.{u.Name} - {u.Age}\n";                  
                }
            }
        }

    }
}