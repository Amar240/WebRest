using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("ORDERS")]
public partial class Order
{
    [Key]
    [Column("ORDERS_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string OrdersId { get; set; } = null!;

    [Column("ORDERS_DATE")]
    [Precision(6)]
    public DateTime OrdersDate { get; set; }

    [Column("ORDERS_CUSTOMER_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string OrdersCustomerId { get; set; } = null!;

    [Column("ORDERS_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrdersCrtdId { get; set; } = null!;

    [Column("ORDERS_CRTD_DT", TypeName = "DATE")]
    public DateTime OrdersCrtdDt { get; set; }

    [Column("ORDERS_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string OrdersUpdtId { get; set; } = null!;

    [Column("ORDERS_UPDT_DT", TypeName = "DATE")]
    public DateTime OrdersUpdtDt { get; set; }

    [InverseProperty("OrderStateOrders")]
    public virtual ICollection<OrderState> OrderStates { get; set; } = new List<OrderState>();

    [ForeignKey("OrdersCustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer OrdersCustomer { get; set; } = null!;

    [InverseProperty("OrdersLineOrders")]
    public virtual ICollection<OrdersLine> OrdersLines { get; set; } = new List<OrdersLine>();
}
