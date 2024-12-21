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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleado()
        {
            this.Pedido = new HashSet<Pedido>();
            this.Usuario = new HashSet<Usuario>();
        }
        [Display(Name = "Código")]
        public int cod_emp { get; set; }
        [Display(Name = "Nombres")]
        public string nom_emp { get; set; }
        [Display(Name = "Apellidos")]
        public string ape_emp { get; set; }
        [Display(Name = "DNI")]
        public string dni_emp { get; set; }
        [Display(Name = "Fecha Ing.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha_in_emp { get; set; }
        [Display(Name = "Teléfono")]
        public string telf_emp { get; set; }
        [Display(Name = "Sexo")]
        public string sexo_emp { get; set; }
        [Display(Name = "Num. Ventas")]
        public int num_emp { get; set; }
        [Display(Name = "Estado")]
        public bool est_emp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
