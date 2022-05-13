# SMX (Quartz) compatibility

SMX and Quartz (formerly known as SMXlib) are (c) by Sirillion, Laydor et al.
Please check https://www.nexusmods.com/7daystodie/mods/22 for their wonderful UI mod!

I'm including a version of Quart.dll, masqueraded as Quart.lib, so it is not picked up
by either the game mod loaded or any other loader, in order to allow CI to compile the dll.
There is no other need to include it here beside that. I researched on how to just include
a "header" of the dll in order to compile it, but not sure if that is doable for csharp?
On runtime, it will depend on the actual loaded Quart.dll from the SMXcore mod!