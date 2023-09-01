using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Echipa_de_fotbal.Models;

public partial class Jucator
{
    [Key]
    public int IdJucator { get; set; }

    public string? Nume { get; set; }

    public string? Prenume { get; set; }

    public DateOnly? DataN { get; set; }

    public string? Pozitie { get; set; }

    public int? Inaltime { get; set; }

    public int? Greutate { get; set; }

    public int? IdEchipa { get; set; }

    public virtual ICollection<ContractJucator> Contractjucatori { get; set; } = new List<ContractJucator>();

    public virtual Echipa? IdEchipaNavigation { get; set; }

    public virtual ICollection<SponsorJucator> Sponsorjucatori { get; set; } = new List<SponsorJucator>();
}
