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
using RagaliaHotelMS.model.dummy;

namespace RagaliaHotelMS.vandc
{
    /// <summary>
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {

        private Login login = new Login();

        public Auth()
        {
            InitializeComponent();
        }

        private void Authenticate(object sender, RoutedEventArgs e)
        {

            if (authUsername.Text.Equals("") || authPassword.Password.Equals(""))
            {
                MessageBox.Show("User name or password is null");
            }
            else {

                // if login was successfull
                if (login.authenticate(authUsername.Text, authPassword.Password))
                {
                    Main main = new Main();
                    main.Show();
                    this.Close();
                }
                // un autherized user
                else
                {
                    MessageBox.Show("User name or password is incorrect!");
                }
            }

            
        }

       
    }
}
