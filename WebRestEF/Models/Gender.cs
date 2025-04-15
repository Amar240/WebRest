using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebRest.EF.Models;

[Table("GENDER")]
public partial class Gender
{
    [Key]
    [Column("GENDER_ID")]
    [StringLength(38)]
    [Unicode(false)]
    public string GenderId { get; set; } = null!;

    [Column("GENDER_NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string GenderName { get; set; } = null!;

    [Column("GENDER_CRTD_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string GenderCrtdId { get; set; } = null!;

    [Column("GENDER_CRTD_DT", TypeName = "DATE")]
    public DateTime GenderCrtdDt { get; set; }

    [Column("GENDER_UPDT_ID")]
    [StringLength(40)]
    [Unicode(false)]
    public string GenderUpdtId { get; set; } = null!;

    [Column("GENDER_UPDT_DT", TypeName = "DATE")]
    public DateTime GenderUpdtDt { get; set; }

    [InverseProperty("CustomerGender")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
