using System;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Windows;


namespace Proyecto_Integrador_3
{
    public partial class DBManagers
    {
       public abstract class DBManager<T>
        {
            protected DBManager(DBManagers sender)
            {
                this.Parent = sender;
                this.connection = Parent.conn;
            }


            abstract public void AddToDB();

            virtual public void itemModified(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                setItem((T)sender);
            }

            abstract public void modificarDato();

            virtual public bool RemoveFromDB()
            {
                throw new NotImplementedException();
            }

            protected DBManagers Parent;

            protected T heldItem;

            protected SqlCeConnection connection;

            

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