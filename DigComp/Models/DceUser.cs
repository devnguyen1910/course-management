using System;
using System.Collections.Generic;

namespace DigComp.Models;

public partial class DceUser
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public string? Email { get; set; }
}
