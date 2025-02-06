using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Role
{
    public int PkRoleId { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<UserRole>? UserRoles { get; set; } = new List<UserRole>();
}
