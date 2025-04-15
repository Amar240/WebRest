using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("CUSTOMER")]
public partial class Customer
{
    [Key]
    [Column("CUSTOMER_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string CustomerId { get; set; } = null!;

    [Column("CUSTOMER_FIRST_NAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string CustomerFirstName { get; set; } = null!;

    [Column("CUSTOMER_MIDDLE_NAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CustomerMiddleName { get; set; }

    [Column("CUSTOMER_LAST_NAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string CustomerLastName { get; set; } = null!;

    [Column("CUSTOMER_DATE_OF_BIRTH", TypeName = "DATE")]
    public DateTime? CustomerDateOfBirth { get; set; }

    [Column("CUSTOMER_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CustomerCrtdId { get; set; } = null!;

    [Column("CUSTOMER_CRTD_DT", TypeName = "DATE")]
    public DateTime CustomerCrtdDt { get; set; }

    [Column("CUSTOMER_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string CustomerUpdtId { get; set; } = null!;

    [Column("CUSTOMER_UPDT_DT", TypeName = "DATE")]
    public DateTime CustomerUpdtDt { get; set; }

    [Column("CUSTOMER_GENDER_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string? CustomerGenderId { get; set; }

    [InverseProperty("CustomerAddressCustomer")]
    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    [ForeignKey("CustomerGenderId")]
    [InverseProperty("Customers")]
    public virtual Gender? CustomerGender { get; set; }

    [InverseProperty("OrdersCustomer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
