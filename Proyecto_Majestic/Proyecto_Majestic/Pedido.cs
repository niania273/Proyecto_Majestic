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

    public partial class Pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedido()
        {
            this.Pedido_Producto = new HashSet<Pedido_Producto>();
        }
        [Display(Name = "Código")]
        public int cod_ped { get; set; }
        public int cod_cli { get; set; }
        public int cod_emp { get; set; }
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fec_ped { get; set; }
        [Display(Name = "Actualización")]
        public string act_ped { get; set; }
        [Display(Name = "Estado")]
        public bool est_ped { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Empleado Empleado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido_Producto> Pedido_Producto { get; set; }
    }
}