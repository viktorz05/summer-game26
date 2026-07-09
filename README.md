# summer-game26
See branch `dev-test` for actual progress while we merge it into main...

# Current features
## Player system

- [x]  Walking
- [ ]  Running
- [x]  Jumping
- [ ]  Crouching
- [ ]  Sliding
- [x]  Throwing equipment

## Interaction system
- [x]  Grabbing items
- [ ]  Consuming items

## Inventory system

- [x]  Storing weapons
- [x]  Storing equipment (grenades, mines, etc)
- [ ]  Storing money/credits

## Upgrades system

- [ ]  Health upgrade
- [ ]  Speed upgrade

## Item system

Interfaces → Consumable

- [x]  Grenades

## UI components

- [ ]  HUD: We're using a pretty basic Unity `Canvas` with `TextMeshPRO` to display ammo and interactions
- [ ]  Menus

## Zombie AI

- [x]  Chasing the player
- [x]  Object pooling
- [x]  Spawning waves
- [ ]  Increasing # of zombies after waves
- [ ]  Increasing zombie health after # waves
- [ ]  Zombie dropping powerups or items

## Animation

- [ ]  Character movement animation
- [ ]  Player hand animation
- [ ]  Gun reload animations
- [ ]  Zombie death animation

## Weapon system

- [x]  Raycast shooter
    - [ ] Burst shooter
- [ ]  Projectile shooter
    - [ ] Pellet shooter
    - [ ] Explosive shooter/launcher 
- [x]  Reloading
- [x]  Ammo module
- [ ]  Aim module

## Character stats
Stats are implemented as a Dictionary with a `(Stat, float)` key-value pair, where `Stat` is an Enum
- [ ]  Speed
- [ ]  Health
- [ ]  Attack
# WIP Models
+ Pistol
+ Rifles (2)
+ Shotgun (wip)
+ Grenade
