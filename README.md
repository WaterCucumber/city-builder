# City Builder Project
(A project for personal experience)


## Setting
- You are a machine that need to build a *factory* and *World Regulator* building on a dangerous planet.
---


## Design
- Difficulty is "*Asteroid rain*" that appears on the planet every *2-3 minute*.
If asteroids destroy "*Machine Center*", you lose.
- Resources set: Steel, Biomass, Golden Ore, Golden Ingot, *Radioactive Cell*
- Build "*Protective Umbrella*" and place *Radioactive Cell* in to activate it
- "*Protective Umbrella*" also provides energy for your buildings, but the more buildings plugged in the less Umbrella's protection.
- *Radioactive Cell* can be set into buildings to improve their efficiency
---


## Current Work Flow
1. Create Health Component and Max Health to Data
2. Damage System
	1. Buildings Take Damage 
	2. Some Buildings Can't take damage
	3. Some Protect (flat value) from damage
4. Protective Umbrella
	1. Damage Protection
	1. Visual
2. Asteroids
	1. Asteroids Timer
	2. Asteroids Visual
	3. Asteroids UI (timer)
3. Radioactive Cell Place Component
1. Improve UI
	1. Add Global Inventory UI
	2. Add Color Indications (red for 0 amount, yellow for full storages)
2. Area Component (buffs & debuffs, requirements)
4. etc. ( WIP )
---


## Already Done
1. Build Finite State Machine
	1. Ghost Placement State
	2. Delete Buildings State
	3. Selection State
	4. Idle State
2. Building Data & Building Instance
3. Building Grid Data & Grid Visualiser
4. Building Allowed Tiles Placement (**TODO**: Replace integers to something better)
5. Buildings Selection
6. Buildings Dynamic UI
	1. Using ScrollContainer Node
	2. Changes size based on contents
	3. ScrollContainer activates VScroll only if UI won't be full on screen
7. Item Management
	1. Item Resources
	2. Item Data Base
	3. Inventory System
	4. Player Singletone Inventory
	5. Buildings Cost
	6. Building Expanses
	7. Inventory Dynamic Limit (calculates with given function)
8. Test Building Behaviour
	1. Global Storage Component
	2. Inventory Component
	3. Default Comonent
	4. Item Generator Component (*For test only*)
9. Components UI & UI Auto Update
	1. Global Storage Component UI (**TODO**: Merge UI Script with Inventory Component UI because of identical logic)
	2. Inventory Component UI
	3. Default Building Component UI (for name display)
10. Data Base for items and UI resource references
11. Build UI
	1. Building shop & Place Mode (**TODO**: Add tooltip for prices)
	2. Delete Mode
	3. Idle & Select Mode
---
