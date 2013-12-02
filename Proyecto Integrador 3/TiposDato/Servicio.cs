using System;

namespace Proyecto_Integrador_3.TiposDato
{
    public partial class Servicio : DBItem
    {
        private long _id;

        public long Id { get { return _id; } set { _id = value; } }

        private short _tipoUsuario;

        public short TipoUsuario { get { return _tipoUsuario; } set { _tipoUsuario = value; } }

        private System.Guid _unidad;

        public System.Guid Unidad { get { return _unidad; } set { _unidad = value; } }

        private System.Guid _usuario;

        public System.Guid Usuario { get { return _usuario; } set { _usuario = value; } }

        private DateTime _fecha;

        public System.DateTime Fecha { get { return _fecha; } set { _fecha = value; } }

        public virtual Unidad UnidadObject { get; set; }

        public virtual Usuario UsuarioObject { get; set; }

        public override bool isAdded()
        {
            return !(Id == 0);
        }
    }
}