using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Echipa_de_fotbal.Models;

public partial class Echipa
{
    [Key]
    public int IdEchipa { get; set; }

    public string? NumeEchipa { get; set; }

    public string? NumePatron { get; set; }

    public int? IdCampionat { get; set; }

    public int? IdStadion { get; set; }

    public virtual ICollection<Angajat> Angajati { get; set; } = new List<Angajat>();

    public virtual ICollection<ContractAngajat> Contractangajati { get; set; } = new List<ContractAngajat>();

    public virtual ICollection<ContractJucator> Contractjucatori { get; set; } = new List<ContractJucator>();

    public virtual Campionat? IdCampionatNavigation { get; set; }

    public virtual Stadion? IdStadionNavigation { get; set; }

    public virtual ICollection<Jucator> Jucatori { get; set; } = new List<Jucator>();

    public virtual ICollection<SponsorEchipa> SponsorEchipe { get; set; } = new List<SponsorEchipa>();
}
