using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace Mono_Sims.Engine;

public class State : ILoopObject
{
	private List<ILoopObject> Elements;
	private Queue<ILoopObject> ObjectsToRemove;

	public State()
	{
		Elements = new List<ILoopObject>();
		ObjectsToRemove = new Queue<ILoopObject>();
	}

	public void Add(ILoopObject obj)
	{
		Elements.Add(obj);
	}

	public void Remove(ILoopObject obj)
	{
		ObjectsToRemove.Enqueue(obj);
	}

	public virtual void Update(GameTime gameTime)
	{
		for (int i = 0; i < Elements.Count; i++)
		{
			Elements[i].Update(gameTime);
		}
		while (ObjectsToRemove.Count > 0)
		{
			ILoopObject obj = ObjectsToRemove.Dequeue();
			Elements.Remove(obj);
		}
	}

	public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		for (int i = 0; i < Elements.Count; i++)
		{
			Elements[i].Draw(gameTime, spriteBatch);
		}
	}

	public virtual void HandleInput(InputHelper inputHelper)
	{
		for (int i = 0; i < Elements.Count; i++)
		{
			Elements[i].HandleInput(inputHelper);
		}
	}

	public virtual void Reset()
	{
		for (int i = 0; i < Elements.Count; i++)
		{
			Elements[i].Reset();
		}
	}
}