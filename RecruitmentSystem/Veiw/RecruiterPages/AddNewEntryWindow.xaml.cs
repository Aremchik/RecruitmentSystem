using RecruitmentSystem.Model.EmployeesModels.Recruiters;
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

namespace RecruitmentSystem.Veiw.RecruiterPages
{
    /// <summary>
    /// Логика взаимодействия для AddNewEntryWindow.xaml
    /// </summary>
    public partial class AddNewEntryWindow : Window
    {
        public Approves approves { get; private set; }

        public AddNewEntryWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            approves = new Approves
            {
                DateAdded = DateTime.Now.ToString(),
                СandidateName = nameTextBox.Text,
                Summary = summaryTextBox.Text,
                Contacts = contactsTextBox.Text,
                Comment = commentTextBox.Text, 
                ApprovedByIT = "Проверка"
            };

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
