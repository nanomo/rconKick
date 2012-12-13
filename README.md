RCon Kicker for Arma2/DayZ
===========================

This is a kicker utility originally (fast)develped to https://github.com/Torndeco/pyBEscanner


Prerequisites
=============

 - BattleNET https://github.com/ziellos2k/BattleNET
 - Microsoft .NET Framework 3.5

Installation
============

No needed. 


Usage
=========

**Parameters** 

1. RCon: -ip, -port, -password
2. -file absolute path to the file that contains a list of names to be kicked from the game
3. -kickall: kick all the current players, i saw this request in the community

Command Lines Examples:

To Kick the current players that exists in the file "players_to_kick.txt"
rcon_kick.exe -ip="123.123.123.123" -port="2402" -password="zedar" -file="I:\Rock\players_to_kick.txt"

To kick all the ingame players
rcon_kick.exe -ip="123.123.123.123" -port="2402" -password="zedar" -kickall



Credits
========

Developer: nanomo

Zedar.com.ar the biggest Zombie Community in LATAM


Support
=======

**Twitter**: @nanomo
**Forum**: http://zedar.com.ar/forum/index


License
=======

Do what ever you want, be happy.
