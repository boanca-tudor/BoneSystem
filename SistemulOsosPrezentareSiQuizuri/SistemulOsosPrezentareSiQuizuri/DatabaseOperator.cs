using System;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace SistemulOsosPrezentareSiQuizuri
{
    class DatabaseOperator
    {
        private string connectionString;
        private SqlConnection connection = null;

        public DatabaseOperator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void FillDatabase(string filePath)
        {
            OpenConnection();

            string[] separatori = { "\n", "\r", "*" };
            string linie;

            using (SqlCommand command = new SqlCommand() { Connection = connection })
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    command.Parameters.Add("@Intrebare", SqlDbType.NVarChar);
                    command.Parameters.Add("@Raspuns1", SqlDbType.NVarChar);
                    command.Parameters.Add("@Raspuns2", SqlDbType.NVarChar);
                    command.Parameters.Add("@Raspuns3", SqlDbType.NVarChar);
                    command.Parameters.Add("@Raspuns4", SqlDbType.NVarChar);
                    command.Parameters.Add("@RaspunsCorect", SqlDbType.NVarChar);

                    while ((linie = sr.ReadLine()) != null)
                    {
                        if (linie != "")
                        {
                            var rezultat = linie.Split(separatori, StringSplitOptions.RemoveEmptyEntries);
                            command.CommandText = "INSERT INTO ResurseGrile VALUES (@Intrebare, @Raspuns1, @Raspuns2, @Raspuns3, @Raspuns4, @RaspunsCorect)";
                            command.Parameters["@Intrebare"].Value = rezultat[0];
                            command.Parameters["@Raspuns1"].Value = rezultat[1];
                            command.Parameters["@Raspuns2"].Value = rezultat[2];
                            command.Parameters["@Raspuns3"].Value = rezultat[3];
                            command.Parameters["@Raspuns4"].Value = rezultat[4];
                            command.Parameters["@RaspunsCorect"].Value = rezultat[5];

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void ShowDatabase(DataGridView grid, string table)
        {
            using (var command = new SqlCommand()
            {
                Connection = connection,
                CommandText = "SELECT * FROM " + table
            })
            {
                using (var dr = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(dr);

                    grid.DataSource = tb;
                }
            };
        }

        public string GetValue(string field, string table, int ID)
        {
            using (SqlCommand command = new SqlCommand() { Connection = connection })
            {
                command.CommandText = "SELECT " + field + " FROM " + table + " WHERE ID='" + ID.ToString() + "';";

                using (var dr = command.ExecuteReader())
                {
                    dr.Read();
                    return dr.GetString(0);
                }
            }
        }
    }

    
}
