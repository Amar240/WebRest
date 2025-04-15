using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("ORDER_STATE")]
public partial class OrderState
{
    [Key]
    [Column("ORDER_STATE_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string OrderStateId { get; set; } = null!;

    [Column("ORDER_STATE_ORDERS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string OrderStateOrdersId { get; set; } = null!;

    [Column("ORDER_STATE_ORDER_STATUS_ID")]
    [StringLength(32)]
    [Unicode(false)]
    public string OrderStateOrderStatusId { get; set; } = null!;

    [Column("ORDER_STATE_EFF_DATE", TypeName = "DATE")]
    public DateTime OrderStateEffDate { get; set; }

    [Column("ORDER_STATE_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrderStateCrtdId { get; set; } = null!;

    [Column("ORDER_STATE_CRTD_DT", TypeName = "DATE")]
    public DateTime OrderStateCrtdDt { get; set; }

    [Column("ORDER_STATE_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrderStateUpdtId { get; set; } = null!;

    [Column("ORDER_STATE_UPDT_DT", TypeName = "DATE")]
    public DateTime OrderStateUpdtDt { get; set; }

    [ForeignKey("OrderStateOrderStatusId")]
    [InverseProperty("OrderStates")]
    public virtual OrderStatus OrderStateOrderStatus { get; set; } = null!;

    [ForeignKey("OrderStateOrdersId")]
    [InverseProperty("OrderStates")]
    public virtual Order OrderStateOrders { get; set; } = null!;
}
