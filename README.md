# This game, created in Unity, is about online war between players with the ability to communicate with players using chat

## About the project
This session-based shooter, developed in the Unity engine using the Photon Fusion network framework, is significantly technologically advanced compared to its predecessor, Photon Pun. It includes a registration and authorization system for entering the game, the ability to communicate with other players via chat, a weapon pickup mechanic, and a small but expandable inventory

## What has been implemented
- Implemented network logic, shooting mechanics, weapon interactions, chat, character controller, and system synchronization;
- Performance optimization and basic project structure;
- Network logic with prediction and interpolation for smooth multiplayer;
- Shooting and weapon interaction system;
- Built-in player-to-player chat;
- Character controller with network synchronization;
- Architecture separating client and server logic;
- Performance optimization and code structuring for project scalability

## How to play

### Objective
Destroy other players

### Controls
- **WASD** — move
- **Space** — jump
- **Shift** — run
- **RMB** — shoot
- **R** — reload
- **E** — pick up a weapon
- **Q** — throw down the weapon
- **T** — open chat
- **F1** — close chat
- **Enter** — send message
- **Esc** — open game menu

### Gameplay
1. Register in the game (login, password, repeat password, and email). All fields are validated and verified through Firebase. You can also remember yourself so you don't have to enter your details every time you log in.

2. After registering, the screen will turn yellow and you can log in. You'll see the main menu, where you can click "Start Game," "Settings," where you can adjust the volume of sounds and music, and the "Exit" button, which closes the game and "Logout" button to log out of your account.

3. After clicking "Start Game," you'll see a field where you can enter a session (room) name. If such a session doesn't exist, it will be created automatically and load the game scene.

4. You've entered the game and can see the scattered weapons. You can pick them up with "E," shoot with the left mouse button, and reload by pressing "R." The "Q" key releases weapons. You can also chat with other players by pressing the "T" key. This will open the chat window, allowing you to type a message and see both your own and other players' messages. To close the chat, press "F1." To open the game menu, press "Escape." Enjoy the game!

## Technologies
- Unity 6
- C#
- Photon Fusion
- Firebase

## Architecture
- MVC
- Event-driven
- ScriptableObjects
- Facade
- Strategy
- Observer
- OOP
- EntryPoint
- Proxy

## Screenshots

<img width="1280" height="720" alt="photo_2025-07-03_12-29-49" src="https://github.com/user-attachments/assets/8465fef4-1899-40cf-89f7-1e7047ca3578" />

Main menu


<img width="1280" height="720" alt="photo_2025-07-03_12-29-53" src="https://github.com/user-attachments/assets/aeba228c-86a5-44bb-bfd6-15485e35be9a" />

Session search menu


<img width="1280" height="512" alt="photo_2025-
07-03_12-29-56" src="https://github.com/user-attachments/assets/d3ad48f0-adbf-4d66-ab65-602344562718" />

Demonstration of the scene and interface of the health and stamina hood


<img width="1280" height="524" alt="photo_2025-07-03_12-30-04" src="https://github.com/user-attachments/assets/5d8140e8-e844-410e-adf5-261a485b85d2" />

Demonstration of a shot and explosion from a bazooka, and damage to the player


<img width="1280" height="526" alt="photo_2025-07-03_12-30-10" src="https://github.com/user-attachments/assets/7865ed2d-bc39-4bd8-8591-c0761edc7544" />

A frame of a flying projectile and correct processing of the physics of fast projectiles and synchronization


## Video demonstration

https://drive.google.com/drive/folders/1wO48FAtIYbDhtdgvaGRRWf-7AJDCAuCf?usp=sharing



