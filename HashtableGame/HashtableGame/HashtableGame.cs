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
        RepopulateTables();
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
    }

    //something to force reading
    void SwapTables(Hashtable alpha, Hashtable beta)
    {
        var alphaPrime = alpha.Clone() as Hashtable;
        alpha.Clear();
        CopyContent(beta, alpha);
        CopyContent(alphaPrime, beta);
    }

    void RepopulateTables() {
        foreach (var map in tables) {
            var size = 15 + lottery.Next(55) - map.Count;
            if (size > 0) {
                RepopulateTable(map, size);
            }
        }
    }

    void CopyContent(Hashtable source, Hashtable destination) {
        object nextKey = null;
        object nextValue = null;
        try {
            foreach (var entry in source.Keys) {
                nextKey = entry;
                nextValue = source[entry];
                destination.Add(entry, source[entry]);
            }
        } catch (Exception error) {
            Console.WriteLine($"failed to add {nextKey}:{nextValue} to hashtable: " + error);
        }
    }

    ulong keyCount = 0;
    void RepopulateTable(Hashtable target, int howMany) {
        for (int i = 0; i < howMany; i++) {
            target.Add(keyCount++, "Jose we have a problem :-)");
        }
    }
}
