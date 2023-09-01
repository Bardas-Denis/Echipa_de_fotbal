using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Echipa_de_fotbal.Models;

public partial class ContractJucator
{
    [Key]
    public int IdContractJucator { get; set; }

    public int? Salariu { get; set; }

    public DateOnly? DataSemnare { get; set; }

    public DateOnly? DataExpirare { get; set; }

    public int? ClauzaReziliere { get; set; }

    public int? IdEchipa { get; set; }

    public int? IdJucator { get; set; }

    public virtual Echipa? IdEchipaNavigation { get; set; }

    public virtual Jucator? IdJucatorNavigation { get; set; }
}
