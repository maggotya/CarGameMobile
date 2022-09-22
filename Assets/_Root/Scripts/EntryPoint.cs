using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
    }
}