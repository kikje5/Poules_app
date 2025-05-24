using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Mono_Sims.Engine;

public interface ILoopObject
{
	void Update(GameTime gameTime);
	void Draw(GameTime gameTime, SpriteBatch spriteBatch);
	void HandleInput(InputHelper inputHelper);
	void Reset();
}
