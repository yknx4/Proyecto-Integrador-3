using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Integrador_3.dsUsuariosTableAdapters;

namespace Proyecto_Integrador_3
{
    partial class DBManagers
    {
        
        /*permiso temporal - def protected*/public dsUsuarios mdsUsuarios = new dsUsuarios();
        protected UsuariosTableAdapter mUsuariosTableAdapter = new UsuariosTableAdapter();

        protected SqlCeConnection conn;

        public void setConnection(SqlCeConnection conn){

            this.conn = conn;
        }

        public string LastMessage = "";

        public void Refresh() {
            mdsUsuarios.Clear();
            //SqlCeDataAdapter adap = new SqlCeDataAdapter("SELECT * FROM Usuarios", conn);
            mUsuariosTableAdapter.Fill(mdsUsuarios.Usuarios);
            //LastMessage = adap.Update(mdsUsuarios, mdsUsuarios.Tables[0].TableName).ToString();
        }

        public DBManagers(SqlCeConnection conn)
        {
            // TODO: Complete member initialization
            this.conn = conn;
            //SqlCeDataAdapter adap = new SqlCeDataAdapter("SELECT * FROM Usuarios", conn);
            //adap.Fill(mdsUsuarios, mdsUsuarios.Tables[0].TableName);
            mUsuariosTableAdapter.Fill(mdsUsuarios.Usuarios);
            
            
        }
    }
}
