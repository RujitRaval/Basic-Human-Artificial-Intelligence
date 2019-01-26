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
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using Microsoft.Samples.Kinect.ControlsBasics.DataModel;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private kinectdataEntities context = new kinectdataEntities();

        public StartWindow()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserLogIn usr = new UserLogIn();

            usr.Email = textBoxEmail.Text;
            usr.Password = passwordBox1.Password.ToString();


            var result = context.UserLogIns.Where(t => t.Email == textBoxEmail.Text && t.Password == passwordBox1.Password.ToString()).FirstOrDefault();
            if (result == null)
            {
               // MessageBox.Show("Email / Password is invalid.");
                errormessage.Text = "Enter valid Email Id or Password.";
            }
            else
            {
                MainWindow win2 = new MainWindow();
                win2.Show();
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogInPopup.IsOpen = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SignUpPopup.IsOpen = true;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
           
            if (textBoxFirstName.Text.Length == 0)
            {
                errormessage1.Text = "Enter First Name.";
                textBoxFirstName.Focus();
            }
            else if (textBoxLastName.Text.Length == 0)
            {
                errormessage1.Text = "Enter Last Name.";
                textBoxLastName.Focus();
            }
            else if (textBoxEmail1.Text.Length == 0)
            {
                errormessage1.Text = "Enter an email.";
                textBoxEmail1.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail1.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage1.Text = "Enter a valid email.";
                textBoxEmail1.Select(0, textBoxEmail1.Text.Length);
                textBoxEmail1.Focus();
            }
            else
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail1.Text;
                string password = passwordBox2.Password;
                if (passwordBox2.Password.Length == 0)
                {
                    errormessage1.Text = "Enter password.";
                    passwordBox2.Focus();
                }
                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    errormessage1.Text = "Enter Confirm password.";
                    passwordBoxConfirm.Focus();
                }
                else if (passwordBox2.Password != passwordBoxConfirm.Password)
                {
                    errormessage1.Text = "Password doesn't match.";
                    passwordBoxConfirm.Focus();
                }
                else if (textBlockAddress.Text.Length == 0)
                {
                    errormessage1.Text = "Enter Address.";
                    textBlockAddress.Focus();
                }
                else
                {


                    UserLogIn usr = new UserLogIn();
                    usr.FirstName = textBoxFirstName.Text;
                    usr.LastName = textBoxLastName.Text;
                    usr.Email = textBoxEmail1.Text;
                    usr.Password = passwordBox2.Password.ToString();
                    usr.Address = textBoxAddress.Text;


                    context.UserLogIns.Add(usr);
                    context.SaveChanges();
                    System.Windows.MessageBox.Show("Record saved successfully.");

                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail1.Text = "";
            passwordBox2.Password = "";
            passwordBoxConfirm.Password = "";
            textBoxAddress.Text = "";

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            LogInPopup.IsOpen = false;
        }

        private void close_Click1(object sender, RoutedEventArgs e)
        {
            SignUpPopup.IsOpen = false;
        }
    }
}
