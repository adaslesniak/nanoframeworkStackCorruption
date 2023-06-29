using System;
using System.Collections;
using System.Diagnostics;
namespace HashtableGame;

internal class HashtableGame
{
    Random lottery = new();
    Hashtable[] tables;

    internal HashtableGame(int howManyTables = 50) {
        PrepareTables(howManyTables);
    }

    void PrepareTables(int howMany) {
        tables = new Hashtable[howMany];
        for (int i = 0; i < howMany; i++) {
            tables[i] = new();
        }
    }

    internal void Execute() {
        for (int i = 1; i < tables.Length; i += 2) {
            SwapTables(tables[i - 1], tables[i]);
        }
        RepopulateTables();
    }

    //something to force reading
    void SwapTables(Hashtable alpha, Hashtable beta) {
        var alphaPrime = alpha.Clone() as Hashtable;
        alpha.Clear();
        object nextKey = null;
        object nextValue = null;
        try {
            foreach (var entry in beta.Keys) {
                nextKey = entry;
                nextValue = beta[entry];
                alpha.Add(entry, beta[entry]);
            }
            foreach (var entry in alphaPrime.Keys) {
                nextKey = entry;
                nextValue = alphaPrime[entry];
                beta.Add(entry, alphaPrime[entry]);
            }
        }
        catch (Exception error) {
            Console.WriteLine($"failed to add {nextKey}:{nextValue} to hashtable: " + error);
        }
    }

    void RepopulateTables() {
        foreach (var map in tables) {
            RandomCleanup(map);
            var size = 15 + lottery.Next(55) - map.Count;
            if (size > 0) {
                RepopulateTable(map, size);
            }
        }
    }

    void RandomCleanup(Hashtable target)
    {
        var everyX = 1 + lottery.Next(3);
        var iteration = 0;
        object[] toBeRemoved = new object[target.Keys.Count];
        foreach (var entry in target.Keys) {
            if (iteration++ % everyX != 0) {
                continue;
            }
            if (iteration >= target.Count) {
                break;
            }
            toBeRemoved[iteration] = entry;
        }
        foreach (var item in toBeRemoved) {
            if (item is null) {
                continue;
            }
            target.Remove(item);
        }
    }

    ulong keyCount = 0;
    void RepopulateTable(Hashtable target, int howMany) {
        for (int i = 0; i < howMany; i++) {
            target.Add(keyCount++, RandomText());
        }
    }

    string RandomText() {
        var howLong = lottery.Next(511);
        var stupidText = "";
        for (int i = 0; i < howLong; i++) {
            stupidText += (char)lottery.Next(254);
        }
        return stupidText;
    }
}
