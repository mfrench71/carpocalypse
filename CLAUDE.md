# Carpocalypse - Claude Code Guidelines

## Project Documentation

**Always refer to and update `DEVELOPMENT.md`** for:
- Feature roadmap and task tracking
- Known issues and technical debt
- Architecture patterns to follow
- Asset requirements

## Code Standards

### Namespace
All scripts must use the `Carpocalypse` namespace:
```csharp
namespace Carpocalypse
{
    public class MyScript : MonoBehaviour
    {
        // ...
    }
}
```

### Architecture Patterns
- Use interfaces for extensibility (IDamageable, IWeapon, etc.)
- Event-driven communication between systems
- ScriptableObjects for data-driven content
- Object pooling for frequently spawned objects

### Conventions
- Add `[Header("Section Name")]` attributes for inspector organization
- Include null checks with `Debug.LogError` for required components
- Use properties over public fields where appropriate
- Physics code in `FixedUpdate()`, input in `Update()`

## Key Systems

- **WeaponSystem**: Handles all shooting, uses runtime ammo tracking
- **Health**: Generic damage system, score added on enemy death only
- **ObjectPool**: Singleton for bullet pooling
- **GameManager**: Game state, score, restart logic

## Before Committing

1. Ensure all scripts compile
2. Update DEVELOPMENT.md if completing tasks
3. Test in Unity editor if possible
4. Use descriptive commit messages
