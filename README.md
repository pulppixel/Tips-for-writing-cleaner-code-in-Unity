# Tips for Writing Cleaner Code that Scales in Unity

This repository contains the source code for a series of articles about writing cleaner and scalable code in Unity. The series is designed to help developers improve their coding practices by applying object-oriented design principles, SOLID principles, and design patterns. You can read the full series of articles [here](<LINK>).

![Article 3 10 NPC Wave result processed gif](https://github.com/PetterSunnyVR/Tips-to-writing-cleaner-code-in-Unity-/assets/17239042/caa3436f-727b-41b4-9215-610e012e787d)


## Getting Started


### Prerequisites

- Unity 2022 LTS or higher
- Basic understanding of C# and Unity

### Installation

This repository contains a '**Project**' folder, which can be opened in Unity Hub, and a '**Scripts for Articles**' folder, where you can find the scripts as they evolve with each article. This setup makes it easy to see how each script changes after refactoring or extending its functionality.

1. Clone or download this repository
2. Open the "Project" folder in Unity Hub
3. Search for "_Scritps" folder and inside it you will find folders called "Article 1" (up to "Article 5")
4. Each folder contains "Start" and "Result" folder where you will find a unity scene that you can open and all the scripts used in it.

![image](https://github.com/PetterSunnyVR/Tips-to-writing-cleaner-code-in-Unity-/assets/17239042/007f0cab-2fdd-45d0-8331-d9d4523743b9)


### Scripts for Articles

It contains scripts that I reference in the Article series to make it easier for the readers to follow along. Scripts for each article are places in separate namespaces ex "**Tips.Part_1_Result**" or "**Tips.Part_1_Start**" so that you can follow along with our article series or preview the resuls.

## Series Overview


### Article 1: Breaking Down the Monolith to Add an AI-Driven NPC

In this article, we refactor the initial project to separate concerns and make the codebase more maintainable. We focus on:

- **Single Responsibility Principle (SRP)**: Breaking down the monolithic `PlayerMonolithic` script into smaller, focused components.
- **Dependency Inversion Principle (DIP)**: Introducing abstractions to decouple the `PlayerMonolithic` from specific input implementations.
- **Strategy Pattern**: Implementing different rotation strategies for the player and NPC.

___

### Article 2: Adding a Jump Mechanic without Spaghetti Code

This article demonstrates how to add a jumping mechanic to the game using the State Pattern, ensuring that the code remains clean and maintainable. Key topics covered:

- **State Pattern**: Separating different states (e.g., Movement, Jump, Fall) and their transitions.
- **Transitions**: Defining transition rules to manage state changes.
- **Interface Segregation Principle (ISP)**: Extending functionality without modifying existing code.

___

### Article 3: Enabling Flexible Interactions through Interfaces

In this article, we build a modular interaction system using interfaces, allowing for scalable and maintainable interactions in the game. Topics include:

- **Interfaces and Polymorphism**: Using interfaces to define common interaction behaviors.
- **Composition over Inheritance**: Combining interfaces to build complex behaviors.
- **Open-Closed Principle (OCP)**: Extending the interaction system without modifying existing code.
- **Interaction System**: Implementing interactable objects like levers and pickups.

___

### Article 4: Implementing a Damage System Using Interfaces
In this article, we extend our project by introducing a flexible and reusable damage system through the use of interfaces. This approach enhances the scalability of the game while maintaining a clean and organized codebase. Key topics covered:

- **Interfaces for Flexibility**: Using interfaces to create a damage system that can be applied to different entities (e.g., players, NPCs) without modifying existing code.
- **Separation of Concerns**: Keeping the damage logic separate from other gameplay mechanics, allowing for easier updates and maintenance.
- **Dependency Injection**: Injecting dependencies to make the damage system more modular and testable.

___

### Article 5: Refactoring the Agent System for Extensibility
In the final article of the series, we focus on further refactoring the agent system to enhance the project's extensibility and maintainability. The improvements allow for easier integration of new features and better management of the codebase. Key topics covered:

- **State Factory Refactoring**: Extracting the state creation logic into a factory class, reducing the complexity of agent classes.
- **Modular Design**: Breaking down the AgentMonolithic class into smaller, manageable pieces, with none exceeding 100 lines of code.
- **Code Metrics and Maintainability**: Evaluating the refactored code using metrics, emphasizing maintainability and ease of understanding.
- **Interface Implementation**: Leveraging interfaces to create a reusable damage system, further decoupling the code and improving extensibility


## Assets Used:
- [3D Game Kit by Unity](https://assetstore.unity.com/packages/templates/tutorials/unity-learn-3d-game-kit-115747),
- [3D Game Kit Lite by Unity](https://assetstore.unity.com/packages/templates/tutorials/3d-game-kit-lite-135162),
- [Food Props by Unity by Unity](https://assetstore.unity.com/packages/3d/food-props-163295),
- [Starter Assets - ThirdPerson by Unity](https://assetstore.unity.com/packages/essentials/starter-assets-thirdperson-updates-in-new-charactercontroller-pa-196526)
- [Animations created using Unity Muse AI](https://unity.com/products/muse)

## License

This project is licensed under the MIT License.
