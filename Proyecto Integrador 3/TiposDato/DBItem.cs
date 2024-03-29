﻿using System;

namespace Proyecto_Integrador_3.TiposDato
{
    /// <summary>
    /// Modelo abstracto de un objeto de la base de datos
    /// </summary>
    public class DBItem
    {
        /// <summary>
        /// Determina si esta instancia esta agregada a la base de datos.
        /// </summary>
        /// <returns>Verdadero si esta agregada, falso si no</returns>
        public virtual bool isAdded()
        {
            throw new NotImplementedException();
        }
    }
}