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
- **LMB** — shoot
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
- DOTween

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

<img width="962" height="713" alt="image23" src="https://github.com/user-attachments/assets/06c2be75-be43-4dd8-96eb-a94295b12609" />

Registration menu


<img width="1280" height="720" alt="photo_2025-07-03_12-18-43" src="https://github.com/user-attachments/assets/b1878f49-ccf4-4191-ae99-d2a3a117bfe5" />

Authorization menu


<img width="1280" height="720" alt="photo_2025-07-03_12-19-14" src="https://github.com/user-attachments/assets/f08f2536-2f9c-4416-ba55-6c06f416f427" />

Main menu


<img width="1280" height="720" alt="photo_2025-07-03_12-19-18" src="https://github.com/user-attachments/assets/e7adad1e-1ba9-4937-9d8d-1185bf52b6d2" />

Menu for creating and connecting to sessions


<img width="1280" height="468" alt="photo_2025-07-03_12-19-25" src="https://github.com/user-attachments/assets/97e3612b-ee49-4162-baab-361e028b8dab" />

Demonstration of the game scene, HUD and chat between players


## Video demonstration

https://drive.google.com/drive/folders/1VeZaT56SrITuUIBeXXgfGLgfq2CN4Zpz?usp=sharing





