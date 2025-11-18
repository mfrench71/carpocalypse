# Carpocalypse - Development Plan

## Project Overview
A Unity car combat game with vehicle physics, weapons, and enemy AI.

**Current Progress: ~35%**

---

## Phase 1: Core Foundation (COMPLETE)

- [x] Basic vehicle movement (WASD/Arrow keys)
- [x] Bullet system with object pooling
- [x] Health system with events
- [x] Score tracking
- [x] Basic UI (health bar, score, game over)
- [x] Camera follow system
- [x] Basic enemy AI (chase + contact damage)
- [x] Game manager singleton
- [x] Enemy and bullet prefabs
- [x] Namespace organization (Carpocalypse)
- [x] Interface-based architecture (IDamageable, IPoolable, IPickup)
- [x] Event-driven systems (GameEvents hub)
- [x] Data-driven design (ScriptableObjects for weapons, vehicles, enemies)

---

## Phase 2: Combat System Refinement (IN PROGRESS)

### Weapons
- [x] Connect WeaponSystem to VehicleController
- [x] Remove duplicate shooting code from VehicleController
- [x] Create WeaponData assets:
  - [x] Machine Gun (fast fire, low damage)
  - [x] Cannon (slow fire, high damage)
  - [x] Shotgun (spread shot)
- [ ] Create weapon pickup prefabs
- [ ] Add ammo UI display
- [ ] Weapon switching UI indicator

### Pedestrians (COMPLETE)
- [x] PedestrianData ScriptableObject
- [x] PedestrianAI with behavior states (Wander, Flee, Aggressive, Neutral)
- [x] PedestrianSpawner system
- [x] Armed/unarmed variants
- [x] Default types: Civilian (flees), Bandit (armed), Survivor (wanders)
- [x] Editor tool: Carpocalypse > Create Default Pedestrians

### Enemy Improvements
- [ ] Enemy spawner system (wave-based)
- [ ] Multiple enemy types:
  - [ ] Chaser (current behavior)
  - [ ] Shooter (ranged attacks)
  - [ ] Tank (high HP, slow)
- [ ] Enemy death effects
- [ ] Health bars above enemies

---

## Phase 3: Arena & Environment

### Procedural Terrain Generation
- [ ] Terrain generator system
- [ ] Perlin noise heightmap generation
- [ ] Procedural obstacle placement
- [ ] Road/path generation
- [ ] Spawn point validation (ensure drivable areas)
- [ ] Seed-based generation (reproducible layouts)
- [ ] Runtime terrain modification (destruction)

### Biomes
- [ ] Biome system architecture
- [ ] Desert biome
  - [ ] Sand terrain texture
  - [ ] Dunes and flat expanses
  - [ ] Rock formations, cacti props
  - [ ] Heat haze effect
- [ ] Urban biome
  - [ ] Asphalt/concrete textures
  - [ ] Building ruins, barriers
  - [ ] Street props (cars, signs)
  - [ ] Dust/debris particles
- [ ] Industrial biome
  - [ ] Metal/rust textures
  - [ ] Warehouses, containers
  - [ ] Pipes, machinery props
  - [ ] Smoke/steam effects
- [ ] Wasteland biome
  - [ ] Cracked earth texture
  - [ ] Sparse vegetation
  - [ ] Wreckage, debris
  - [ ] Toxic pools/hazards
- [ ] Snow biome
  - [ ] Snow terrain texture
  - [ ] Ice patches (slippery physics)
  - [ ] Frozen props
  - [ ] Snowfall particles
- [ ] Biome transitions/blending
- [ ] Biome-specific enemy spawns
- [ ] Biome-specific pickups/hazards

### Arena Setup
- [ ] Arena boundaries (walls/colliders)
- [ ] Procedural cover placement
- [ ] Destructible objects
- [ ] Jump ramps and terrain features

### Visual Environment
- [ ] Terrain texturing (based on height/slope)
- [ ] Arena walls/fences
- [ ] Environmental props (procedurally placed)
- [ ] Lighting improvements
- [ ] Fog/atmosphere

---

## Phase 4: Audio & Visual Effects

### Audio
- [ ] AudioManager singleton
- [ ] Shooting sounds (per weapon)
- [ ] Impact/hit sounds
- [ ] Explosion sounds
- [ ] Engine sounds
- [ ] UI sounds (button clicks, game over)
- [ ] Background music

### Visual Effects
- [ ] Muzzle flash particles
- [ ] Bullet impact particles
- [ ] Explosion effects
- [ ] Damage indicators (screen flash)
- [ ] Screen shake on hit
- [ ] Vehicle destruction effect

