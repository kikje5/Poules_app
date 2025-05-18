using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JuKu_Poules.Engine.UI;

public class Button : UIElement
{
	public Button(Vector2 position, Vector2 size) : base(
		App.Instance.AssetManager.GetTexture("UI/Buttons/ButtonNormal"),
		App.Instance.AssetManager.GetTexture("UI/Buttons/ButtonHover"),
		App.Instance.AssetManager.GetTexture("UI/Buttons/ButtonPressed"),
		App.Instance.AssetManager.GetTexture("UI/Buttons/ButtonDisabled"),
		position,
		size
		)
	{ }

	public override void HandleInput(InputHelper inputHelper)
	{
		if (UIElementState == UIElementMouseState.Disabled) return;
		Vector2 mousePosition = inputHelper.MousePosition;
		int mouseX = (int)mousePosition.X;
		int mouseY = (int)mousePosition.Y;
		if (mouseX >= CollisionRectangle.X && mouseX <= CollisionRectangle.X + CollisionRectangle.Width &&
			mouseY >= CollisionRectangle.Y && mouseY <= CollisionRectangle.Y + CollisionRectangle.Height)
		{
			if (inputHelper.MouseLeftButtonPressed)
			{
				isClicked = true;
				UIElementState = UIElementMouseState.Pressed;
			}
			else if (inputHelper.MouseLeftButtonReleased && isClicked)
			{
				isClicked = false;
				OnClicked();
			}
			else
			{
				UIElementState = UIElementMouseState.Hover;
			}
		}
		else
		{
			isClicked = false;
			UIElementState = UIElementMouseState.Normal;
		}
	}
}