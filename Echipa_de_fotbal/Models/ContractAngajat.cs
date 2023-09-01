using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Echipa_de_fotbal.Models;

public partial class ContractAngajat
{
    [Key]
    public int IdContractAngajat { get; set; }

    public int? Salariu { get; set; }

    public DateOnly? DataSemnare { get; set; }

    public DateOnly? DataExpirare { get; set; }

    public int? ClauzaReziliere { get; set; }

    public int? IdEchipa { get; set; }

    public int? IdAngajat { get; set; }

    public virtual Angajat? IdAngajatNavigation { get; set; }

    public virtual Echipa? IdEchipaNavigation { get; set; }
}
