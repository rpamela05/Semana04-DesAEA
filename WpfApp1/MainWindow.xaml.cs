using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3EHKHA9\\SQLEXPRESS;Initial Catalog=db_lab04;Integrated Security=true;");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();

            con.Open();
            SqlCommand command = new SqlCommand("BuscarPersonaNombre",con);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Size = 50;
            parameter.Value = "";
            parameter.ParameterName = "@LastName";

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;
            parameter1.Value = "";
            parameter1.ParameterName = "@FirstName";
            
            command.Parameters.Add(parameter1);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new Person{
                    PersonId = dataReader["PersonID"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    FirstName=dataReader["FirstName"].ToString(),
                    FullName=string.Concat(dataReader["FirstName"].ToString(), " ",
                    dataReader["LastName"].ToString())
                });
            }
            con.Close();
            dvgPeople.ItemsSource = people;
        }
    }
}
