
using UnityEngine;

namespace Managers
{

    //This is an event manager my current lead taught me the moment I started working with him.
    //I've been using this type of observation since then.

    
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

#region Visuals Effects

        public event System.Action<Vector3,ParticleType> OnPlayParticleHere;

        public void ONOnPlayParticleHere(Vector3 position,ParticleType type)
        {
            OnPlayParticleHere?.Invoke(position,type);
        }

#endregion

#region UI

        public event System.Action<int,bool> OnSetCurrentCoin;
        public event System.Action OnOpenCarSelection;
        public event System.Action OnCloseCarSelection;
        public event System.Action<bool> OnEnableCarSelection;

        public event System.Action<bool> OnBlockInput;

        public void ONOnSetCurrentCoin(int amount,bool isIncrement)
        {
            OnSetCurrentCoin?.Invoke(amount,isIncrement);
        }

        public void ONOnOpenCarSelection()
        {
            OnOpenCarSelection?.Invoke();
        }

        public void ONOnCloseCarSelection()
        {
            OnCloseCarSelection?.Invoke();
        }

        public void ONOnEnableCarSelection(bool b)
        {
            OnEnableCarSelection?.Invoke(b);
        }

        public void ONOnBlockInput(bool b)
        {
            OnBlockInput?.Invoke(b);
        }

#endregion

#region Data Flow

        public event System.Action<Transform> OnSendPlayerData;
        public event System.Action<float> OnSetRoadProgress;
        public event System.Action<float> OnSetSpeedMeter;
        public void ONOnSendPlayerData(Transform player)
        {
            OnSendPlayerData?.Invoke(player);
        }

        public void ONOnSetRoadProgress(float progress)
        {
            OnSetRoadProgress?.Invoke(progress);
        }

        public void ONOnSetSpeedMeter(float speed)
        {
            OnSetSpeedMeter?.Invoke(speed);
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

            OnPlayParticleHere = null;
            OnSetCurrentCoin = null;
            OnOpenCarSelection = null;
            OnCloseCarSelection = null;
            OnEnableCarSelection = null;
            OnBlockInput = null;

            OnSendPlayerData = null;

            OnCreateRoad = null;

            OnPlayerCrash = null;

            OnSetPlayerColor = null;

            OnMouseDown = null;
            OnMouseUp = null;
            OnSendCurrentDelta = null;
            OnStopRotation = null;

            OnSetRoadProgress = null;
            OnSetSpeedMeter = null;
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
