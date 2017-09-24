
Hello there!

How it works?
-------------

ShooterMain is the main controller class. It triggers all the events that other objects listen to.
PoolManager takes care of maintain a pool of gameobjects which include the bullets and the enemy ships.
EnemyController decides when to spawn an enemy. Frquency of spawn changes based on the level.
The game is currently set for two levels. Enemy and player ship change their color based on the level. 
There are two weapons for both the enemy and the player ship, one for each level. 
Functionality of the weapons change as the level increases.

This game has support for localisation too. But, only the IntroUI has it for now. ShooterMain has an exposed TextAsset 
variable that should be set to point to one of the two locale xml files below

LocaleData_English.xml
LocaleData_Spanish.xml

-ajjo.