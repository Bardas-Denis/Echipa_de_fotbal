using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Echipa_de_fotbal.Models;

public partial class SponsorEchipa
{
    [Key]
    public int IdSponsor { get; set; }

    public int IdEchipa { get; set; }

    public int? SumaOferita { get; set; }

    public int? DurataContract { get; set; }

    public DateOnly? DataSemnare { get; set; }

    public virtual Echipa IdEchipaNavigation { get; set; } = null!;

    public virtual Sponsor IdSponsorNavigation { get; set; } = null!;
}
