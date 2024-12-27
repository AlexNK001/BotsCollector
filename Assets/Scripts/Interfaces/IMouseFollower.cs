using UnityEngine;

public interface IMouseFollower
{
    public void Cancel();
    public void Click();
    public void Move(Vector3 position);
    public void Stop();
}
