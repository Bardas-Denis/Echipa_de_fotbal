using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Echipa_de_fotbal.Models;

public partial class Sponsor
{
    [Key]
    public int IdSponsor { get; set; }

    public string? NumeSponsor { get; set; }

    public virtual ICollection<SponsorEchipa> SponsorEchipe { get; set; } = new List<SponsorEchipa>();

    public virtual ICollection<SponsorJucator> SponsorJucatori { get; set; } = new List<SponsorJucator>();
}
