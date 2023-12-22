using RecruitmentSystem.Model.EmployeesModels.ITSpecialists;
using RecruitmentSystem.Model.EmployeesModels.Recruiters;
using RecruitmentSystem.Model.EmployeesModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentSystem.Model.DataBase
{
    public static class Database
    {
        private const string ConnectionString = "Data Source=D:\\prod\\Prilo\\RecruitmentSystem\\RecruitmentSystem\\Model\\db\\RecruitmenSystemDatabase.db;Version=3;";

        public static void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        
                        CREATE TABLE IF NOT EXISTS Employees (
                            UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT,
                            Email TEXT,
                            Password TEXT,
                            Specialization TEXT
                        );
                        CREATE TABLE IF NOT EXISTS Recruiters (
                            RecruiterId INTEGER,
                            Approved INTEGER DEFAULT 0,
                            Hired INTEGER DEFAULT 0,
                            FOREIGN KEY (RecruiterId) REFERENCES Employees(UserId)
                        );
                        CREATE TABLE IF NOT EXISTS ITSpecialists (
                            ITSpecialistId INTEGER, 
                            InterviewsCount INTEGER DEFAULT 0,
                            FOREIGN KEY (ITSpecialistId) REFERENCES Employees(UserId)
                        );
                        CREATE TABLE IF NOT EXISTS Approves (
                            ApprovedId INTEGER PRIMARY KEY AUTOINCREMENT,
                            RecruiterId INTEGER,
                            DateAdded TEXT,
                            СandidateName TEXT,
                            Summary TEXT,
                            Contacts TEXT,
                            Comment TEXT,
                            ApprovedByIT TEXT,
                            FOREIGN KEY (RecruiterId) REFERENCES Employees(UserId)
                        );
                    ";
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void RegisterUser(Recruiter recruiter)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("INSERT INTO Employees (Username, Email, Password) VALUES (@Username, @Email, @Password)", connection))
                {
                    command.Parameters.AddWithValue("@Username", recruiter.UserName);
                    command.Parameters.AddWithValue("@Email", recruiter.Email);
                    command.Parameters.AddWithValue("@Password", recruiter.Password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool VerifyData(string username, string password)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Employees WHERE Username = @Username AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static bool VerifyExistence(string username, string password)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Employees WHERE Username = @Username OR Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static string GetUserId(string username, string password)
        {
            string userId = "";
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT UserId FROM Employees WHERE Username = @Username AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = Convert.ToString(reader["UserId"]);

                        }
                    }

                }
            }
            return userId;
        }
        public static Employees GetUserInformation(string userId)
        {
            Employees employees = null;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Employees WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees = new Employees
                            {
                                EmployeesId = Convert.ToInt32(reader["UserId"]),
                                UserName = Convert.ToString(reader["Username"]),
                                Email = Convert.ToString(reader["Email"]),
                                Password = Convert.ToString(reader["Password"]),
                                Specialization = Convert.ToString(reader["Specialization"]),
                            };

                        }

                    }
                }
            }

            return employees;
        }
        public static void AddNewRecruiter(Recruiter recruiter)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("INSERT INTO Employees (Username, Email, Password, Specialization) VALUES (@username, @email, @password, @specialization)", connection))
                {
                    command.Parameters.AddWithValue("@username", recruiter.UserName);
                    command.Parameters.AddWithValue("@email", recruiter.Email);
                    command.Parameters.AddWithValue("@password", recruiter.Password);
                    command.Parameters.AddWithValue("@specialization", recruiter.Specialization);
                    command.ExecuteNonQuery();
                }
                long lastUserId;
                using (SQLiteCommand command = new SQLiteCommand("SELECT last_insert_rowid()", connection))
                {
                    lastUserId = (long)command.ExecuteScalar();
                }
                using (var command = new SQLiteCommand("INSERT INTO Recruiters (RecruiterId) VALUES (@recruiterId)", connection))
                {
                    command.Parameters.AddWithValue("@recruiterId", lastUserId);
                    command.ExecuteNonQuery();
                }
            }

        }
        public static void AddNewITSpecialist(ITSpecialist iTSpecialist)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("INSERT INTO Employees (Username, Email, Password, Specialization) VALUES (@username, @email, @password, @specialization)", connection))
                {
                    command.Parameters.AddWithValue("@username", iTSpecialist.UserName);
                    command.Parameters.AddWithValue("@email", iTSpecialist.Email);
                    command.Parameters.AddWithValue("@password", iTSpecialist.Password);
                    command.Parameters.AddWithValue("@specialization", iTSpecialist.Specialization);
                    command.ExecuteNonQuery();
                }
                long lastUserId;
                using (SQLiteCommand command = new SQLiteCommand("SELECT last_insert_rowid()", connection))
                {
                    lastUserId = (long)command.ExecuteScalar();
                }
                using (var command = new SQLiteCommand("INSERT INTO ITSpecialists (ITSpecialistId) VALUES (@iTSpecialistId)", connection))
                {
                    command.Parameters.AddWithValue("@iTSpecialistId", lastUserId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static ObservableCollection<Employees> GetEmployeesInfo()
        {
            ObservableCollection<Employees> employees = new ObservableCollection<Employees>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT UserId, Username, Email, Specialization FROM Employees ", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employees employee = new Employees
                            {
                                EmployeesId = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Email = reader.GetString(2),
                                Specialization = reader.GetString(3)
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }

        public static ObservableCollection<Recruiter> GetRecruiterInfo()
        {
            ObservableCollection<Recruiter> recruiters = new ObservableCollection<Recruiter>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(@"
                    SELECT Employees.UserId, Employees.Username, Employees.Email, Recruiters.Approved, Recruiters.Hired
                    FROM Employees
                    INNER JOIN Recruiters ON Employees.UserId = Recruiters.RecruiterId
                ", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Recruiter recruiter = new Recruiter
                            {

                                EmployeesId = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Email = reader.GetString(2),
                                ApprovedCount = reader.GetInt32(3),
                                HiredCount = reader.GetInt32(4)

                            };
                            recruiters.Add(recruiter);
                        }
                    }
                }
            }

            return recruiters;
        }
        public static ObservableCollection<Approves> GetApprovesInfo(int employeesId, string userName)
        {
            ObservableCollection<Approves> approves = new ObservableCollection<Approves>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT ApprovedId, DateAdded, СandidateName, Summary, Contacts, Comment, ApprovedByIT FROM Approves WHERE RecruiterId = @recruiterId", connection))
                {
                    command.Parameters.AddWithValue("@recruiterId", employeesId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Approves approve = new Approves
                            {
                                UserName = userName,
                                ApproveId = Convert.ToInt32(reader["ApprovedId"]),
                                DateAdded = Convert.ToString(reader["DateAdded"]),
                                СandidateName = Convert.ToString(reader["СandidateName"]),
                                Summary = Convert.ToString(reader["Summary"]),
                                Contacts = Convert.ToString(reader["Contacts"]),
                                Comment = Convert.ToString(reader["Comment"]),
                                ApprovedByIT = Convert.ToString(reader["ApprovedByIT"]),

                            };
                            approves.Add(approve);
                        }
                    }
                }
            }

            return approves;
        }

        public static void AddNewApprove(Approves approve, int employeesId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("INSERT INTO Approves (RecruiterId, DateAdded, СandidateName, Summary, Contacts, Comment, ApprovedByIT) VALUES (@recruiterId, @dateAdded, @candidateName, @summary, @contacts, @comment, @approvedByIT)", connection))
                {
                    command.Parameters.AddWithValue("@recruiterId", employeesId);
                    command.Parameters.AddWithValue("@dateAdded", approve.DateAdded);
                    command.Parameters.AddWithValue("@candidateName", approve.СandidateName);
                    command.Parameters.AddWithValue("@summary", approve.Summary);
                    command.Parameters.AddWithValue("@contacts", approve.Contacts);
                    command.Parameters.AddWithValue("@comment", approve.Comment);
                    command.Parameters.AddWithValue("@approvedByIT", approve.ApprovedByIT);
                    command.ExecuteNonQuery();
                }

            }
        }
        public static int GetApprovedCount(int employeesId)
        {
            int approvedCount = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT Approved FROM Recruiters WHERE RecruiterId = @recruiterId", connection))
                {
                    command.Parameters.AddWithValue("@recruiterId", employeesId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            approvedCount = Convert.ToInt32(reader["Approved"]);

                        }
                    }

                }
            }
            return approvedCount;
        }
        public static void UpdateStatisticApprovedCount(int approvedCount, int employeesId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("UPDATE Recruiters SET Approved = @approvedCount WHERE RecruiterId = @recruiterId ", connection))
                {
                    command.Parameters.AddWithValue("@approvedCount", approvedCount);
                    command.Parameters.AddWithValue("@recruiterId", employeesId);
                    command.ExecuteNonQuery();
                }

            }
        }
        public static ObservableCollection<Approves> GetResumeByApprove()
        {
            ObservableCollection<Approves> approves = new ObservableCollection<Approves>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("SELECT ApprovedId, RecruiterId, DateAdded, СandidateName, Summary, Contacts, Comment FROM Approves WHERE ApprovedByIT = 'Проверка'", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Approves approve = new Approves
                            {
                                ApproveId = Convert.ToInt32(reader["ApprovedId"]),
                                EmployeesId = Convert.ToInt32(reader["RecruiterId"]),
                                DateAdded = Convert.ToString(reader["DateAdded"]),
                                СandidateName = Convert.ToString(reader["СandidateName"]),
                                Summary = Convert.ToString(reader["Summary"]),
                                Contacts = Convert.ToString(reader["Contacts"]),
                                Comment = Convert.ToString(reader["Comment"])

                            };
                            approves.Add(approve);
                        }
                    }
                }
            }

            return approves;
        }

        public static void UpdateAproves(int approvedId, string status)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("UPDATE Approves SET ApprovedByIT = @approvedByIT WHERE ApprovedId = @approvedId", connection))
                {
                    command.Parameters.AddWithValue("@approvedByIT", status);
                    command.Parameters.AddWithValue("@approvedId", approvedId);
                    command.ExecuteNonQuery();
                }

            }
        }

        public static int GetHiredCount(int employeesId)
        {
            int hiredCount = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT Hired FROM Recruiters WHERE RecruiterId = @recruiterId", connection))
                {
                    command.Parameters.AddWithValue("@recruiterId", employeesId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hiredCount = Convert.ToInt32(reader["Hired"]);

                        }
                    }

                }
            }
            return hiredCount;
        }
        public static void UpdateStatisticHiredCount(int hiredCount, int employeesId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("UPDATE Recruiters SET Hired = @hired WHERE RecruiterId = @recruiterId ", connection))
                {
                    command.Parameters.AddWithValue("@hired", hiredCount);
                    command.Parameters.AddWithValue("@recruiterId", employeesId);
                    command.ExecuteNonQuery();
                }

            }
        }
        public static int GetApprovedResume(int employeesId)
        {
            int approvedResume = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT ApprovedResume FROM ITSpecialists WHERE ITSpecialistId = @iTSpecialistId", connection))
                {
                    command.Parameters.AddWithValue("@iTSpecialistId", employeesId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            approvedResume = Convert.ToInt32(reader["ApprovedResume"]);

                        }
                    }

                }
            }
            return approvedResume;
        }
        public static void UpdateApprovedResume(int approvedResume, int employeesId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("UPDATE ITSpecialists SET ApprovedResume = @approvedResume WHERE ITSpecialistId = @iTSpecialistId ", connection))
                {
                    command.Parameters.AddWithValue("@approvedResume", approvedResume);
                    command.Parameters.AddWithValue("@iTSpecialistId", employeesId);
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
