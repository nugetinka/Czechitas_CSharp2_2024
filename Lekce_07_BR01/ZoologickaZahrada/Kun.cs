﻿namespace Lekce_07_BR01.ZoologickaZahrada;

public class Kun : Zvire
{
    public int RychlostBehu { get; private set; }

    public Kun(int vek, string druh, int rychlostBehu) : base(vek, druh)
    {
    }

    public override void VydavejZvuk()
    {
        Console.WriteLine("ihaaa");
    }
}
