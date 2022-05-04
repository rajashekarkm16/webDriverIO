using System;
using System.Data.SqlClient;

namespace Dnata.Automation.BDDFramework.Helpers
{
    public class DBHelper
    {
        /// <summary>
        /// Executes the sql and returns the first rows first column value
        /// </summary>
        /// <param name="sql">SQL to execute</param>
        /// <returns></returns>
        public static string ExecuteSQLAndReturnCode(string sql, string connectionString)
        {
            SqlConnection sqlCon1 = new SqlConnection(connectionString);

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (SqlCommand sqlCom1 = new SqlCommand())
                {
                    sqlCom1.Connection = sqlCon;
                    sqlCom1.CommandType = System.Data.CommandType.Text;
                    sqlCom1.CommandText = sql;
                    return sqlCom1.ExecuteScalar().ToString();
                }
            }
        }


        /// <summary>
        /// Executes the sql query and return the rows affected
        /// </summary>
        /// <param name="sql"></param>
        public static int ExecuteSQL(string sql, string connectionString)
        {
            int rowsAffected;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (SqlCommand sqlCom = new SqlCommand())
                {
                    if (sqlCon.State == System.Data.ConnectionState.Open)
                    {
                        sqlCom.Connection = sqlCon;
                        sqlCom.CommandType = System.Data.CommandType.Text;
                        sqlCom.CommandText = sql;
                        rowsAffected = sqlCom.ExecuteNonQuery();
                        return rowsAffected;
                    }
                    else
                    {
                        throw new Exception("SQL Connection is closed");
                    }
                }
            }

        }


        /// <summary>
        ///  Returnd the results on the query in the form of reader
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteAndReturnReader(string sql, string connectionString)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            sqlCon.Open();
            SqlCommand sqlCom = new SqlCommand();
            sqlCom.Connection = sqlCon;
            sqlCom.CommandType = System.Data.CommandType.Text;
            sqlCom.CommandText = sql;
            return sqlCom.ExecuteReader(System.Data.CommandBehavior.CloseConnection);        
        }

    }
}
