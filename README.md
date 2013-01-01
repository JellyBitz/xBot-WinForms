# xBot
[xBot](https://projexbot.blogspot.com/) - A simple but elegant bot for Silkroad Online! (vsro1.188)

### Features
- Client loader included
- Clientless support
- Auto Login from command line
- Auto Potions (Pets & transport included)
- Auto Party (Party matching included)
- Chat leader commands controller
- Character inventory
- Chatting
- Minimap
- Game info viewer, also known as "spy" but better
- Packet analyzer (Filter & injector included)

#### Command Line Options:
[code]
-silkroad=@param    - Silkroad name to select
-username=@param    - Game ID
-password=@param    - Game Password
-server=@param      - Game server channel
-captcha=@param     - NOT IMPLEMENTED
-character=@param   - Character name
--clientless        - Use clientless mode
--relogin           - Activate relogin on disconnect
--goclientless      - Try to go clientless mode after joined to the game
--usereturn         - Try to use a return scroll from inventory after joined to the game
[/code]

#### Chat Leader Commands:
[code]
TRACE ?Charname                   - Start trace to player
NOTRACE                           - Stop trace
RETURN                            - Try to use a return scroll from inventory
INJECT *Opcode ?Encrypted ?Data   - Inject a packet
[/code]

Only the player(s) into the leader list can use this commands
But don't worry, this commands will work only if you had `checked` the respective option from `Party > Settings`

All commands are UPPERCASED. However, the command won't work if you don't follow the indications as `*Required` `?Optional`
A few examples below:
[code]
"TRACE JellyBitz"
"INJECT 3091 false 01"
"INJECT 3091 01"
[/code]

------------
> **Do you like this Project ?**
> Support me! [Buy me a coffee <img src="https://twemoji.maxcdn.com/2/72x72/2615.png" width="18" height="18">](https://www.buymeacoffee.com/JellyBitz "Coffee <3")
>
> Made with <img title="Love" src="https://twemoji.maxcdn.com/2/72x72/1f499.png" width="18" height="18"> for Silkroad community. Pull if you want! <img title="JellyBitz" src="https://twemoji.maxcdn.com/2/72x72/1f575.png" width="18" height="18">

#### Credits and special thanks!
- Drew "pushedx" Benton for his past work and sources!
- DaxterSoul for his time and help about packet structures

------------
> ###### Created on VisualStudio2014 with NET4.5
> - CefSharp web browser
> - FontAwesome Icons
> - Leaflet maps
> - Silkroad Images and others..