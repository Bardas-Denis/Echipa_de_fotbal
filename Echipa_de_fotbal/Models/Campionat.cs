using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Echipa_de_fotbal.Models;

public partial class Campionat
{
    [Key]
    public int IdCampionat { get; set; }

    public string? NumeCampionat { get; set; }

    public string? Tara { get; set; }

    public int? Divizie { get; set; }

    public virtual ICollection<Echipa> Echipe { get; set; } = new List<Echipa>();
}
