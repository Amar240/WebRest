using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("ADDRESS")]
public partial class Address
{
    [Key]
    [Column("ADDRESS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string AddressId { get; set; } = null!;

    [Column("ADDRESS_LINE1")]
    [StringLength(50)]
    [Unicode(false)]
    public string AddressLine1 { get; set; } = null!;

    [Column("ADDRESS_LINE2")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddressLine2 { get; set; }

    [Column("ADDRESS_LINE3")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddressLine3 { get; set; }

    [Column("ADDRESS_CITY")]
    [StringLength(40)]
    [Unicode(false)]
    public string AddressCity { get; set; } = null!;

    [Column("ADDRESS_STATE")]
    [StringLength(2)]
    [Unicode(false)]
    public string AddressState { get; set; } = null!;

    [Column("ADDRESS_ZIP")]
    [StringLength(9)]
    [Unicode(false)]
    public string AddressZip { get; set; } = null!;

    [Column("ADDRESS_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string AddressCrtdId { get; set; } = null!;

    [Column("ADDRESS_CRTD_DT", TypeName = "DATE")]
    public DateTime AddressCrtdDt { get; set; }

    [Column("ADDRESS_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string AddressUpdtId { get; set; } = null!;

    [Column("ADDRESS_UPDT_DT", TypeName = "DATE")]
    public DateTime AddressUpdtDt { get; set; }

    [InverseProperty("CustomerAddressAddress")]
    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
}
