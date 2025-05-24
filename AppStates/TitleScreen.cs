using Mono_Sims.Engine;
using Mono_Sims.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono_Sims.AppStates;

public class TitleScreen : State
{
	private Button CreteNewPouleButton;
	private Button LoadPouleButton;
	private Button exitButton;

	private TextInput textInput;
	private TextElement titleText;

	public TitleScreen()
	{
		int ButtonWidth = 192;
		int ButtonHeight = 48;
		titleText = new TextElement("Fonts/TitleFont");
		titleText.Text = "Mono Sims";
		titleText.Position = new Vector2(480, 50);
		Add(titleText);

		CreteNewPouleButton = new Button(new Vector2(480, 200), new Vector2(ButtonWidth, ButtonHeight));
		CreteNewPouleButton.Text = "Create Poule";
		CreteNewPouleButton.Clicked += () => App.StateManager.SwitchTo(StateManager.TITLE_SCREEN);
		Add(CreteNewPouleButton);

		LoadPouleButton = new Button(new Vector2(480, 270), new Vector2(ButtonWidth, ButtonHeight));
		LoadPouleButton.Text = "Load Poule";
		LoadPouleButton.Clicked += () => App.StateManager.SwitchTo(StateManager.TITLE_SCREEN);
		Add(LoadPouleButton);

		exitButton = new Button(new Vector2(480, 340), new Vector2(ButtonWidth, ButtonHeight));
		exitButton.Clicked += () => App.Instance.Exit();
		exitButton.Text = "Exit";
		Add(exitButton);
	}
}