# RLSharp
RLSharp is a framework to create Rocket League bots in .NET languages (C#, Visual Basic, C++, ...).

# Installation
* Download RLBot.dll on commit 487a268 from the RLBot repository (https://github.com/drssoccer55/RLBot/).
* Download RLSharp.dll v1.0.0 from the RLSharp releases (https://github.com/TheBlocks/RLSharp/releases/tag/v1.0.0).
* Install vJoy 2.x by running the setup executable (https://sourceforge.net/projects/vjoystick/files/latest/download).
* Create a new Visual Studio project and add RLSharp.dll as a reference.
* Add vJoyInterfaceWrap.dll (found in the vJoy installation directory) as a reference.
* Add vJoyInterface.dll (found in the vJoy installation directory) as a reference, by right clicking the solution in the Solution Explorer window, clicking Add -> Add existing and selecting the DLL. Make sure to set its "Build Action" to "Content" and "Copy to Output Directory" to "Copy if newer" in the Properties window.
* Install x360ce in your Rocket League executable folder, and run it. When it asks you about the vJoy controllers, add them.
* When running Rocket League, inject RLBot.dll into RocketLeague.exe using Cheat Engine (or whatever injector you prefer).

Detailed instructions can be found on the RLBot wiki here: https://github.com/drssoccer55/RLBot/wiki/Setup-Instructions. Please note that not all of the steps are relevant to RLSharp (i.e. you do not need to install pyvjoy or Python or move vJoyInterface.dll to any folder other than the installation directory). The relevant sections are: "Install x360ce", "Configure VJoy", "Set up x360ce", and "Troubleshooting" (except where Python is involved).

Instructions for injecting RLBot.dll into RocketLeague.exe can be found here: https://github.com/drssoccer55/RLBot/wiki/Injecting-DLL-Instructions.

There are plans to move RLSharp away from vJoy and move towards vXbox so that bot development is less of a hassle.

# Acknowledgements
* Massive thanks to drssoccer55 (https://github.com/drssoccer55) for creating RLBot. Without RLBot, I wouldn't know where to start with making RLSharp.
* Another massive thanks to ccman32 (https://github.com/ccman32) for creating RLBot.dll so that game data can be accurately and for helping me with reading the DLL.
