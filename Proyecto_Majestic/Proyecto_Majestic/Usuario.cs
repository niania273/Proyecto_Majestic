//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_Majestic
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuario
    {
        [Display(Name = "Código")]
        public int cod_usu { get; set; }

        [Display(Name = "Nombre")]
        public string nom_usu { get; set; }

        [Display(Name = "Apellido")]
        public string ape_usu { get; set; }

        [Display(Name = "Email")]
        public string ema_usu { get; set; }
        [Display(Name = "Contraseña")]
        public string con_usu { get; set; }
        [Display(Name = "Fecha de Creación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fec_cre { get; set; }
        public Nullable<int> cod_emp { get; set; }
        public Nullable<int> cod_rol { get; set; }
        [Display(Name = "Estado")]
        public bool est_usu { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