---

## Phase 5: Game Systems

### Progression
- [ ] Wave system with increasing difficulty
- [ ] Score multipliers
- [ ] High score saving (PlayerPrefs)
- [ ] Unlockable weapons
- [ ] Currency/scrap collection

### Vehicle Upgrades
- [ ] Upgrade system architecture
- [ ] Upgrade UI/shop menu
- [ ] Engine upgrades
  - [ ] Top speed increase
  - [ ] Acceleration boost
  - [ ] Handling improvement
- [ ] Armor upgrades
  - [ ] Max health increase
  - [ ] Damage resistance
  - [ ] Repair speed
- [ ] Weapon upgrades
  - [ ] Damage boost
  - [ ] Fire rate increase
  - [ ] Ammo capacity
  - [ ] Reload speed
- [ ] Special upgrades
  - [ ] Nitro/boost system
  - [ ] Ram damage
  - [ ] Turret rotation speed
- [ ] Upgrade persistence (save/load)
- [ ] Upgrade visual changes on vehicle

### Pickups & Powerups
- [ ] Health pickup
- [ ] Ammo pickup
- [ ] Speed boost
- [ ] Shield/invincibility
- [ ] Damage boost
- [ ] Scrap/currency pickup

### Player Spawning
- [ ] Implement player spawn system in GameManager
- [ ] Respawn on death (lives system) OR
- [ ] Single life with restart

---

## Phase 6: UI & Menus

### Menus
- [ ] Main menu scene
- [ ] Pause menu (ESC key)
- [ ] Settings menu (audio, controls)
- [ ] Game over screen with stats

### In-Game UI
- [ ] Ammo counter
- [ ] Current weapon display
- [ ] Wave/level indicator
- [ ] Minimap
- [ ] Damage direction indicator
- [ ] Kill feed/notifications

---

## Phase 7: Polish & Optimization

### Visual Polish
- [ ] Post-processing (bloom, vignette)
- [ ] Better materials/shaders
- [ ] Vehicle models (replace cubes)
- [ ] Enemy models
- [ ] Animations

### Code Cleanup
- [ ] Add namespaces to all scripts
- [ ] Refactor UIManager to use events
- [ ] Fix WeaponData ammo persistence issue
- [ ] Implement new Input System
- [ ] Add comprehensive null checks

### Performance
- [ ] Object pooling for enemies
- [ ] Object pooling for effects
- [ ] LOD for models
- [ ] Occlusion culling

---

## Phase 8: Content & Expansion

### Additional Content
- [ ] Multiple arenas/levels
- [ ] Boss enemies
- [ ] Achievement system

### Vehicle Types
- [ ] Vehicle selection system
- [ ] Vehicle data ScriptableObjects
- [ ] Scout
  - [ ] High speed, low armor
  - [ ] Quick acceleration
  - [ ] Small hitbox
- [ ] Brawler
  - [ ] Balanced stats
  - [ ] Medium speed/armor
  - [ ] Standard loadout
- [ ] Tank
  - [ ] High armor, low speed
  - [ ] Slow acceleration
  - [ ] Heavy weapons
- [ ] Interceptor
  - [ ] High speed, medium armor
  - [ ] Boost ability
  - [ ] Light weapons
- [ ] Truck
  - [ ] Very high armor
  - [ ] Very slow
  - [ ] Ram damage bonus
- [ ] Buggy
  - [ ] Highest speed
  - [ ] Lowest armor
  - [ ] Jump ability
- [ ] Vehicle unlock progression
- [ ] Vehicle preview in selection
- [ ] Unique vehicle abilities

### Game Modes
- [ ] Survival mode (current)
- [ ] Time attack
- [ ] Arena challenges

---

## Phase 9: Scalability & Multiplayer

### Multiplayer Foundation
- [ ] Netcode for GameObjects OR Photon integration
- [ ] Network manager system
- [ ] Player synchronization
- [ ] Networked physics (client prediction)
- [ ] Lobby system
- [ ] Matchmaking

### Multiplayer Modes
- [ ] Co-op survival (2-4 players)
- [ ] Deathmatch (FFA)
- [ ] Team Deathmatch
- [ ] Capture the Flag
- [ ] King of the Hill
- [ ] Battle Royale (shrinking arena)

### Backend Services
- [ ] Player accounts/authentication
- [ ] Cloud save (progress, unlocks)
- [ ] Leaderboards (global/friends)
- [ ] Daily/weekly challenges
- [ ] Season pass system
- [ ] Analytics integration

