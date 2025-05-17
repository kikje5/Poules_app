using JuKu_Poules.Engine;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace Blok3Game.Engine.UI;

public enum UIElementMouseState
{
	Disabled = 0,
	Normal = 1,
	Hover = 2,
	Pressed = 3
}

public abstract class UIElement : ILoopObject
{
	private Texture2D _disabledTexture;
	private Texture2D _normalTexture;
	private Texture2D _hoverTexture;
	private Texture2D _pressedTexture;
	private Texture2D _currentTexture;
	public UIElementMouseState UIElementState
	{
		get
		{
			return uiElementMouseState;
		}
		set
		{
			uiElementMouseState = value;
			switch (uiElementMouseState)
			{
				case UIElementMouseState.Disabled:
					_currentTexture = _disabledTexture;
					break;
				case UIElementMouseState.Normal:
					_currentTexture = _normalTexture;
					break;
				case UIElementMouseState.Hover:
					_currentTexture = _hoverTexture;
					break;
				case UIElementMouseState.Pressed:
					_currentTexture = _pressedTexture;
					break;
			}
		}
	}
	private UIElementMouseState uiElementMouseState;

	public event Action<UIElement> Clicked;

	protected bool isClicked = false;

	private Rectangle CollisionRectangle;

	public Vector2 Position
	{
		get
		{
			return new Vector2(CollisionRectangle.X, CollisionRectangle.Y);
		}
		set
		{
			CollisionRectangle.X = (int)value.X;
			CollisionRectangle.Y = (int)value.Y;
		}
	}

	public Vector2 Size
	{
		get
		{
			return new Vector2(CollisionRectangle.Width, CollisionRectangle.Height);
		}
		set
		{
			CollisionRectangle.Width = (int)value.X;
			CollisionRectangle.Height = (int)value.Y;
		}
	}

	public UIElement(Texture2D normalTexture, Texture2D hoverTexture, Texture2D pressedTexture, Texture2D disabledTexture, Vector2 position, Vector2 size)
	{
		CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
		_normalTexture = normalTexture;
		_hoverTexture = hoverTexture;
		_pressedTexture = pressedTexture;
		_disabledTexture = disabledTexture;
		_currentTexture = _normalTexture;
	}

	protected void OnClicked()
	{
		Clicked?.Invoke(this);
	}

	public virtual void Update(GameTime gameTime)
	{

	}

	public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(_currentTexture, CollisionRectangle, Color.White);
	}

	public virtual void HandleInput(InputHelper inputHelper)
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

	public virtual void Reset()
	{
		isClicked = false;
		UIElementState = UIElementMouseState.Normal;
	}
}
