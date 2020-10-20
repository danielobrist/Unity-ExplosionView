# Unity-ExplosionView
 
A simple implementation of an ExplodeView for 3D-Objects in Unity. Currently based on a central explosion point, pushing the objects outwards. Toggle the explosion with "space".

# Functionality

The central part is the [Explode.cs](https://github.com/danielobrist/Unity-ExplosionView/blob/main/Assets/Scripts/Explode.cs) script. It has to be placed on a parent object, which holds all the "explodable" objects as children. The script offers the following inspector properties:

* **Explosion Center**: this is the central point from where the child objects will move out- and inwards. It is an invisible GameObject, placed ideally inside the explodable object.
* **Explosion Tag**: all child objects have to be tagged with the tag name which is defined here as a string. Untagged or diffenrently tagged children will not move!
* **Explosion Factor**: this is the factor which defines how much further away the chilren will move from the Explosion Center. E.g. with an Explosion Factor of 2, their exploded distance from the center will be 2x their unexploded distance from the center.
* **Explosion Speed**: this is the speed by which the children will move away/towards the Explosion Center
