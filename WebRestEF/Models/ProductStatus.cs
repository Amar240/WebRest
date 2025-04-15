using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("PRODUCT_STATUS")]
public partial class ProductStatus
{
    [Key]
    [Column("PRODUCT_STATUS_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string ProductStatusId { get; set; } = null!;

    [Column("PRODUCT_STATUS_DESC")]
    [StringLength(32)]
    [Unicode(false)]
    public string ProductStatusDesc { get; set; } = null!;

    [Column("PRODUCT_STATUS_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ProductStatusCrtdId { get; set; } = null!;

    [Column("PRODUCT_STATUS_CRTD_DT", TypeName = "DATE")]
    public DateTime ProductStatusCrtdDt { get; set; }

    [Column("PRODUCT_STATUS_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ProductStatusUpdtId { get; set; } = null!;

    [Column("PRODUCT_STATUS_UPDT_DT", TypeName = "DATE")]
    public DateTime ProductStatusUpdtDt { get; set; }

    [InverseProperty("ProductProductStatus")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
