# Cain, the Penitent

A hero mod, introducing Cain, a Heron who has suffered so much in service of corruption that he became a Flagellant to atone for his sins.

This currently does not include any events or quests related. It will likely not be updated with events in the future unless I can think of a good sub area.

A couple of notes:
## Notes:
- I understand that things are going to be janky at times, and there are definitely bugs that will be worked out. Let me know if you come across any.
_ I attempted to make some mouse-over text (a la Magnus/Yogger). Let me know how it turns out, it seems to be different on different resolutions, though not certain.
- **What to do if the hero is not unlocked:** Due to some jankiness of the way the code works, heroes are unlocked only for the profile that is open when you launch the game (and for new profiles). So if they aren't unlocked in the correct profile, switch to that profile, close the game and re-open it and they will be unlocked. I'll fix this in the future, but most people won't notice it. You can also just use the profile editor to fix it.
- There are **no character events** for Cain at this time. I will try to add some in the future, but need to finish my event editing mod first.

This mod relies on [Obeliskial Content](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/).

<details>
<summary>Traits</summary>

### Level 1
The Meek shall Inherit: Weak does not reduce this hero's damage, but Powerful does. Gain 5% damage for each unique Curse on this hero.


### Level 2

![Blood for the Burden](https://github.com/binbinmods/ThePenitent/blob/main/Assets/bloodburden.png?raw=true)

![Armored in Faith](https://github.com/binbinmods/ThePenitent/blob/main/Assets/armoredinfaith.png?raw=true)

### Level 3

Ardea Dei: Draw 2 cards, gain 1 Energy, and gain 1 Vitality when you play an Injury (3x/turn)  
Qui Tollis Peccata: Vitality +1. When you apply Vitality to a different hero, Steal 1 Curse from them. When you play an Injury, gain 2 Vitality.

### Level 4

![Blessed are the Merciful](https://github.com/binbinmods/ThePenitent/blob/main/Assets/blessedmerciful.png?raw=true)

![Blessed are the Pure of Heart](https://github.com/binbinmods/ThePenitent/blob/main/Assets/purehearted.png?raw=true)

### Level 5

Guided by Pain!: Zeal on this hero increases All Damage and Healing by (1+X) per charge, where X is the number of Injuries in your deck at the start of combat.  
Favored by the Dark!: Vitality increases All Damage by 0.3 per charge. Once per turn, when you Heal a hero, apply Vitality equal to (10+3X)% of all Curses on this hero, where X is the number of Injuries in your deck at the start of combat.  

</details>


## Installation (manual)

1. Install [Obeliskial Essentials](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Essentials/) and [Obeliskial Content](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/).
2. Click _Manual Download_ at the top of the page.
3. In Steam, right-click Across the Obelisk and select _Manage_->_Browse local files_.
4. Extract the archive into the game folder. Your _Across the Obelisk_ folder should now contain a _BepInEx_ folder and a _doorstop\_libs_ folder.
5. Run the game. If everything runs correctly, you will see this mod in the list of registered mods on the main menu.
6. Press F5 to open/close the Config Manager and F1 to show/hide mod version information.
7. Note: I am not certain about these install instructions. In the worst case, just copy `com.binbin.ThePenitent.dll` into the `BepInEx\plugins` folder, and the _Ulfvitr_ folder (the one with the subfolders containing the json files) into `BepInEx\config\Obeliskial\_importing`

## Installation (automatic)

1. Download and install [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager) or [r2modman](https://across-the-obelisk.thunderstore.io/package/ebkr/r2modman/).
2. Click **Install with Mod Manager** button on top of the page.
3. Run the game via the mod manager.

## Support

This has been updated for version 1.4.

Hope you enjoy it and if have any issues, ping me in Discord or make a post in the **modding #support-and-requests** channel of the [official Across the Obelisk Discord](https://discord.gg/across-the-obelisk-679706811108163701).

## Donation

Please do not donate to me. If you wish to support me, I would prefer it if you just gave me feedback. 