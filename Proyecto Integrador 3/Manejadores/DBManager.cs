using System;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Windows;
using mValue = System.UInt32;

namespace Proyecto_Integrador_3
{
    partial class DBManagers
    {
       public abstract class DBManager<T>
        {
            protected DBManager()
            {

            }


            abstract public bool AddToDB();

            virtual public void itemModified(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                setItem((T)sender);
            }

            abstract public bool modificarDato(mValue modificar);

            virtual public bool RemoveFromDB()
            {
                throw new NotImplementedException();
            }


            protected T heldItem;

            protected SqlCeConnection connection;

            public void setConnection(SqlCeConnection connection)
            {
                this.connection = connection;
            }

            public void setItem(T Input)
            {
                heldItem = Input;
            }

            public void Clear()
            {
                heldItem = default(T);
            }

            protected bool Active()
            {
                if (heldItem == null || connection == null) return false;
                return true;
            }

            private string _error;
            public string Error { get { return _error; } set { _error = value; } }

        }
    }
}