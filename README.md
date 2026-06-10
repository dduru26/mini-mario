# 2D Platformer Game (Unity Assignment)

Hi! This is my 2D platformer game that I built for my game development course. For
this assignment I was given a project that was already half made but had a bunch of
bugs in it on purpose. My job was to read through the code someone else wrote,
figure out what was broken, fix it, and then add my own features to make the game
actually playable. This was one of my first proper projects in Unity and C#, so I
learned a TON while doing it (and got stuck a lot, but I figured things out).

## What the game is

It's a side-scrolling 2D platformer, kind of like the old Mario games. You control a
little character that can run, jump, and shoot fireballs. There are enemies walking
around that hurt you if you touch them from the side, and there's water you can fall
into. You start with 3 lives, and if you lose all of them it's game over.

## Controls

- **Left / Right arrow keys** (or **A** and **D**) - move left and right
- **Spacebar** - jump
- **J** - shoot a fireball

## How to run it

1. Open the project in Unity (I used Unity 6).
2. In the Project window, go to `Assets/Scenes` and open the **StartMenu** scene.
3. Press the **Play** button at the top of the editor.
4. On the start screen, click the **Play** button to start the actual game.

(If you open `GameScene-ALU` directly it also works, but starting from StartMenu is
the proper way because that's how the menu is supposed to load first.)

## Bugs I had to fix

When I first opened the project nothing worked and the console was full of errors.
Here are the main bugs I tracked down and fixed:

- **The Input System error.** The project was set to use Unity's new Input System but
  all the scripts used the old `Input` class, so the game wouldn't even run. I changed
  the Active Input Handling in Player Settings to "Both" so the old code works.
- **The camera didn't follow the player.** The CameraFollow script had a `target`
  variable but nothing was ever put in it, so it crashed with a NullReference. I made
  it find the player by tag at the start.
- **The player wouldn't move.** A few things were wrong here. The Rigidbody2D line in
  Awake was written backwards so it never got assigned. The movement axis was set to
  `"0"` instead of `"Horizontal"`. And the Update method was completely empty so the
  jump and ground checks were never running.
- **The jump didn't work.** This one took me a while. The code checked if the player
  was standing on the ground using a layer, but the ground objects didn't have the
  right layer set, so the game always thought the player was in the air. Once I made a
  "Ground" layer and put the ground on it, the jump worked.

## Features I added

- **A GameManager.** I made this script from scratch to keep track of the player's
  lives in one place. Before, the life stuff was scattered around, so I moved it all
  into the GameManager so there's only one place that controls it.
- **Falling in water.** If you fall in the water you lose a life and respawn back on
  the last safe ground you were standing on (a little bit back from the edge so you
  don't just fall in again). This was the "extra mark" part of the assignment.
- **Enemies.** I added the enemy prefabs (snail, beetle, frog) to the level. If they
  touch you from the side you lose a life and get sent back like with the water. You
  can also jump on top of them or shoot them.
- **Shooting fireballs.** Press J to shoot. The fireball goes in whatever direction
  you're facing and destroys enemies when it hits them.
- **A start menu.** A separate scene with a background and a Play button that loads the
  game. There's also a Quit button.
- **A game over screen.** When you run out of lives, a "Game Over" panel pops up with a
  Replay button (restarts the game) and a Quit button.
- **A countdown timer placeholder** in the top-right corner of the screen.

## Things I learned

This project taught me a lot of stuff I didn't know before:

- **Tags vs layers are NOT the same thing.** This confused me so much. A tag is like a
  name for an object, and a layer is more about physics groups. Raycasts use layers,
  not tags. I kept thinking my ground tag would make the jump work but it needed a
  ground *layer*.
- **What a singleton is.** The GameManager uses `public static Instance` so other
  scripts can reach it from anywhere without me having to drag references around. I
  thought this was really cool once it clicked.
- **`Time.timeScale = 0` pauses the game.** That's how the game over screen freezes
  everything. I also learned you have to set it back to 1 before reloading or the new
  game starts frozen.
- **Triggers need a Rigidbody2D.** For `OnTriggerEnter2D` to even fire, one of the two
  objects has to have a Rigidbody2D. That's why the water and bullets work.
- **`private` vs `public` and `[SerializeField]`.** I learned you can keep a variable
  private but still set it in the Inspector by adding `[SerializeField]` above it.
- **You can't have two scripts with the same class name.** I got a duplicate error
  once because there were two copies of the same script, and that broke everything
  until I deleted one.

## Scripts overview

- `GameManager.cs` - controls lives, respawning, and the game over screen
- `PlayerMovement.cs` - moving and jumping
- `CameraFollow.cs` - makes the camera follow the player
- `PlayerDamage.cs` - handles the player getting hurt (now talks to the GameManager)
- `PlayerShoot.cs` / `FireBullet.cs` - shooting fireballs
- `SnailScript.cs` - the enemy behaviour (used by all the enemies)
- `ScoreManager.cs` - collecting coins
- `Water.cs` - tells the GameManager when the player falls in water
- `StartMenu.cs` - the start menu buttons

## Notes

This was a really good learning project even though it was frustrating at times.
Fixing someone else's broken code taught me how to actually read code and not just
write it from scratch. If I had more time I'd make the timer count down for real and
add a proper settings menu.

## Credits

Game art and starter project provided by the course. Scripts fixed and extended by me
as part of the assignment.