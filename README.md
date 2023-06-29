# nanoframeworkStackCorruption

This is example code to debug corruption of stack in nf interpreter.
Where exectly problem originates is unknown but after running this for few dozen seconds it's clear that stack is corrupted.
Method in https://github.com/nanoframework/nf-interpreter/blob/main/src/nanoFramework.System.Collections/nf_system_collections_System_Collections_Hashtable.cpp
faults at line:
> key = stack.Arg1().Dereference();
> FAULT_ON_NULL_ARG(key);
what suggest that it can't read properly value from the stack. It's a value type (may be wrapped in object) and when logging it from C# code it clearly isn't null.

Problematic is that this doesn't happend first time (and as of now it's not confirmed does it happen every time), but Method hashtable.Add(...) is called thousands 
of times and errors can be seen easily.
