using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JuKu_Poules.Engine.UI;

public class TextInput : UIElement
{
	public string Text { get; set; } = string.Empty;

	public TextInput(Vector2 position, Vector2 size) : base(
		App.Instance.AssetManager.GetTexture("UI/TextInput/TextInputNormal"),
		App.Instance.AssetManager.GetTexture("UI/TextInput/TextInputHover"),
		App.Instance.AssetManager.GetTexture("UI/TextInput/TextInputPressed"),
		App.Instance.AssetManager.GetTexture("UI/TextInput/TextInputDisabled"),
		position,
		size
		)
	{ }

	public override void HandleInput(InputHelper inputHelper)
	{
		if (UIElementState == UIElementMouseState.Disabled) return;
		if (inputHelper.MouseLeftButtonPressed)
		{
			Vector2 mousePosition = inputHelper.MousePosition;
			int mouseX = (int)mousePosition.X;
			int mouseY = (int)mousePosition.Y;
			if (mouseX >= CollisionRectangle.X && mouseX <= CollisionRectangle.X + CollisionRectangle.Width &&
				mouseY >= CollisionRectangle.Y && mouseY <= CollisionRectangle.Y + CollisionRectangle.Height)
			{
				isClicked = true;
				UIElementState = UIElementMouseState.Pressed;
			}
			else
			{
				isClicked = false;
				UIElementState = UIElementMouseState.Normal;
			}
		}

		if (!isClicked) return;

		//if it is clicked, do text input logic

		inputHelper.GetKeyPressed(out Keys key);
		Console.WriteLine(key);

		if (key == Keys.None) return;

		if (key == Keys.Back)
		{
			if (Text.Length > 0)
			{
				Text = Text.Substring(0, Text.Length - 1);
			}
			return;
		}
		if (Text.Length >= 40) return;

		if (key == Keys.Space)
		{
			Text += " ";
			return;
		}

		//letters
		if ((int)key < 65 || (int)key > 90) return;

		if (inputHelper.ShiftKeyDown)
		{
			Text += key.ToString();
		}
		else
		{
			Text += key.ToString().ToLower();
		}
	}
}