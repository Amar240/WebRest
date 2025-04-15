using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("CUSTOMER_ADDRESS")]
public partial class CustomerAddress
{
    [Key]
    [Column("CUSTOMER_ADDRESS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CustomerAddressId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_CUSTOMER_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CustomerAddressCustomerId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_ADDRESS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CustomerAddressAddressId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_ADDRESS_TYPE_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string CustomerAddressAddressTypeId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_ACTV_IND", TypeName = "NUMBER(1)")]
    public bool CustomerAddressActvInd { get; set; }

    [Column("CUSTOMER_ADDRESS_DEFAULT_IND", TypeName = "NUMBER(1)")]
    public bool CustomerAddressDefaultInd { get; set; }

    [Column("CUSTOMER_ADDRESS_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CustomerAddressCrtdId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_CRTD_DT", TypeName = "DATE")]
    public DateTime CustomerAddressCrtdDt { get; set; }

    [Column("CUSTOMER_ADDRESS_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CustomerAddressUpdtId { get; set; } = null!;

    [Column("CUSTOMER_ADDRESS_UPDT_DT", TypeName = "DATE")]
    public DateTime CustomerAddressUpdtDt { get; set; }

    [ForeignKey("CustomerAddressAddressId")]
    [InverseProperty("CustomerAddresses")]
    public virtual Address CustomerAddressAddress { get; set; } = null!;

    [ForeignKey("CustomerAddressAddressTypeId")]
    [InverseProperty("CustomerAddresses")]
    public virtual AddressType CustomerAddressAddressType { get; set; } = null!;

    [ForeignKey("CustomerAddressCustomerId")]
    [InverseProperty("CustomerAddresses")]
    public virtual Customer CustomerAddressCustomer { get; set; } = null!;
}
