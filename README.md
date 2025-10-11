# ⚔️ LostSword – Top-Down Dungeon Adventure

## 🎮 Overview
**LostSword** is a **Top-Down Adventure** game where the player finds themselves trapped inside a mysterious **dungeon** with three challenging levels.  
The goal is to progress through each stage, collect valuable items, manage resources wisely, and fight enemies and traps — until reaching the final boss battle to **retrieve the stolen sword**.

Throughout the journey, the player must carefully manage their **arrows (Ammo)** and **coins**, as shooting is only possible when arrows are collected.  
Each level tests the player’s strategy, timing, and decision-making to survive and reach the exit door.

---

## ⚙️ Core Systems

### 🧩 Inventory System
- Manages all the items the player collects:  
  arrows, coins, health potions, quest items, and other collectibles.
- Opens and closes by pressing the **I** key using Unity’s **Input System**.
- Displays all collected items through an **interactive UI panel**.
- The inventory data is temporary and resets each new playthrough.
- Implemented as a **Singleton**, ensuring a single consistent instance that manages the player’s data and logic across all levels.

---

### 📜 Mission System
- Each level presents a **unique mission**, such as:
  - Collecting specific items
  - Defeating a certain number of enemies
  - Reaching a designated location
- The system **interacts directly with the Inventory**, especially when missions involve item collection.
- Managed by a Singleton called `QuestManager`, which centralizes mission tracking and prevents multiple conflicting instances.

---

### 🧠 Boss System
- The final boss is implemented using a **State Machine** structure.  
- This approach allows clean control of the boss’s behavior, animations, and transitions between states.  
- It provides flexibility, scalability, and easy maintenance for future improvements.

---

### 🔫 Combat & Ammo System
- The player can only shoot if arrows (Ammo) are available in the Inventory.
- Each shot decreases the arrow count automatically and updates the UI in real time.
- When arrows are picked up, they are added instantly to the Inventory and displayed correctly.
- Uses a **GameEvents** architecture to synchronize gameplay actions like shooting, picking up, and updating ammo counts.

---

### 🏰 Levels & Progression
- The game contains **three levels (Level 1–3)**, each with its own enemies, traps, and time challenges.
- The goal of each stage is to reach the exit door while maintaining the player’s health.
- After clearing all three levels, the player faces the **final boss battle** to reclaim the lost sword.

---

## 🧱 Architecture
- The project follows the **Responsibility Separation** principle — every system (Inventory, Missions, PlayerHealth, EnemyAI, etc.) has its own responsibility and communicates via events.
- Uses **ScriptableObjects** for defining item data (`ItemSO`, `InventorySO`) and missions, allowing modular and scalable design.
- The `Inventory` and `QuestManager` are implemented as **Singletons**, ensuring centralized control and avoiding data duplication across scenes.

---

## 🧩 Technologies Used
- **Unity 2D (URP)**
- **C# Scripting**
- **Input System**
- **ScriptableObjects**
- **State Machine Architecture**
- **Canvas UI / UI Toolkit**
- **Prefab System**

---

## 👾 Key Features
- Interactive **Inventory UI** toggled with the **I** key  
- Dynamic and modular **Mission System**  
- Full **resource management** for arrows, coins, and potions  
- **State Machine**-based boss behavior  
- **Event-Driven Architecture** between systems  
- Immersive **Top-Down Dungeon** design  

---

## 🧑‍💻 Developers
**Eli Alhazov**  
**Michael Miron**  

🎓 *Final Project – Sapir College*  
🕹️ *Year: 2025*
