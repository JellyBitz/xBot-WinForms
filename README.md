# xBot
[<img src="https://1.bp.blogspot.com/-C9g73Lled-8/XSbNNtzCyII/AAAAAAAAA8o/Ho6JXt8pdygdjGwEJ_YCXCQye8HngxrFQCLcBGAs/s500-c/icon.ic" width="18" height="18"> xBot](https://projexbot.blogspot.com/ "xBot v0.0.6") - A simple but elegant bot for Silkroad Online! (vsro1.188)

### Features
- Client loader included
- Clientless support
- Auto Login from [command line](#Command-Line-Options)
- Auto Potions (Pets & transport included)
- Auto Party (Party matching included)
- Leader [commands](#Chat-Leader-Commands) control from chat
- Character inventory
- Chatting
- Minimap
- Game info viewer, also known as "Spy" but better!
- Packet analyzer (Filter & injector included)

---
#### Command Line Options :
| Command | Description |
| :----: | :--- |
|`-silkroad=?`| Silkroad name to select
|`-username=?`| Game ID
|`-password=?`| Game Password
|`-server=?`| Game server channel
|`-captcha=?`| NOT IMPLEMENTED
|`-character=?`| Character name
|`--clientless`| Use clientless mode
|`--relogin`| Activate relogin on disconnect
|`--goclientless`| Go clientless mode after joined to the game
|`--usereturn`| Use a return scroll from inventory after joined to the game

#### Chat Leader Commands :
| Command | Description |
| :---: | :--- |
|`TRACE ?Charname`| Start trace to player
|`NOTRACE`| Stop trace
|`RETURN`| Use a return scroll from inventory
|`INJECT *Opcode ?Encrypted ?Data`| Inject packet
|`TELEPORT *SourceName *DestinationName`| Use a teleport
|`TELEPORT *SourceModelID *DestinationModelID`| Use a teleport

All commands are **UPPERCASED**. However, the command won't work if you don't follow the indications as `*Required` `?Optional`

A few using examples from any chat:
- `TRACE JellyBitz`
- `INJECT 3091 false 01`
- `TELEPORT Ferry ticket seller doji,Ferry ticket seller tayun`
- `TELEPORT 2011 2056`
- `INJECT 3091 01`

Only the player(s) into the **Leader list** can use this commands
This commands will work only if you had **checked** the option from **Party > Settings**

#### Tips:
- Right click from the buff list will remove the buff

---
> ### **Do you like this Project ?**
> ### Support me! [Buy me a coffee <img src="https://twemoji.maxcdn.com/2/72x72/2615.png" width="18" height="18">](https://www.buymeacoffee.com/JellyBitz "Coffee <3")
>
> Made with <img title="Love" src="https://twemoji.maxcdn.com/2/72x72/1f499.png" width="18" height="18"> for Silkroad community. Pull if you want! <img title="JellyBitz" src="https://twemoji.maxcdn.com/2/72x72/1f575.png" width="18" height="18">

---
> ##### Credits and special thanks!
> - Drew "pushedx" Benton for his past work and sources!
> - DaxterSoul for his time and help about packet structures
> - [ConfuserEx2](https://github.com/mkaring/ConfuserEx) to let me create a free software!
>
> ##### Created on VisualStudio2015 with NET4.6
> - CefSharp web browser
> - FontAwesome Icons
> - Leaflet maps
> - Silkroad Images and others..