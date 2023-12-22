using RecruitmentSystem.Model.EmployeesModels.ITSpecialists;
using RecruitmentSystem.Model.EmployeesModels.Recruiters;
using RecruitmentSystem.Model.EmployeesModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using RecruitmentSystem.Model.DataBase;

namespace RecruitmentSystem.Veiw.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            PopulateEmployeeList();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSpecialization.Text == "Рекрутер")
            {
                Recruiter recruiter = new Recruiter
                {
                    UserName = txtUserName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Password,
                    Specialization = "Recruiter"

                };
                Database.AddNewRecruiter(recruiter);
            }
            else if (cmbSpecialization.Text == "IT специалист")
            {
                ITSpecialist iTSpecialist = new ITSpecialist
                {
                    UserName = txtUserName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Password,
                    Specialization = "ITSpecialist"
                };
                Database.AddNewITSpecialist(iTSpecialist);
            }

            txtUserName.Text = "";
            txtEmail.Text = "";
            txtPassword.Password = "";
            cmbSpecialization.Text = "";

            PopulateEmployeeList();
        }

        private void PopulateEmployeeList()
        {
            ObservableCollection<Employees> employees = Database.GetEmployeesInfo();
            employeeListView.ItemsSource = employees;
        }
    }
}
