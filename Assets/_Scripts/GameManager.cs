/*
 * Branch: Hossein (Higham, Ben)
 * Commit: 057afddb335e248e8a0c986f6117e1e702cddf11
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
 */

// Updated 14/10/24 (Higham, Ben), changes commented

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //I've moved any references I could (without redoing too much stuff) to a ReferenceManager. Level-only references should not be handled in the GameManager as it persists across
    //all scenes (It's a manager for the entire game). (BH)

    public static GameManager instance;
    public int finalScore;
    
    // Separated Litter management into separate class (BH)
    private LitterManager _litterManager;
    private AI_SpawnManager _AISpawnManager;

    private PlayerScript _playerScript;
    private ScoreManager _scoreManager;
    private ReferenceManager _referenceManager;
    private LitterTracker _litterTracker;
    private UIManager _uiManager;
    private AudioManager _audioManager;
    private VisualEffectManager _visualEffectManager;
    
    public int PlayerScore = 0;
    public int StoredScore = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            //Creation will be handled outside of awake once menu scenes are added, likely won't happen for submission (too much reorganising because references weren't being maintained properly.(BH)
            //Keep reference manager at the top.
            _referenceManager = FindObjectOfType<ReferenceManager>();
            _litterManager = new LitterManager();
            _AISpawnManager = new AI_SpawnManager();
            _audioManager = new AudioManager();
            _visualEffectManager = new VisualEffectManager();
            _playerScript = FindObjectOfType<PlayerScript>();
            _scoreManager = FindObjectOfType<ScoreManager>();

            _referenceManager.Init();
            _AISpawnManager.Init();
            //_uiManager.Init();
        }

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if(_AISpawnManager == null)
        {
            Debug.LogError("Missing AI_SpawnManager Reference in GameManager");
            return;
        }

        instance._AISpawnManager.Update(Time.deltaTime);
    }
    public static VisualEffectManager GetVisualEffectManager()
    {
        return instance._visualEffectManager;   
    }
    public static AudioManager GetAudioManager()
    {
        return instance._audioManager;
    }
    public static ReferenceManager GetReferenceManager()
    {
        return instance._referenceManager;
    }
    public static LitterManager GetLitterManager()
    {
        return instance._litterManager;
    }
    public static AI_SpawnManager GetAISpawnManager()
    {
        return instance._AISpawnManager;
    }
    public static PlayerScript GetPlayerScript()
    {
        return instance._playerScript;
    }
    public static ScoreManager GetScoreManager()
    {
        return instance._scoreManager;
    }

    public static UIManager GetUIManager()
    {
        return instance._uiManager;
    }

    public static LitterTracker GetLitterTracker()
    {
        return instance._litterTracker;
    }





}

/*public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public List<GameObject> LitterInstantiated = new List<GameObject>();
   
   private void Awake()
   {
      if (instance != null && instance != this) Destroy(gameObject);
      else instance = this;
   }
}*/