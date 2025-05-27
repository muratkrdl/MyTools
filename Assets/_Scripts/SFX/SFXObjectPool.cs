using _Scripts.ObjectPool;
using UnityEngine;

namespace _Scripts.SFX
{
    public class SFXObjectPool : BaseObjectPool<SFXObject, SFXObjectPool>
    {
        protected override void OnRelease(SFXObject obj)
        {
            base.OnRelease(obj);
            obj.SetPosition(Vector3.zero);
        }
    }
}