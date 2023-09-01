using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Echipa_de_fotbal.Models;

public partial class Angajat
{
    [Key]
    public int IdAngajat { get; set; }

    public string? Nume { get; set; }

    public string? Prenume { get; set; }

    public DateOnly? DataN { get; set; }

    public string? Functie { get; set; }

    public string? NivelStudii { get; set; }

    public int? IdEchipa { get; set; }

    public virtual ICollection<ContractAngajat> ContractAngajati { get; set; } = new List<ContractAngajat>();

    public virtual Echipa? IdEchipaNavigation { get; set; }
}
