﻿using API.Models.Entities;

namespace API.Models.ValueObjects;

public class Address : Entitie
{
    public string? Country { get; private set; }
    public string? State { get; private set; }
    public string? City { get; private set; }
    public string? ZipCode { get; private set; }
    public string? District { get; private set; }
    public string? Street { get; private set; }
    public string? Additional { get; private set; }
    public int Number { get; private set; }
}
