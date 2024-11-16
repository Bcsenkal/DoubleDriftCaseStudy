
using UnityEngine;

namespace Managers
{
    public sealed class EventManager : Singleton<EventManager>
    {
        
#region Level Status

        public event System.Action<bool> ONLevelEnd;
        public event System.Action ONLevelStart;
        
        public void OnONLevelStart(){
            ONLevelStart?.Invoke();
        }
        public void OnONLevelEnd(bool isSuccess)
        {
            ONLevelEnd?.Invoke(isSuccess);
        }

#endregion

#region Data Flow

        public event System.Action<Transform> OnSendPlayerData;
        public void ONOnSendPlayerData(Transform player)
        {
            OnSendPlayerData?.Invoke(player);
        }

#endregion

#region Object Creation

        public event System.Action OnCreateRoad;

        public void ONOnCreateRoad()
        {
            OnCreateRoad?.Invoke();
        }

#endregion

#region Player Movement
        public event System.Action OnPlayerCrash;

        public void ONOnPlayerCrash()
        {
            OnPlayerCrash?.Invoke();
        }

#endregion

#region Customization

        public event System.Action<ColorType> OnSetPlayerColor;

        public void ONOnSetPlayerColor(ColorType colorType)
        {
            OnSetPlayerColor?.Invoke(colorType);
        }
#endregion

#region Input

        public event System.Action<Vector2> OnMouseDown;
        public event System.Action<Vector2> OnMouseUp;

        public event System.Action<Vector2> OnSendCurrentDelta;
        public event System.Action OnStopRotation;

        public void ONOnMouseDown(Vector2 position)
        {
            OnMouseDown?.Invoke(position);
        }

        public void ONOnMouseUp(Vector2 position)
        {
            OnMouseUp?.Invoke(position);
        }

        public void ONOnSendCurrentDelta(Vector2 delta)
        {
            OnSendCurrentDelta?.Invoke(delta);
        }

        public void ONOnStopRotation()
        {
            OnStopRotation?.Invoke();
        }

#endregion


        //remove listeners from all of the events here
        public void NextLevelReset()
        {
            ONLevelStart= null;
            ONLevelEnd = null;
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
