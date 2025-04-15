using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("ORDER_STATUS")]
public partial class OrderStatus
{
    [Key]
    [Column("ORDER_STATUS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string OrderStatusId { get; set; } = null!;

    [Column("ORDER_STATUS_DESC")]
    [StringLength(20)]
    [Unicode(false)]
    public string OrderStatusDesc { get; set; } = null!;

    [Column("ORDER_STATUS_NEXT_ORDER_STATUS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string? OrderStatusNextOrderStatusId { get; set; }

    [Column("ORDER_STATUS_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrderStatusCrtdId { get; set; } = null!;

    [Column("ORDER_STATUS_CRTD_DT", TypeName = "DATE")]
    public DateTime OrderStatusCrtdDt { get; set; }

    [Column("ORDER_STATUS_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrderStatusUpdtId { get; set; } = null!;

    [Column("ORDER_STATUS_UPDT_DT", TypeName = "DATE")]
    public DateTime OrderStatusUpdtDt { get; set; }

    [InverseProperty("OrderStatusNextOrderStatus")]
    public virtual ICollection<OrderStatus> InverseOrderStatusNextOrderStatus { get; set; } = new List<OrderStatus>();

    [InverseProperty("OrderStateOrderStatus")]
    public virtual ICollection<OrderState> OrderStates { get; set; } = new List<OrderState>();

    [ForeignKey("OrderStatusNextOrderStatusId")]
    [InverseProperty("InverseOrderStatusNextOrderStatus")]
    public virtual OrderStatus? OrderStatusNextOrderStatus { get; set; }
}
