# Spinballs 1.2 -- dev branch

![Logo](Images/logo.png)

## Preface
I found veery Spinballs old theme on 4PDA site. I tried that oldie, but game don't want to run. So, I decide(d) to do some little micro-RnD. :) 


## Game scenario 
- The game consists of seven discs, with six color balls. Each disk can rotate in both directions. You need to collect one whole ball from the same side balls and it will disappear, and you will get points for it. But do not forget about the running time.

## Screenshots
![](Images/sshot01.png)
![](Images/sshot02.png)


## Status of RnD
- PC: game runs ok... but no game-screen autoscaling after window resize
- Lumia 950 smartphone: no touch support (coordinate transfer problems?), and no screen autoscaling  
- Game save/load damaged
- God mode on: game over blocked by me (for game debug simplify.) 
- Language strings not ready (only tech. words at now)

## Tech details
- UWP app (micro-game)
- Min. Win. SDK used: 10240 (Astoria compatibility)
- Monogame "engine" used
- VSCode/KiloCode's Qwen-3 AI used for simple gamedev "recovering" 

## Main Tasks realized
- [+] Game controls (tune touch mode, add mouse mode) fixed
- [+] CSound / music theme, and Settings save/restore fixed
- [+-] Fix game screen scaling (some Astoria bug still there!)

## Know problems
- On W10M Astoria, some screen distortion persists... Try to change screen orientation twise :)
- No Russian localization (no Cyrillic symbols at all xap fonts, so I renamed ru-RU folder to _ru-RU to avoid game crashing!)


## TODO / Current Goal
- Polish and test final gameplay experience 
- Realize game continuation (i.e, tap on High score item to restore game level, score, etc.) 
- Add some new gamification, such as coins, extra-lives, wall breaks, god-mode, etc... :) 

## Reference
- https://4pda.to/forum/index.php?showtopic=218315 4PDA Windows Phone :: Spinballs archived theme

## ..
As is. No support. Educational purposes only / Retro-Coding in pair with AI. Just-for-funnn! 

## .
[m][e] Nov, 29 2025

![Logo](Images/footer.png)