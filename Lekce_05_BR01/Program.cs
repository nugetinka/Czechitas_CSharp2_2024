﻿namespace Lekce_05_BR01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Definice Banky
    public class Banka
    {
        public string Symbol { get; set; }
        public string Jmeno { get; set; }
    }

    // Definice Zákazníka
    public class Zakaznik
    {
        public string Jmeno { get; set; }
        public double Zustatek { get; set; }
        public string Banka { get; set; }
    }

    // Definice Skupiny milionářů
    public class SkupinaMilionaru
    {
        public string Banka { get; set; }
        public IEnumerable<string> Milionari { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            // ==========================================
            // LINQ
            // 1. Nalezněte slova začínající písmenem 'M'
            List<string> ovoce = new List<string>() { "Merunka", "Jablko", "Pomeranc", "Meloun", "Malina", "Limetka" };

            // 1. Řešení
            List<string> mOvoce = ovoce.Where(o => o.StartsWith("M")).ToList();

            foreach (string o in mOvoce)
            {
                Console.WriteLine(o);
            }

            // ==========================================		
            // 2. Která z následujících čísel jsou násobky 4 nebo 6
            List<int> ruznaCisla = new List<int>()
        {
            15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96, 15
        };

            // 2. Řešení
            List<int> nasobky4a6 = ruznaCisla.Where(c => c % 4 == 0 || c % 6 == 0).ToList();

            foreach (int cislo in nasobky4a6)
            {
                Console.WriteLine(cislo);
            }

            // 3. Kolik je v seznamu ruznaCisla čísel?
            int pocetCisel = ruznaCisla.Count();
            Console.WriteLine(pocetCisel);

            // počet unikátních čísel
            int pocetUnikatnichCisel = ruznaCisla.Distinct().Count();
            Console.WriteLine(pocetUnikatnichCisel);

            // ==========================================
            // 4. Seřaďte jména vzestupně
            List<string> jmena = new List<string>()
        {
            "Hana", "Jaroslav", "Xenie", "Michaela", "Borivoj", "Nela",
            "Katerina", "Sofie", "Adam", "David", "Zuzana", "Barbara",
            "Tereza", "Lenka", "Svetlana", "Cecilie", "Renata",
            "Evzen", "Pavel", "Eliska", "Viktor", "Antonin",
            "Frantisek", "Radek"
        };

            // 4. Řešení
            List<string> vzestupne = jmena.Order().ToList();

            foreach (string text in vzestupne)
            {
                Console.WriteLine(text);
            }

            // ==========================================
            // 5. Kolik je celkový součet?
            List<double> utrata = new List<double>()
        {
            2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
        };

            // 5. Řešení
            double soucet = utrata.Sum();
            Console.WriteLine(soucet);

            // ==========================================		
            // 6. Jaké je největší cena?
            List<double> cena = new List<double>()
        {
            879.45, 9442.85, 2454.63, 45.65, 2340.29, 34.03, 4786.45, 745.31, 21.76
        };

            double nejvetsiCena = cena.Max();

            // 6. Řešení
            Console.WriteLine(nejvetsiCena);

            // ==========================================		
            // 7. Zobrazte vsechny milionare v kazde bance
            // Napr. 
            // CS: Jan Novak a Josef Novotny
            // KB: Jana Nova

            List<Zakaznik> zakaznici = new List<Zakaznik>() {
            new Zakaznik(){ Jmeno="Jan Maly", Zustatek=10345.50, Banka="CS"},
            new Zakaznik(){ Jmeno="Jiri Hladny", Zustatek=452.10, Banka="KB"},
            new Zakaznik(){ Jmeno="Lenka Sporiva", Zustatek=523665.13, Banka="CS"},
            new Zakaznik(){ Jmeno="Marie Bohata", Zustatek=7482184.38, Banka="FIO"},
            new Zakaznik(){ Jmeno="Michal Marny", Zustatek=745234.93, Banka="KB"},
            new Zakaznik(){ Jmeno="Lada Vychytraly", Zustatek=8832937.34, Banka="CS"},
            new Zakaznik(){ Jmeno="Sandra Nedostatecna", Zustatek=942488.48, Banka="KB"},
            new Zakaznik(){ Jmeno="Silvie Zavodou", Zustatek=56198334.72, Banka="FIO"},
            new Zakaznik(){ Jmeno="Tereza Presna", Zustatek=1000000.00, Banka="CITI"},
            new Zakaznik(){ Jmeno="Stefan Pilny", Zustatek=48282.73, Banka="CITI"}
        };

            // 7. Řešení

            List<SkupinaMilionaru> skupinyPodleBanky = zakaznici
                .Where(z => z.Zustatek >= 1_000_000)
                .GroupBy(z => z.Banka, (kodBanky, milionari) => new SkupinaMilionaru
                {
                    Banka = kodBanky,
                    Milionari = milionari.Select(m => m.Jmeno)
                })
                .ToList();

            foreach (var polozka in skupinyPodleBanky)
            {
                Console.WriteLine(polozka.Banka + ": " + string.Join(" a ", polozka.Milionari));
            }

            // ==========================================		
            // 8. Vytisknete jmeno kazdeho milionare a jeho banky
            // Napr
            // Jan Novak v Ceska Sporitelna
            // Josef Novotny v Komercni Banka

            List<Banka> banky = new List<Banka>() {
                new Banka(){ Jmeno="Ceska Sporitelna", Symbol="CS"},
                new Banka(){ Jmeno="Komercni Banka", Symbol="KB"},
                new Banka(){ Jmeno="Fio Banka", Symbol="FIO"},
                new Banka(){ Jmeno="Citibank", Symbol="CITI"},
            };

            // 8. Řešení

            var reportMilionaru = from b in banky
                                  join z in zakaznici
                                  on b.Symbol equals z.Banka
                                  where z.Zustatek >= 1_000_000
                                  select new
                                  {
                                      JmenoMilionare = z.Jmeno,
                                      JmenoBanky = b.Jmeno
                                  };

            foreach (var polozka in reportMilionaru)
            {
                Console.WriteLine(polozka.JmenoMilionare + " v " + polozka.JmenoBanky);
            }
        }
    }
}
