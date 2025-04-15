using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("PRODUCT_PRICE")]
public partial class ProductPrice
{
    [Key]
    [Column("PRODUCT_PRICE_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string ProductPriceId { get; set; } = null!;

    [Column("PRODUCT_PRICE_PRODUCT_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string ProductPriceProductId { get; set; } = null!;

    [Column("PRODUCT_PRICE_EFF_DATE", TypeName = "DATE")]
    public DateTime ProductPriceEffDate { get; set; }

    [Column("PRODUCT_PRICE_PRICE", TypeName = "NUMBER(9,2)")]
    public decimal ProductPricePrice { get; set; }

    [Column("PRODUCT_PRICE_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ProductPriceCrtdId { get; set; } = null!;

    [Column("PRODUCT_PRICE_CRTD_DT", TypeName = "DATE")]
    public DateTime ProductPriceCrtdDt { get; set; }

    [Column("PRODUCT_PRICE_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string ProductPriceUpdtId { get; set; } = null!;

    [Column("PRODUCT_PRICE_UPDT_DT", TypeName = "DATE")]
    public DateTime ProductPriceUpdtDt { get; set; }

    [ForeignKey("ProductPriceProductId")]
    [InverseProperty("ProductPrices")]
    public virtual Product ProductPriceProduct { get; set; } = null!;
}
