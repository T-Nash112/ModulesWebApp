using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Part1
{
    //using System.Data.SqlClient;
    internal class Auth
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Auth() 
        {
        
        }

        public Auth(string username, string password)
        {
            Username=username;
            Encrypt.Hash(Password=password);
        }

        //Enter own connection string
        SqlConnection conn = new SqlConnection(@"");

        //Module to insert details to the database
        public void Register()
        {
            SqlCommand cmd = new SqlCommand($"INSERT INTO tblLogin VALUES('{Username}','{Password}')", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Auth> Logins()
        {
            string strSelect = $"SELECT * FROM tblLogin WHERE Username = '{Username}'";
            SqlCommand cmdSel = new SqlCommand(strSelect, conn);
            DataTable dtTable = new DataTable();
            DataRow dtRow;
            SqlDataAdapter adapt = new SqlDataAdapter(cmdSel);

            List<Auth> loginList = new List<Auth>();

            conn.Open();
            adapt.Fill(dtTable);

            if (dtTable.Rows.Count > 0)
            {
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    dtRow = dtTable.Rows[i];
                    Username = (string)dtRow["Username"]; //using a column name
                    Password = (string)dtRow["Password"]; //using a column index

                    loginList.Add(new Auth(Username, Password));
                }
            }
            return loginList;
        }

        //Module to verify if the users details match
        public bool Verify(string name, string pass)
        {
            int count = 0;
            string strSelect = $"SELECT Username,Password FROM tblLogin WHERE Username = '{name}' AND password = '{pass}'";
            SqlCommand cmdSelect = new SqlCommand(strSelect, conn);

            
                using (conn)
                {
                    conn.Open();
                    using (SqlDataReader reader = cmdSelect.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count++;
                        }
                    }
                    conn.Close();
                }            

            return count == 1;
        }
    }
}
