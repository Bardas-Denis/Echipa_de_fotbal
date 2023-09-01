using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Echipa_de_fotbal.Models;

public partial class Stadion
{
    [Key]
    public int IdStadion { get; set; }

    public string? NumeStadion { get; set; }

    public int? Capacitate { get; set; }

    public string? Strada { get; set; }

    public string? Oras { get; set; }

    public virtual ICollection<Echipa> Echipe { get; set; } = new List<Echipa>();
}
