using UnityEngine;
using System.Collections;

public static class Game {
	
	private static BallCreator _ballCreator;
	public static BallCreator ballCreator {
		get {
			if(_ballCreator) return _ballCreator;
			_ballCreator = MonoBehaviour.Instantiate(Game.settings.ballCreator, Game.settings.ballCreator.transform.position, Quaternion.identity) as BallCreator;
			return _ballCreator;
		}
		set {
			return;
		}
	}
	
	private static InfoText _infoText;
	public static InfoText infoText {
		get {
			if(_infoText) return _infoText;
			_infoText = MonoBehaviour.Instantiate(Game.settings.infoText) as InfoText;
			return _infoText;
		}
		set {
			return;
		}
	}
	
	private static GameTime _time;
	public static GameTime time {
		get {
			if(_time) return _time;
			_time = (new GameObject("GameTime")).AddComponent<GameTime>();
			return _time;
		}
		set {
			return;
		}
	}
	
	private static GameLoader _loader;
	public static GameLoader loader {
		get {
			if(_loader) return _loader;
			_loader = (new GameObject("GameLoader")).AddComponent<IndestructableMenu>().gameObject.AddComponent<GameLoader>();
			return _loader;
		}
		set {
			return;
		}
	}
	
	private static GameScore _score;
	public static GameScore score {
		get {
			if(_score) return _score;
			_score = (new GameObject("GameScore")).AddComponent<GameScore>();
			return _score;
		}
		set {
			return;
		}
	}
	
	private static Settings _settings;
	public static Settings settings {
		get {
			if(_settings) return _settings;
			_settings = MonoBehaviour.FindObjectOfType(typeof(Settings)) as Settings;
			return _settings;
		}
		set {
			return;
		}
	}
	
	private static SoundHandler _sound;
	public static SoundHandler sound {
		get {
			if(_sound) return _sound;
			_sound = MonoBehaviour.FindObjectOfType(typeof(SoundHandler)) as SoundHandler;
			return _sound;
		}
		set {
			return;
		}
	}
	
	public static void Over() {
		Debug.Log("Game Over");
		OnGameOver();
	}
	
	public static Callback OnGameOver;
	
}
