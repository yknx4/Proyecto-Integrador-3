using System;
using System.Collections.Generic;

namespace Proyecto_Integrador_3.TiposDato
{
    /// <summary>
    /// Modelo de la unidad
    /// </summary>
    public partial class Unidad : DBItem
    {
        public Unidad()
        {
            this.Servicios = new HashSet<Servicio>();
        }

        private Guid _uid;

        public System.Guid Uid { get { return _uid; } set { _uid = value; } }

        private string _nounidad;

        public string NoUnidad { get { return _nounidad; } set { _nounidad = value; } }

        public virtual ICollection<Servicio> Servicios { get; set; }

        public override bool isAdded()
        {
            return !(Uid == Guid.Empty);
        }
    }
}