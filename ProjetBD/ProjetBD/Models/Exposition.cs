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
    using System.ComponentModel.DataAnnotations;

    public partial class Exposition
    {
        public decimal numero { get; set; }
        public string libelle { get; set; }

        [StringLength(100)]
        [DisplayName ("Commentaire éventuel")]
        public string Commentaire { get; set; }
    
        public virtual Emploi Emploi { get; set; }
        public virtual Risque Risque { get; set; }
    }
}
