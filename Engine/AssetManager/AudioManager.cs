using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Blok3Game.Engine;

public class AudioManager
{
	private static readonly string FilePathContentAudioEffects = Path.Combine("Content", "Audio", "AudioEffects");
	private static readonly string FilePathAudioEffects = Path.Combine("Audio", "AudioEffects");
	private static readonly string FilePathContentMusic = Path.Combine("Content", "Audio", "Music");
	private static readonly string FilePathMusic = Path.Combine("Audio", "Music");

	private readonly Dictionary<string, Song> songs = new Dictionary<string, Song>();
	private readonly Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

	public float MusicVolume { get; set; } = 0.5f;

	public float EffectVolume { get; set; } = 0.5f;

	public void LoadAllAudio(ContentManager contentManager)
	{
		LoadSoundEffects(contentManager);
		LoadSongs();
	}

	private void LoadSoundEffects(ContentManager contentManager)
	{
		foreach (var filePath in Directory.GetFiles(FilePathContentAudioEffects))
		{
			var assetName = Path.GetFileNameWithoutExtension(filePath);

			soundEffects.Add(assetName, contentManager.Load<SoundEffect>(Path.Combine(FilePathAudioEffects, assetName)));
		}
	}

	private void LoadSongs()
	{
		foreach (var filePath in Directory.GetFiles(FilePathContentMusic))
		{
			var songName = Path.GetFileNameWithoutExtension(filePath);

			if (songs.ContainsKey(songName)) continue;

			songs.Add(songName, Song.FromUri(songName,
				new Uri(Path.Combine(FilePathContentMusic, songName + ".ogg"), UriKind.Relative)));
		}

	}

	public void LoadAllSongs(ContentManager contentManager)
	{
		foreach (var filePath in Directory.GetFiles(FilePathContentMusic))
		{
			var assetName = Path.GetFileNameWithoutExtension(filePath);

			songs.Add(assetName, contentManager.Load<Song>(Path.Combine(FilePathMusic, assetName)));
		}
	}

	public void PlaySong(string assetName, bool repeat = false)
	{
		if (string.IsNullOrEmpty(assetName)) throw new ArgumentNullException(nameof(assetName));

		MediaPlayer.Volume = MusicVolume;
		MediaPlayer.IsRepeating = repeat;

		MediaPlayer.Play(songs[assetName]);
	}

	public void StopSong()
	{
		MediaPlayer.Stop();
	}

	public void PauseSong()
	{
		MediaPlayer.Pause();
	}

	public void ResumeSong()
	{
		MediaPlayer.Resume();
	}

	public void ChangeMusicVolume()
	{
		MediaPlayer.Volume = MusicVolume;
	}

	/// <summary>
	/// Play sound effect from the effects dictionary.
	/// </summary>
	/// <param name="assetName"> Sound effect to play. </param>
	/// <param name="pitch"> Float to change the pitch of sound effect. </param>
	/// <param name="pan"> Float to change the panning of the sound effect, -1.0 left, 1.0 right. </param>
	public void PlaySoundEffect(string assetName, float pitch = 0, float pan = 0f)
	{
		if (string.IsNullOrEmpty(assetName))
		{
			throw new ArgumentNullException(nameof(assetName));
		}

		if (!soundEffects.ContainsKey(assetName))
		{
			throw new ArgumentException($"Sound effect ({assetName}) not found in dictionary. Did you use the correct name?");
		}

		soundEffects[assetName].Play(EffectVolume, pitch, pan);
	}
}