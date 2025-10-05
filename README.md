# Solitaire

## Objective
You’ll design and prototype a mini-feature for a Solitaire-style mobile game. The goal is to assess
your ability to think critically, execute with speed, use AI tools efficiently, and structure code
clearly within a limited time frame.

## Task
Create a Unity prototype of the following feature: "Undo Move" System
Implement a basic undo system that lets the player revert their last move in a simplified Solitaire
Setting. You don’t need to recreate full Solitaire — just a minimal card movement setup (e.g.,
dragging cards between 2–3 stacks.

## Requirements:
- Show movement between stacks with drag-and-drop or click
- Implement undo functionality for at least 1 previous move
- Code should be clean and modular
- Include a short README describing:
  - What you built
  - What you’d improve with more time
  - Which parts were AI-assisted, and how you prompted/used it
 

## General Explanation:

For the test, I created a solitaire card movement between slots. I also added a check to determine if a movement is possible, based on an interface and a class. 
For the Undo movement, I use a command system to be able to add any movement or operation that happens in the game, being able to undo if required. 
I divide the important part of the game into Services, but for now, there are only two of them, GameplayService and UndoService.

### Scheduled steps by steps:
1. First of all, I check the assessment and create the project and all the necessary to begin to work: 0:30 aprox
2. I added the art and basic card movement into the game: 1:00 approx.
3. I began with the undo movement and behavior using the commands. 1:00 approx.
4. Improved the card placement and settled the placement logic. 0:45 approx.
5. Improved the animations when the card is moving. 1:00 approx
6. Play and some small bug fixes. 0:30 approx
7. Create the Readme file. 0:30 approx.

Total time: 5:15 approximately.

## How it works:
#### Cards:
The card's movement is handled by the UICardController. It handles that a card is being dragged and dropped using the interfaces IBeginDragHandler, IDragHandler, and IEndDragHandler
The cards had the knowledge of his children, allowing to have a card hierarchy.

#### Cards Slots:
In the same way that the cards, the card slots control when a card is being dropped on them, checking if it is able to be placed in the stack or not.
To implement the decision the drop I created an interface that can be settable in the prefab of the object. This allows us to create different behaviors easily.
To help myself assign this, I used the plugin [Serialize Reference](https://github.com/mackysoft/Unity-SerializeReferenceExtensions)

#### Undo:
The undo is a service in the game. Right now, it is a singleton, but I would prefer to have a service locator.
The undo registry records each action that happens in the game using "commands". Each command is something that can be undone in the game. The command implements and interfaces, and also controls its own undo behavior.
I created an UndoServiceConfig to control how many undo movements are allowed.

#### EventBus:
To make the communication between the Undo button and the system, I use an event bus behavior. It is a code copy from [signals aka event bus](https://github.com/yankooliveira/signals). But I renamed it  to Event Bus and made small changes to it.

#### Others:
- There are a few config files that handle the cards, that there is the game "DeckConfig".
- There is a "CardConfig" that has the config of the animations on the cards when moved and rotated.

## Future:
- I will create proper systems, moving the singletos to a Service Locator.
- I already have the placement logic, so making the top card slot is quite easy to do.
- Make the initial configuration randomly/procedurally to be able to play.
- I will do the flip behavior when a card on the top is being removed.
- Victory/loss condition.
- And some SFX.
- In general, I will finish the Solitaire game. I think in around 10/15 hours, I would be able to finish it.
- And of course, I will add some meta, like choose the card configuration that you would like to play. Add a change in the board itself.

## More Future:
- I will make a Victory animation
- I will add a hint system.
- I will need to know if a ways is possible to finish in or not before it begins. I will need a logic to be able to play the game and to create a configuration that is possible to win.

## AI Assisted
- In this project, I use the AI system to help me finish the drag-and-drop system. I just asked for help in my code, about what is missing to do a proper Drag and drop, and he replied to me with some adjustments.
- I also use a little bit for the animations in the cards, but I also check one [YouTube video](https://www.youtube.com/watch?v=I1dAZuWurw4). Here I ask him, but it was not very helpful, then I check the video.\
- I use the AI to help me correct this document.


