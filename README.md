# City Builder Project
(A project for personal experience)

## Current Work Flow
1. Improve UI
	1. *see* **TODO** [Already-Done+11.1]
	2. Add Global Inventory UI
	3. Add Color Indications (red for 0 amount, yellow for full storages)
2. Area Component (buffs & debuffs, requirements)
3. Workers System    
	1. Workers Data
		1. Amount
		2. Efficiency
		3. Quality
	2. Workers Container Component
	3. Workers Consumer Component
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
	1. Building shop & Place Mode (**TODO**: Add tooltip for prices, names (& descriptions?))
	2. Delete Mode
	3. Idle & Select Mode
---
