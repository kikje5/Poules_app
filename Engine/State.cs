using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace JuKu_Poules.Engine;

public class State : ILoopObject
{
	private List<ILoopObject> Children;
	private Queue<ILoopObject> ObjectsToRemove;

	public State()
	{
		Children = new List<ILoopObject>();
		ObjectsToRemove = new Queue<ILoopObject>();
	}

	public void Add(ILoopObject obj)
	{
		Children.Add(obj);
	}

	public void Remove(ILoopObject obj)
	{
		ObjectsToRemove.Enqueue(obj);
	}

	public void Update(GameTime gameTime)
	{
		for (int i = 0; i < Children.Count; i++)
		{
			Children[i].Update(gameTime);
		}
		while (ObjectsToRemove.Count > 0)
		{
			ILoopObject obj = ObjectsToRemove.Dequeue();
			Children.Remove(obj);
		}
	}

	public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		for (int i = 0; i < Children.Count; i++)
		{
			Children[i].Draw(gameTime, spriteBatch);
		}
	}

	public void HandleInput(InputHelper inputHelper)
	{
		for (int i = 0; i < Children.Count; i++)
		{
			Children[i].HandleInput(inputHelper);
		}
	}

	public void Reset()
	{
		for (int i = 0; i < Children.Count; i++)
		{
			Children[i].Reset();
		}
	}
}