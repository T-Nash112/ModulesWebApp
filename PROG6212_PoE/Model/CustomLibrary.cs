using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace PROG6212_PoE.Model
{
    public class CustomLibrary
    {
        //get and set properties for the variables
        public string moduleCode { get; set; }
        public string moduleName { get; set; }
        public int moduleCredits { get; set; }
        public int hrsWeekly { get; set; }

        public int weeks { get; set; }

        public double hours { get; set; }
        public double selfstudy_Hours { get; set; }

        public DateTime date { get; set; }

        public double remainHours { get; set; }

        //Constructor for the custom library
        public CustomLibrary(string code, string name, int credits, int hoursPerWeek, int numOfWeeks, double selfstudyHours, DateTime date)
        {
            moduleCode = code;
            moduleName = name;
            this.moduleCredits = credits;
            this.hrsWeekly = hoursPerWeek;
            weeks = numOfWeeks;
            this.selfstudy_Hours = selfstudyHours;
            this.date = date;
        }

        public CustomLibrary()
        {
        }

        //Enter own connection string
        SqlConnection conn = new SqlConnection(@"");

        //Method to calculate self study hours
        public double selfstudyHours()
        {
            selfstudy_Hours = ((moduleCredits * 10) / weeks) - hrsWeekly;

            return selfstudy_Hours;
        }

        //Method to calculate remaining hours/spent
        public double hoursLeftOnModule()
        {
            double hoursSpent = selfstudy_Hours - hrsWeekly;

            if (hoursSpent < 0)
            {
                return 0; // Return 0 instead of a negative value
            }
            else
            {
                return hoursSpent;
            }
        }


        //Method to display the modules details
        public string displayModules()
        {
            return "            Modules App" +
                "\nModule Code: " + moduleCode + " \nModule Name: " + moduleName + "\nCredits: " +
                moduleCredits.ToString() + " \nClass Hours per week: " + weeks.ToString() + "hrs \n"
                + "\nDate: " + date + "\n------------------------------------------------------------";
        }

        //Method to display hours remaining/spent
        public string displayHours()
        {
            return "\nHours Remaining: " + hoursLeftOnModule() + "hrs \n" + "\n------------------------------------------------------------";
        }

        //Method to display hours spent on a specific module
        public string hoursOnModule()
        {
            return "\nModule Name: " + moduleName + "\nSelf-Study Hours left: " + selfstudyHours() + "hrs \n" + "\n------------------------------------------------------------";
        }

        //Adding modules to the database
        public void addNew()
        {
            SqlCommand cmd = new SqlCommand($"INSERT INTO tblModules VALUES('{moduleCode}','{moduleName}','{moduleCredits}',{weeks},'{hrsWeekly}','{date}')", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Module to show all modules from the database
        public List<CustomLibrary> allModules()
        {
            string strSelect = "SELECT * FROM tblModules";
            SqlCommand cmdSelect = new SqlCommand(strSelect, conn);
            DataTable myTable = new DataTable();
            DataRow myRow;
            SqlDataAdapter myAdapter = new SqlDataAdapter(cmdSelect);
            List<CustomLibrary> eList = new List<CustomLibrary>();

            conn.Open();
            myAdapter.Fill(myTable);

            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];
                    moduleCode = (string)myRow[0];
                    moduleName = (string)myRow[1]; //using a column index                  
                    moduleCredits = (int)myRow[2];
                    weeks = Convert.ToInt32(myRow[3]);
                    hrsWeekly = Convert.ToInt32(myRow[4]);
                    date = (DateTime)myRow[5];



                    eList.Add(new CustomLibrary(moduleCode, moduleName, moduleCredits, hrsWeekly, weeks, selfstudy_Hours, date));
                }
            }

            return eList;
        }

        public CustomLibrary getModule(string code)
        {
            string strSelect = $"SELECT * FROM tblModules WHERE code = '{code}' ";
            SqlCommand cmdSelect = new SqlCommand(strSelect, conn);

            conn.Open();
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    moduleCode = (string)reader[0];
                    moduleName = (string)reader[1]; //using a column index                  
                    moduleCredits = (int)reader[2];
                    weeks = Convert.ToInt32(reader[3]);
                    hrsWeekly = Convert.ToInt32(reader[4]);
                    date = (DateTime)reader[5];
                }
            }
            conn.Close();

            return new CustomLibrary(moduleCode, moduleName, moduleCredits, hrsWeekly, weeks, selfstudy_Hours, date);


        }

        public void delete(string code)
        {
            string strDelete = $"DELETE FROM tblModules WHERE code = '{code}'";
            SqlCommand cmdDelete = new SqlCommand(strDelete, conn);

            conn.Open();
            cmdDelete.ExecuteNonQuery();
            conn.Close();
        }

        public void update(string code)
        {
            string strUpdate = $"UPDATE tblModules SET name = '{moduleName}', " +
                $"credits = '{moduleCredits}', weeks = '{weeks}', " +
                $"hours = '{hrsWeekly}', date = '{date}' WHERE code = '{code}'";
            SqlCommand cmdUpdate = new SqlCommand(strUpdate, conn);

            conn.Open();
            cmdUpdate.ExecuteNonQuery();
            conn.Close();
        }

        public ActionResult Index()
        {
            string query = "SELECT name, hours FROM tblModules";


            List<CustomLibrary> chartData = new List<CustomLibrary>();


            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        chartData.Add(new CustomLibrary
                        {
                            moduleName = (string)sdr["name"],
                            hrsWeekly = Convert.ToInt32(sdr["hours"])
                        });
                    }


                    conn.Close();
                }
            }

            return View(chartData);
        }

        private ActionResult View(List<CustomLibrary> chartData)
        {
            throw new NotImplementedException();
        }
    }
}