### Monetization (Optional)
- [ ] Cosmetic skins (vehicles, weapons)
- [ ] Battle pass tiers
- [ ] In-app purchases
- [ ] Ad integration (rewarded ads)

### Scalable Architecture
- [ ] Addressables for asset loading
- [ ] Modular scene loading
- [ ] Content bundles (downloadable)
- [ ] Server-authoritative game logic
- [ ] Anti-cheat measures

---

## Phase 10: Live Service Features

### Content Pipeline
- [ ] Level editor / arena builder
- [ ] User-generated content sharing
- [ ] Seasonal events
- [ ] Limited-time modes
- [ ] Weekly rotating playlists

### Community Features
- [ ] Clans/guilds
- [ ] Friends list
- [ ] Chat system
- [ ] Replay system
- [ ] Spectator mode
- [ ] Share clips/highlights

### Competitive Features
- [ ] Ranked matchmaking
- [ ] Skill-based rankings (ELO/MMR)
- [ ] Seasons with rewards
- [ ] Tournaments
- [ ] Esports integration

---

## Known Issues

1. ~~`CameralFollow.cs` typo in filename~~ FIXED
2. ~~Duplicate shooting code in VehicleController and WeaponSystem~~ FIXED
3. ~~WeaponData ScriptableObject stores runtime ammo (persists in editor)~~ FIXED
4. ~~UIManager updates every frame (should use events)~~ FIXED
5. No arena boundaries (can drive off edge)
6. InputSystem asset exists but unused (using legacy Input)

---

## Technical Debt

- [x] Consolidate shooting logic into WeaponSystem only
- [x] Create runtime weapon instance to track ammo
- [x] Switch UIManager to event-driven updates
- [ ] Migrate to new Input System
- [x] Add error handling and null checks

---

## Scalable Architecture Patterns (Priority)

### Interface-Based Systems
- [x] IDamageable interface (for health/damage)
- [ ] IWeapon interface (for all weapon types)
- [ ] IVehicle interface (for vehicle stats/abilities)
- [x] IPickup interface (for all pickups)
- [x] IPoolable interface (for object pooling)
- [ ] ISpawnable interface (for spawn system)

### Event-Driven Architecture
- [x] GameEvents static class (central event hub)
- [ ] Event channels (ScriptableObject-based events)
- [x] Decouple UI from game logic via events
- [ ] Decouple audio from game logic via events
- [ ] Achievement system via event listeners

### Data-Driven Design
- [x] VehicleData ScriptableObject
- [x] EnemyData ScriptableObject
- [x] WeaponData ScriptableObject
- [x] PedestrianData ScriptableObject
- [ ] BiomeData ScriptableObject
- [x] WaveData ScriptableObject
- [ ] UpgradeData ScriptableObject
- [ ] External balance config (JSON import)
- [ ] Remote config support (for live tuning)

### Service Locator / Dependency Injection
- [ ] ServiceLocator pattern for managers
- [ ] Remove singleton dependencies where possible
- [ ] Testable architecture (mock services)

### Performance Patterns
- [ ] Generic object pool system
- [ ] Spatial partitioning for enemy queries
- [ ] Job System for heavy calculations
- [ ] Burst compiler for performance-critical code
- [ ] Consider ECS for large entity counts (100+ enemies)

### Modular Systems
- [ ] Separate assemblies for core/gameplay/UI
- [ ] Feature toggles (enable/disable systems)
- [ ] Plugin architecture for new content
- [ ] Hot-reload support for balance data

---

## Asset Needs

### Models
- Player vehicle model
- Enemy vehicle models (2-3 types)
- Weapon pickup models
- Powerup models
- Arena props

### Audio
- Weapon sounds (3+ weapons)
- Impact sounds
- Explosion sounds
- Engine loop
- UI sounds
- Music tracks

### Textures/Materials
- Ground textures
- Vehicle materials
- Arena materials

### Particles
- Muzzle flash
- Bullet trails
- Explosions
- Pickup effects

---

## Development Notes

**Last Updated:** 2025-11-18

### Recent Changes
- Fixed CameraFollow.cs filename typo
- Completed initial project analysis

### Next Session Priority
1. Connect WeaponSystem to player
2. Create weapon data assets
3. Add enemy spawner
4. Add arena boundaries

---

## Resources

- Unity Version: [Check ProjectSettings]
- Render Pipeline: URP
- Target Platform: PC (with mobile settings available)
