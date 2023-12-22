using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
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
using RecruitmentSystem.Model.EmployeesModels.Recruiters;
using System.Collections.ObjectModel;

namespace RecruitmentSystem.Veiw.ITSpecialistPages
{
    /// <summary>
    /// Логика взаимодействия для ApprovesByITPage.xaml
    /// </summary>
    public partial class ApprovesByITPage : Page
    {
        public ApprovesByITPage()
        {
            InitializeComponent();
            LoadResumeByApprove();
        }

        private void LoadResumeByApprove()
        {
            ObservableCollection<Approves> approves = Database.GetResumeByApprove();
            userDataGrid.ItemsSource = approves;
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            Approves selectedRow = (Approves)userDataGrid.SelectedItem;
            Database.UpdateAproves(selectedRow.ApproveId, "Принят");
            LoadResumeByApprove();
            Database.UpdateStatisticHiredCount(Database.GetHiredCount(selectedRow.EmployeesId) + 1, selectedRow.EmployeesId);
            Database.UpdateApprovedResume(Database.GetApprovedResume(App.SessionData.EmployeesId) + 1, App.SessionData.EmployeesId);
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            Approves selectedRow = (Approves)userDataGrid.SelectedItem;
            Database.UpdateAproves(selectedRow.ApproveId, "Отклонен");
            LoadResumeByApprove();
        }

        
    }
}
