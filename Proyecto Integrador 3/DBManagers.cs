namespace Proyecto_Integrador_3
{
    public partial class DBManagers
    {
        public string LastMessage = "";

        public void Refresh()
        {
            Clear();
            Fill();
            
        }
        

        public DBManagers()
        {
            
        }

        public void Clear()
        {
            mdsUsuarios.Clear();
            mdsServicios.Clear();
            mdsUnidades.Clear();

        }

        public void Fill()
        {
            mUsuariosTableAdapter.Fill(mdsUsuarios.Usuarios);
            mUnidadlesTableAdapter.Fill(mdsUnidades.Unidad);
            mServiciosTableAdapter.Fill(mdsServicios.Servicios);
        }
    }
}