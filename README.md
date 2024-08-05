# Tips for Writing Cleaner Code that Scales in Unity

This repository contains the source code for a series of articles about writing cleaner and scalable code in Unity. The series is designed to help developers improve their coding practices by applying object-oriented design principles, SOLID principles, and design patterns. You can read the full series of articles [here](<LINK>).

![Article 3 10 NPC Wave result processed gif](https://github.com/PetterSunnyVR/Tips-to-writing-cleaner-code-in-Unity-/assets/17239042/caa3436f-727b-41b4-9215-610e012e787d)

## Series Overview

### Article 1: Breaking Down the Monolith to Add an AI-Driven NPC

In this article, we refactor the initial project to separate concerns and make the codebase more maintainable. We focus on:

- **Single Responsibility Principle (SRP)**: Breaking down the monolithic `PlayerMonolithic` script into smaller, focused components.
- **Dependency Inversion Principle (DIP)**: Introducing abstractions to decouple the `PlayerMonolithic` from specific input implementations.
- **Strategy Pattern**: Implementing different rotation strategies for the player and NPC.

### Article 2: Adding a Jump Mechanic without Spaghetti Code

This article demonstrates how to add a jumping mechanic to the game using the State Pattern, ensuring that the code remains clean and maintainable. Key topics covered:

- **State Pattern**: Separating different states (e.g., Movement, Jump, Fall) and their transitions.
- **Transitions**: Defining transition rules to manage state changes.
- **Interface Segregation Principle (ISP)**: Extending functionality without modifying existing code.

### Article 3: Enabling Flexible Interactions through Interfaces

In this article, we build a modular interaction system using interfaces, allowing for scalable and maintainable interactions in the game. Topics include:

- **Interfaces and Polymorphism**: Using interfaces to define common interaction behaviors.
- **Composition over Inheritance**: Combining interfaces to build complex behaviors.
- **Open-Closed Principle (OCP)**: Extending the interaction system without modifying existing code.
- **Interaction System**: Implementing interactable objects like levers and pickups.

## Getting Started

### Prerequisites

- Unity 2022 LTS or higher
- Basic understanding of C# and Unity

### Installation

1. Clone or downlaod this repository
2. Open the "Project" fodler in Unity
3. Search for "_Scritps" folder and you will find there a sepearate scene for Start and End of each Article

![image](https://github.com/PetterSunnyVR/Tips-to-writing-cleaner-code-in-Unity-/assets/17239042/007f0cab-2fdd-45d0-8331-d9d4523743b9)


### Scripts for Articles

It contains scripts that I reference in the Article series to make it easier for the readers to follow along.

## Assets Used:
- [3D Game Kit by Unity](https://assetstore.unity.com/packages/templates/tutorials/unity-learn-3d-game-kit-115747),
- [3D Game Kit Lite by Unity](https://assetstore.unity.com/packages/templates/tutorials/3d-game-kit-lite-135162),
- [Food Props by Unity by Unity](https://assetstore.unity.com/packages/3d/food-props-163295),
- [Starter Assets - ThirdPerson by Unity](https://assetstore.unity.com/packages/essentials/starter-assets-thirdperson-updates-in-new-charactercontroller-pa-196526)

## License

This project is licensed under the MIT License.
