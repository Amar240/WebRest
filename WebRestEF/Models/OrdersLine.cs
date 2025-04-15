using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("ORDERS_LINE")]
public partial class OrdersLine
{
    [Key]
    [Column("ORDERS_LINE_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string OrdersLineId { get; set; } = null!;

    [Column("ORDERS_LINE_ORDERS_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string OrdersLineOrdersId { get; set; } = null!;

    [Column("ORDERS_LINE_PRODUCT_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string OrdersLineProductId { get; set; } = null!;

    [Column("ORDERS_LINE_QTY")]
    [Precision(4)]
    public byte OrdersLineQty { get; set; }

    [Column("ORDERS_LINE_PRICE", TypeName = "NUMBER(9,2)")]
    public decimal OrdersLinePrice { get; set; }

    [Column("ORDERS_LINE_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrdersLineCrtdId { get; set; } = null!;

    [Column("ORDERS_LINE_CRTD_DT", TypeName = "DATE")]
    public DateTime OrdersLineCrtdDt { get; set; }

    [Column("ORDERS_LINE_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrdersLineUpdtId { get; set; } = null!;

    [Column("ORDERS_LINE_UPDT_DT", TypeName = "DATE")]
    public DateTime OrdersLineUpdtDt { get; set; }

    [ForeignKey("OrdersLineOrdersId")]
    [InverseProperty("OrdersLines")]
    public virtual Order OrdersLineOrders { get; set; } = null!;

    [ForeignKey("OrdersLineProductId")]
    [InverseProperty("OrdersLines")]
    public virtual Product OrdersLineProduct { get; set; } = null!;
}
