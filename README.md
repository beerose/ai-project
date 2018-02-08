# ai-project

## AI mechanics:
### Dungeon:
- Dungeon is generated procedural using the following alghoritm:
    1. With normal distribution there is generated size for the first room. Standard derivation = 1.
    2. The first room is placed in random postion withing the radius given as the parameter of the alghoritm.
    3. The room is added to the graph.
    4. While number of placed room is not equal rooms to ultimately number of rooms:
      1. Picking one room from the graph.
      2. Picking a wall of that room.
      3. With normal distribution there is generated size for another room.
      4. If the rooms doesn't collide with any other rooms, it is added to the graph.
    5. Doors are placed in appropriate places.
- Examples of generated rooms for room count = 50:
  1. seed = 0
  
![](https://d2mxuefqeaa7sj.cloudfront.net/s_459E61CCFE50A6803AD356B0B77ACBA20AD365029E7BED22FECF072DC8698632_1518088925283_Screen+Shot+2018-02-08+at+12.19.17.png)

  1. seed = 100
![](https://d2mxuefqeaa7sj.cloudfront.net/s_459E61CCFE50A6803AD356B0B77ACBA20AD365029E7BED22FECF072DC8698632_1518088964753_Screen+Shot+2018-02-08+at+12.19.39.png)

  2. seed = 1028371834
![](https://d2mxuefqeaa7sj.cloudfront.net/s_459E61CCFE50A6803AD356B0B77ACBA20AD365029E7BED22FECF072DC8698632_1518088970288_Screen+Shot+2018-02-08+at+12.19.55.png)


### Objects generator:
  The generator is dependent on given seed and it uses ewolutional alghoritm. Program allows to choose:
    - amount of population
    - amount of selections
    - statistics interval
    - bullets and spells to be generated
  Alghoritm: 
    1. Creating random population 
    2. Choosing two characters
    3. Crossover these two characters
    4. Mutatiom
    5. Adding character to the population
    7. Selection

  
## Game design

### Navigation:

  - W, S, A, D - move
  - Arrows - attack
  - Spacebar - using spell
  - H - using HP potion
  - E, C - checking equipment
  - I, B - checking bag
  - ESC - game options

### Types of objects:

  - Weapon
![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518038598306_Screenshot_1.png)

  - Armor
![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518038623085_armor.png)

  - Spell
![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518038633966_spells.png)

  - HP potion - gives 25% more HP
![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518038707716_potions.png)


Types of traps that aims injuries:

![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518038768059_tr1.png)

![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518038773079_tr2.png)

![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518038777816_tr3.png)

![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518038787335_tr4.png)


Additional informations:

- Player model:
![](https://d2mxuefqeaa7sj.cloudfront.net/s_3F7778D0F8D13538FB6DBCA56EE93C996F6617ED4E28264A3C35158245CD98E7_1518039270939_Screenshot_2.png)

- All used models, sounds and textures come from Asset Store

