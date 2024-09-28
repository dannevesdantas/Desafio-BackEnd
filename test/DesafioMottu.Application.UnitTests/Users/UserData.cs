﻿using DesafioMottu.Domain.Users;

namespace DesafioMottu.Application.UnitTests.Users;

internal static class UserData
{
    public static User Create() => User.Create(Name, Cnpj, BirthDate);

    public static readonly Name Name = new("First");
    public static readonly Cnpj Cnpj = new("14.143.385/0001-50");
    public static readonly DateOnly BirthDate = new(1990, 1, 1);
}
