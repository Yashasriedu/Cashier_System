using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CashierSystem.Entities
{
    public class DataAccessLayer
    {

        public bool CreateAccount(string firstName, string lastName,int balance)
        {
            bool isSuccess = false;
            String strConnString = ConfigurationManager.ConnectionStrings["CSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prCreateAccount";
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName.Trim();
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName.Trim();
            cmd.Parameters.Add("@Balance", SqlDbType.Int).Value = balance;
            cmd.Connection = con;

            try

            {
                con.Open();
                cmd.ExecuteNonQuery();
                return isSuccess;
            }
            catch (Exception ex)

            {
                return isSuccess;
            }

            finally

            {
                con.Close();
                con.Dispose();
            }

        }
        public bool CreateTransaction(int accountId,int amount ,bool isWithDraw)
        {
            bool isSuccess = false;
            String strConnString = ConfigurationManager.ConnectionStrings["CSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prCreateTranasaction";
            cmd.Parameters.Add("@AccountId", SqlDbType.Int).Value = accountId;
            cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = amount;
            cmd.Parameters.Add("@isWithDraw", SqlDbType.Bit).Value = isWithDraw;
            cmd.Connection = con;

            try

            {
                con.Open();
                cmd.ExecuteNonQuery();
                return isSuccess;
            }
            catch (Exception ex)

            {
                return isSuccess;
            }

            finally

            {
                con.Close();
                con.Dispose();
            }

        }
        public  List<UserAccount> GetAllAccounts()
        {
            var accounts = new List<UserAccount>();
            String strConnString = ConfigurationManager.ConnectionStrings["CSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prGetAccounts";          
            cmd.Connection = con;

            try

            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt.ConvertDataTableToList<UserAccount>().ToList();
                }
            }
            catch (Exception ex)

            {
                return accounts;
            }

            finally

            {
                con.Close();
                con.Dispose();
            }

        }       
        public UserAccount GetAccountDetails(int accountId)
        {
            var accounts = new UserAccount();
            String strConnString = ConfigurationManager.ConnectionStrings["CSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prGetAccountDetails";
            cmd.Parameters.Add("@AccountId", SqlDbType.Int).Value = accountId;
            cmd.Connection = con;

            try

            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt.ConvertDataTableToList<UserAccount>().ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)

            {
                return accounts;
            }

            finally

            {
                con.Close();
                con.Dispose();
            }

        }
        public List<TransactionHistroy> GetAccountTransactions(int accountId)
        {
            var accounts = new List<TransactionHistroy>();
            String strConnString = ConfigurationManager.ConnectionStrings["CSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prGetAccountTransactionDetails";
            cmd.Parameters.Add("@AccountId", SqlDbType.Int).Value = accountId;
            cmd.Connection = con;

            try

            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt.ConvertDataTableToList<TransactionHistroy>().ToList();
                }
            }
            catch (Exception ex)

            {
                return accounts;
            }

            finally

            {
                con.Close();
                con.Dispose();
            }

        }
    }
    public static class Extensions
    {
        public static List<T> ConvertDataTableToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, Convert.ToString(dr[column.ColumnName]).Trim() == string.Empty ? null : dr[column.ColumnName], null);
                    //pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}