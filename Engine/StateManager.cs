using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_Sims.Engine;

public class StateManager : ILoopObject
{
	public static string GO_TO_PREVIOUS_SCREEN = "GO TO PREVIOUS SCREEN";
	public static string TITLE_SCREEN = "Title Screen";
	public static string TEST = "Test";


	private readonly Stack<string> previousGameStates = new Stack<string>();
	private string currentStateName = string.Empty;
	Dictionary<string, State> gameStates;
	State currentGameState;

	public StateManager()
	{
		gameStates = new Dictionary<string, State>();
		currentGameState = null;
		//TODO:AddState(TITLE_SCREEN, new TitleScreen());
	}

	public void AddState(string name, State state)
	{
		gameStates[name] = state;
	}

	public ILoopObject GetState(string name)
	{
		return gameStates[name];
	}

	public void SwitchTo(string name)
	{
		if (name == GO_TO_PREVIOUS_SCREEN)
		{
			GoToPreviousScreen();
		}
		else
		{
			SwitchToState(name);
		}
	}

	private void SwitchToState(string name, bool addStateToStack = true)
	{
		if (gameStates.ContainsKey(name))
		{
			if (addStateToStack && currentStateName != string.Empty)
			{
				previousGameStates.Push(currentStateName);
			}
			currentGameState = gameStates[name];
			currentStateName = name;
			currentGameState.Reset();
		}
		else
		{
			throw new KeyNotFoundException("Could not find game state: " + name);
		}
	}

	private void GoToPreviousScreen()
	{
		if (previousGameStates.Count > 0)
		{
			string previousState = previousGameStates.Pop();

			//Do not add the current state to the stack because we are going back to the previous state.
			SwitchToState(previousState, false);
		}
	}

	public ILoopObject CurrentGameState
	{
		get
		{
			return currentGameState;
		}
	}

	public void HandleInput(InputHelper inputHelper)
	{
		currentGameState?.HandleInput(inputHelper);
	}

	public void Update(GameTime gameTime)
	{
		currentGameState?.Update(gameTime);
	}

	public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		currentGameState?.Draw(gameTime, spriteBatch);
	}

	public void Reset()
	{
		currentGameState?.Reset();
	}
}
