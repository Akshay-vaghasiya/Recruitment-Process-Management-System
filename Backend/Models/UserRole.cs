using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class UserRole
{
    public int PkUserRoleId { get; set; }

    public int? FkUserId { get; set; }

    public int? FkRoleId { get; set; }

    public Role? FkRole { get; set; }

    [JsonIgnore]
    public User? FkUser { get; set; }
}
