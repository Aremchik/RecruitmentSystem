﻿using RecruitmentSystem.Model.DataBase;
using RecruitmentSystem.Model.EmployeesModels;
using RecruitmentSystem.Veiw.AdminPages;
using RecruitmentSystem.Veiw.ITSpecialistPages;
using RecruitmentSystem.Veiw.RecruiterPages;
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

namespace RecruitmentSystem.Veiw.Authentication
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            Database.InitializeDatabase();
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            if (Database.VerifyData(UsernameTextBox.Text, PasswordBox.Password))
            {
                Employees employees = Database.GetUserInformation(Database.GetUserId(UsernameTextBox.Text, PasswordBox.Password));
                App.SessionData.EmployeesId = employees.EmployeesId;
                App.SessionData.UserName = employees.UserName;
                App.SessionData.Specialization = employees.Specialization;
                if (employees.Specialization == "Recruiter")
                {
                    NavigationService.Navigate(new Statistics());
                }
                else if (employees.Specialization == "Admin")
                {
                    NavigationService.Navigate(new AdminPage());
                }
                else if (employees.Specialization == "ITSpecialist")
                {
                    NavigationService.Navigate(new ApprovesByITPage());
                }

            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
