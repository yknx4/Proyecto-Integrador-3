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
            AreUsuariosFilled = false;
            AreServiciosFilled = false;
            AreUnidadesFilled = false;
        }

        public void Clear()
        {
            ClearUsuarios();
            ClearUnidades();
            ClearServicios();
        }

        public void Fill()
        {
            FillUnidades();
            FillUsuarios();
            FillServicios();
        }

        public bool AreUsuariosFilled
        {
            get;
            set;
        }

        public bool AreUnidadesFilled
        {
            get;
            set;
        }

        public bool AreServiciosFilled
        {
            get;
            set;
        }

        public void FillUsuarios()
        {
            mUsuariosTableAdapter.Fill(mdsUsuarios.Usuarios);
            AreUsuariosFilled = true;
        }

        public void FillUnidades()
        {
            mUnidadlesTableAdapter.Fill(mdsUnidades.Unidad);
            AreUnidadesFilled = true;
        }

        public void FillServicios()
        {
            mServiciosTableAdapter.Fill(mdsServicios.Servicios);
            AreServiciosFilled = true;
        }

        public void ClearUsuarios()
        {
            mdsUsuarios.Clear();
            AreUsuariosFilled = false;
        }

        public void ClearUnidades()
        {
            mdsUnidades.Clear();
            AreUnidadesFilled = false;
        }

        public void ClearServicios()
        {
            mdsServicios.Clear();
            AreServiciosFilled = false;
        }
    }
}