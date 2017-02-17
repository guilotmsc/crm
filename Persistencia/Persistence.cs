using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using System.Text;

namespace Persistencia
{
    public class Persistence
    {
        private string ConnectionString;
        public NpgsqlConnection con;
        public NpgsqlTransaction transaction;

        protected string table;
        protected string fields, values, condition;


        public Persistence(String table)
        {
            this.table = table;
        }

        public Persistence(String table, NpgsqlTransaction transaction)
        {
            if (transaction != null)
            {
                this.table = table;
                this.con = transaction.Connection;
                this.transaction = transaction;
            }
            else
            {
                this.table = table;
                OpenConnection();
                startTransaction();
            }
        }

        public void startTransaction()
        {
            if (transaction == null)
            {
                transaction = con.BeginTransaction();
            }
        }


        public string OpenConnection()
        {
            string result;
            this.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            this.con = new NpgsqlConnection(this.ConnectionString);

            try
            {
                this.con.Open();
                result = "Conectado ao banco";
            }
            catch (NpgsqlException ex)
            {
                result = ex.Message;
            }

            return result;
        }


        public string CloseConnection(Boolean close = true)
        {
            string result;

            try
            {
                if (close == true)
                {
                    this.con.Close();//fecha conexao
                }
                result = "Conexão fechada";
            }
            catch (NpgsqlException ex)
            {
                result = ex.Message;
            }

            return result;
        }


        public void AddField(String field, Object value, String type)
        {
            fields += field + ",";

            if (type == "string")
            {
                values += "'" + value + "',";
            }
            else if (type == "int")
            {
                values += value + ",";
            }
            else if (type == "date")
            {
                values += "'" + Convert.ToDateTime(value) + "',";
            }
            else if (type == "decimal")
            {
                values += value.ToString().Replace(",", ".") + ",";
            }
        }


        public void AddWhere(String field, String valor)
        {
            condition = "{0} = {1}";

            condition = String.Format(condition, field, valor);
        }


        public Object Insert()
        {
            String sqlInsert = "INSERT INTO {0} ({1}) VALUES ({2}); SELECT id FROM {0} ORDER BY ID DESC LIMIT 1";

            sqlInsert = String.Format(sqlInsert, table, fields.Substring(0, fields.Length - 1), values.Substring(0, values.Length - 1));

            return queryExecute(sqlInsert);
        }


        public void Update()
        {
            String sqlUpdate = "UPDATE {0} ({1}) SET ({2}) WHERE {4}";

            sqlUpdate = String.Format(sqlUpdate, table, fields.Length - 1, values.Length - 1, condition);

            queryExecute(sqlUpdate);
        }


        public void Delete()
        {
            String sqlDelete = "DELETE FROM {0} WHERE {1}";

            sqlDelete = String.Format(sqlDelete, table, condition);

            queryExecute(sqlDelete);
        }


        public Object queryExecute(string sql)//Método que executa qualquer query sem retorno(UPDATE, INSERT, DELETE, SET)
        {
            Object modified = 0;
            this.OpenConnection();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand(sql, this.con); // cria o comendo de acordo com sua conexão e transação
                //Int32 rowsaffected = command.ExecuteNonQuery();
                modified = command.ExecuteScalar();
                this.CloseConnection();
            }
            catch (NpgsqlException ex)
            {
                transaction.Rollback(); // Em caso de erro cancela (Rollback) a transação.
            }

            return modified;
        }


        public static string limpaLixo(string input)
        {
            string textoOK = "";

            if (input != null)
            {
                string[] lixo = { "select", "drop", ";", "--", "insert", "delete", "xp_" };

                foreach (string item in lixo)
                {
                    textoOK = input.Replace(item, "");
                }

                textoOK = textoOK.Replace("'", "''");
            }
            return textoOK;
        }


        public DataTable queryToDataTable(String sql, Boolean close = true)
        {
            OpenConnection();
            DataTable dataTable = new DataTable();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand(sql, this.con);
                IDataReader buffer = command.ExecuteReader();
                dataTable.Load(buffer);
                buffer.Close();
                CloseConnection(close);
                return dataTable;
            }
            catch (NpgsqlException e)
            {
                throw (e);
            }
        }

        public DataTable getAllRecords(Boolean close = true)
        {
            String sql = "SELECT * FROM {0}";
            sql = String.Format(sql, table);

            OpenConnection();
            DataTable dataTable = new DataTable();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand(sql, this.con);
                IDataReader buffer = command.ExecuteReader();
                dataTable.Load(buffer);
                buffer.Close();
                CloseConnection(close);
                return dataTable;
            }
            catch (NpgsqlException e)
            {
                throw (e);
            }
        }
    }
}
