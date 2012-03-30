using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace MailChecker
{
    public class Database
    {
        public string DbFile { get; set; }

        public Database()
        {
        }
        public Database(string dbFile)
        {
            DbFile = dbFile;
        }

        public List<Account> Get_SavedAccounts()
        {
            try
            {
                List<Account> myAccounts = new List<Account>();
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbFile);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT email, username, password FROM ACCOUNTS", conn);
                command.CommandType = System.Data.CommandType.Text;
                SQLiteDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    myAccounts.Add(new Account(rdr["email"].ToString(), rdr["username"].ToString(), rdr["password"].ToString()));
                }
                conn.Close();
                return myAccounts;
            }
            catch
            {
                return null;
            }
        }
        public string Get_SavedLanguage()
        {
            try
            {
                string return_value = null;
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbFile);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT language_file FROM SETTINGS", conn);
                command.CommandType = System.Data.CommandType.Text;
                return_value = command.ExecuteScalar().ToString();
                conn.Close();
                return return_value;
            }
            catch
            {
                return null;
            }
        }
        public int Get_SavedTimerInterval()
        {
            try
            {
                int return_value = 0;
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbFile);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT timer_interval FROM SETTINGS", conn);
                command.CommandType = System.Data.CommandType.Text;
                return_value = int.Parse(command.ExecuteScalar().ToString());
                conn.Close();
                return return_value;
            }
            catch
            {
                return 5;  //5 minutes - default mail check interval
            }
        }
        public void Delete_Account(string email)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbFile);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("DELETE FROM ACCOUNTS WHERE email = '" + email + "'", conn);
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                return;
            }
        }
        public void Save_Account(string email, string username, string password)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbFile);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO ACCOUNTS(email, username, password) VALUES('" + email + "','" + username + "','" + password + "')", conn);
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                return;
            }
        }
        public void Save_Language(string langauge)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbFile);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("UPDATE SETTINGS SET language_file = '" + langauge + "'", conn);
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                return;
            }
        }
        public void Save_TimerInterval(int minutes)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbFile);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("UPDATE SETTINGS SET timer_interval = " + minutes, conn);
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                return;
            }
        }
        public bool Check_ForDouble(string email)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + DbFile);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM ACCOUNTS WHERE email = '" + email + "'", conn);
                command.CommandType = System.Data.CommandType.Text;
                int count = int.Parse(command.ExecuteScalar().ToString());
                conn.Close();
                return (count != 0);
            }
            catch
            {
                return true;
            }
        }
    }
}
