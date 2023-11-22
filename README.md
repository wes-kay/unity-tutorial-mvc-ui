# unity-tutorial-mvc-ui
Goes over the DI and MVC patterns

## Patterns

**1)** 
Seperate all data from logic.
All GameObjects are different than data, you do not put raw data into a GO, it doesn't belong there. 

Unity teaches developers to use components as MonoBehaviors, it doesn't abstract the idea of moving around data and memory. 

A example of a bad practice in my opinion: 
```
public MonoBehavior Player{
    int health; 
    int manna; 
    string name; 
}

public MonoBehavior Enemy{
    int health; 
    int manna; 
    string name; 
}

**OR Inheratance (that's more messy and more code)**

public Entity{
    int health; 
    int manna; 
    string name; 
}

public MonoBehavior Player : Entity{

}

public MonoBehavior Enemy : Entity{

}


```

While it's not a bad concept you are designing your system around a core set of data, any in game enemy, player, NPC should have that value, and act on that data THEN pass it where it needs to go: 

```
public class Entity: ISaveable, ILoadable, IDamagable{
    int health;
    int mana; 
    string name;

    ...
}

public GameObject WorldPlayer{
    Entity player; 
    Transform worldPosition;
    ...
```

This is better because you're not locking your data to your worldplayer, you can pass the data anywhere that needs it, this is especially designed well for Networking, under the hood systems, or a ECS Style approach. 
`INetworkHandler.DamagePlayer(WorldPlayer.Entity)`
`INetworkHandler.DamagePlayer()`

**2)**
All systems **must control themselves** they never control outside systems, for instance: a character should never know about a save system, they should be seperate at all times and never have any depencency on one another. 

```
public struct Entity{
    int health;
    int mana; 
    string name;
}

public GameObject WorldPlayer{
    Entity player; 
    Transform worldPosition;

    [Inject]
    IDataSystem dataSystem;

    public WorldPlayer(){
        //Get any saved data of the player
        player = dataSystem.LoadData<Player>();
    }    
}

```

Data system is not a dependenty of the player, meaning if we ever change lets say Json to SQL, there's no breaking of the project. 

**3)**

We don't use SO's all data is either Json or pulled from a DB or a custom package like Easy save / Inventory systems that use a Key value type system on binary files. 



## Dependency Injection
At the core of the project almost all references between components DI should be used, this allows us a easy way to extend logic, majority of the installers are created that cover almost all core systems, but add as neeeded: 
- Create a `Scripts/Installers/MyInstaller.cs`
    - Have it pull a Monobehavior from the scene
    - Or have it create a new instance of your native class
- Create a `Scripts/System/MySystem.cs`
    - Implement all exposed functionality in a Interface
    - Create the class implementing that interface

Then add the installer to the `SceneContext` inside the scene and hook up the system

## Model View Controller (MVC)
UI systems should follow the MVC format. 

- Model, controls all data specifically TO the ui, example (A slider value of a volume NOT the actual volume, when you OnDirty the ui / serialize it that data is then passed to the controller)
- View, All gameobject specific handlers, references to the go and elements
- Controller, the setup between the ouside data and the model (The volume slider will be passed the actual volume on audio settings), enabling and disabling of the ui is kept here

## States
The two active states to the game is Game state (loading, gameplay, anything to do with game runtime) and UI state (TitleScreen, menu, anything to do with a UI overlay), the game state should control the ui states. 
Meaning you can open a settings window in both title screen and gameplay. 

## Structure
- `/Assets` Anything media related should be placed here (Textures, Materials, Audo, Mesh)
- `/Scripts/Components` Anything in the scene outside of the systems should be placed here
- `/Scripts/Tools` Anything related to Devops, plugins, outside namespaces should be placed here
- `/Scripts/MVC` All Model View Controller UI should be placed here.
- `/Data` All data outside of streamables or references should be placed here (json, SQL, etc)

## Zenject setup
- Download and install the Zenject package: https://assetstore.unity.com/packages/tools/utilities/extenject-dependency-injection-ioc-157735
- Learn (how to make a new installer, how the construction of the objects under the hood works)
- Create a new Scene context in the inspector 
- ![Screenshot 2023-11-21 165711](https://github.com/wes-kay/unity-tutorial-mvc-ui/assets/40011470/88192916-8676-4897-911a-e4262cae0981)
- Add the mono installer `_Installer` to the Context
- ![image](https://github.com/wes-kay/unity-tutorial-mvc-ui/assets/40011470/6d627c83-28b3-4c95-b247-4dcd6f2a06e3)
- The installer is registed
