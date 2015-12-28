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
    using System.ComponentModel;

    public partial class Personne
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personne()
        {
            this.Emplois = new HashSet<Emploi>();
        }
    
        public decimal numero { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string adr_pays { get; set; }
        public string adr_ville { get; set; }
        public decimal adr_cp { get; set; }
        public string adr_rue { get; set; }
        public decimal adr_numero { get; set; }
        public string demandeExamen { get; set; }

        [DisplayName ("Nom du travailleur")]
        public string fullName
        {
            get
            {
                return this.nom + " " + this.prenom;
            }
        }      
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emploi> Emplois { get; set; }
    }
}