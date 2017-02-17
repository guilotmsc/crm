using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using System.Text; 

namespace Persistencia
{
    public class User : Persistence
    {
        public String usuario = String.Empty;
        public String senha = String.Empty;

        public User() :
            base("usuarios", null) //aqui eh o construtor da tabela (nome_tabela)
        {
        }

        public User(NpgsqlTransaction transaction) :
            base("usuarios") //aqui eh o construtor da tabela (nome_tabela)
        {
            if (transaction != null)
            {
                transaction = this.transaction;
                con = transaction.Connection;
            }
        }

        public Object login(User user)
        {
            String sql = "SELECT COUNT(ID) FROM USUARIOS WHERE USUARIO = '{0}' AND SENHA = '{1}')";

            sql = String.Format(sql, user.usuario, user.senha);
            return queryExecute(sql);
        }
    }
}
