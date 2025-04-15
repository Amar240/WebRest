using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("ADDRESS_TYPE")]
public partial class AddressType
{
    [Key]
    [Column("ADDRESS_TYPE_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string AddressTypeId { get; set; } = null!;

    [Column("ADDRESS_TYPE_DESC")]
    [StringLength(10)]
    [Unicode(false)]
    public string AddressTypeDesc { get; set; } = null!;

    [Column("ADDRESS_TYPE_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string AddressTypeCrtdId { get; set; } = null!;

    [Column("ADDRESS_TYPE_CRTD_DT", TypeName = "DATE")]
    public DateTime AddressTypeCrtdDt { get; set; }

    [Column("ADDRESS_TYPE_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string AddressTypeUpdtId { get; set; } = null!;

    [Column("ADDRESS_TYPE_UPDT_DT", TypeName = "DATE")]
    public DateTime AddressTypeUpdtDt { get; set; }

    [InverseProperty("CustomerAddressAddressType")]
    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
}
