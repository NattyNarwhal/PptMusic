This is a wrapper in C# (1.1, what I had handy in the VM I poked the add-in in)
for the strange [PowerPoint custom soundtracks add-in][1] (further discussion
is on the [Lobsters submission][1]). It comes with an example application to
drive the library that controls it.

## Reverse engineering of the DLL

Surprisingly little. The API prototypes are in the add-in's VBA-driven side,
and if you open it in VBA's object browser, all the prototypes and enum values
are present. Adapt to C#, and an object-oriented wrapper is obvious.

## TODO

Convert to modern C# (trivial), investigate more of the API, investigate the
rest of DirectMusic and its history, and consider more OO wrappers.

[1]: https://cmpct.info/~calvin/Articles/PowerPointSoundtracks/
[2]: https://lobste.rs/s/09fwse/microsoft_s_forgotten_midi_soundtrack