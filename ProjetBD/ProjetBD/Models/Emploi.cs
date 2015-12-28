//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetBD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Emploi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Emploi()
        {
            this.Expositions = new HashSet<Exposition>();
        }
    
        public System.DateTime dateEntree { get; set; }
        public Nullable<System.DateTime> dateSortie { get; set; }
        public decimal numero { get; set; }
        public string code { get; set; }
        public decimal R_1_numero { get; set; }
        public decimal R_numero { get; set; }
    
        public virtual Entreprise Entreprise { get; set; }
        public virtual Personne Personne { get; set; }
        public virtual Profession Profession { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exposition> Expositions { get; set; }
    }
}