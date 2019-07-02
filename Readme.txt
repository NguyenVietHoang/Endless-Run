#############################

Project title: Unity Test
Author: NGUYEN Viet Hoang

#############################

GAME DESCRIPTION 

Endless runner game type: the player will try to jump over obstacle or crounch to dodge the bird and get point. The player will get 10 points each time he/she pass an obstacle. The game will end when the player hit the obstacle or the bird.

#############################

INSTALLATION

+ This game is better running on PC with the aspect ratio of 16:9/16:10. 
+ How to run the game:
	. In Unity: Go to "Main" scene -> press Play
	. Or use the build: Go to Build -> run "Unity Test" app 
		(The build is only available via file downloaded by Google Drive https://drive.google.com/file/d/1LOaYH6O77j9OMyneDCu09ucGS8F4pUAS/view?usp=sharing)

#############################

DESIGN

+ MVC pattern: 
	. All game mecanism was implement inside the CONTROLER (Managers's folder):		
		GameplayControler for Spawning obstacle and game sequence.
		PlayerControler for player physics collision and player Score.
		SoundControler for sound effect.
	. All UI Tab was controled by VIEW (View's folder): the view store all UI element, UI Update function (like Score update) and UI animation (like Tutorial Cinematic, Game over Cinematic). The View was only be called by Manager.
	. All other gameplay object (like Object pooling, ground check, obstacle) is MODEL (Models's folder): these models can be self activated (like the ground check) or be controled by manager (like obstacle spawning).
	. The Manager should be implemented with singleton pattern, but we only have 1 scene, so we don't need to implement that pattern.

+ The game won't use any physics base simulation for better performance. All the collision here are trigger collisions.
 
+ All the player's movement was integrate by animation to avoid coding and make the movement be scalable (like the jump: the player will jump very fast at early, stay in the air for a long delay before slowly fall off -> these things will be complicated when we try to use script).

+ The object pooling: All Obstacle and Bird in the game were implemented with Object pooling design: We don't need to Instantiate the object in need or destroy it when it is outside the screen, so the performance of the game will increase a lot.
	. Preload: Instatiate the pool.
	. Spawn: return the latest object inside the pool and remove it from the pool, then place this object at the start Point.
	. Despawn: Deactivate the object and push it into the pool. 
	. At the start of the game, I use Coroutine for instantiate all the objects of the pool without interupt the game animation. With the delegate "OnPoolingReady", we can know when the pool is ready to use. 

+ The stopping condition of obstacle was determined by comparing its position with the "end position". this will be faster than using collision to determine when will the obstacle was out the of camera.

#############################

HOW TO TEST THE FEATURE

+ Player's movement:
	. Change the player movement: Inside the animation Tab.
	. Test the key control: Deactive the box collider of the player (In the Main scene -> Player -> Player Sprite -> Deactive the box collider)

+ Object Spawing: Same as the player's movement: just deactive the box collider of the player, then you can see how the object was spawned without any interuption.

+ Getting Score: Atthe PlayerController.cs, you can comment the if(collision.tag == "Obstacle") state at OnTriggerEnter2D function, so the player can only collide with check point.

+ Colliding with Obstacle: At the PlayerController.cs, line 49, inside OnTriggerEnter2D -> if(collision.tag == "Obstacle") state, comment the line "GameOver();". Now each time you here the avatar scream, you know that he just collide with an obstacle.

#############################

CREDIT

All Assets, sound and font were downloaded in the internet

#############################
