using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigComp.Models;

public partial class DceUser
{
    [Key]
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public bool? Gender { get; set; }

    public string? Email { get; set; }
}
