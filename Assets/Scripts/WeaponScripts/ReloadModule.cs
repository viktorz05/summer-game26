using UnityEngine;
using System.Collections;

public class ReloadModule : MonoBehaviour
{
    private WeaponData _data;
    public bool isReloading;
    public void Reload()
    {
       if(!isReloading)
       {
            StartCoroutine(ReloadCoroutine());
       }
    }   
    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(_data.reloadSpeed);
        isReloading = false;
    }
}
